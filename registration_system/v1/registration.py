import os
import cgi
import logging
import itertools 
import hashlib
from SendMail import *
from Member import *
from datetime import datetime
from google.appengine.ext.webapp import template
from google.appengine.ext.webapp.util import run_wsgi_app
from google.appengine.ext import webapp
from google.appengine.api import users
from google.appengine.api import memcache

class FormField(object):
    def __init__(self, label, inputName, value, formType, formOptions):
        self.formLabel = label
        self.formInputName = inputName
        self.formValue = value
        self.formType = formType
        self.formOptions = formOptions

class Url(object):
    def __init__(self, urlName, url):
        self.urlName = urlName
        self.url = url

class RegisterPage(webapp.RequestHandler):
    @staticmethod
    def getNext(iterable):
        try:
            value =  iterable.next()
        except StopIteration:
            value = '' 
        return value

    def getInput(self, formId):
        # obtains object value from form and escapes it
        return cgi.escape(self.request.get(formId))
    
    def getFormTemplate(self, formData, messageList):
        it = iter(formData)

        # radio button values
        types = []
        types.append("Student")
        types.append("Faculty")
        types.append("Other (Non-AMS)")

        skillLevels = []
        skillLevels.append("Beginner")
        skillLevels.append("Intermediate I")
        skillLevels.append("Intermediate II")
        skillLevels.append("Advanced")

        memberTypes = []
        memberTypes.append("New")
        memberTypes.append("Returning")

        # building form
        formFieldList = []
        formFieldList.append(FormField("First Name", "firstName", RegisterPage.getNext(it), "text", ""))
        formFieldList.append(FormField("Last Name", "lastName", RegisterPage.getNext(it), "text", ""))
        formFieldList.append(FormField("UBC Affliation", "ubcAffliation", RegisterPage.getNext(it), "radio", types))
        formFieldList.append(FormField("Student No", "studentNo", RegisterPage.getNext(it), "text", ""))
        formFieldList.append(FormField("Phone Number", "phoneNumber", RegisterPage.getNext(it), "text", ""))
        formFieldList.append(FormField("Email", "email", RegisterPage.getNext(it), "text", ""))
        formFieldList.append(FormField("Member Type", "memberType", RegisterPage.getNext(it), "radio", memberTypes))
        formFieldList.append(FormField("Skill Level", "level", RegisterPage.getNext(it), "radio", skillLevels)) 

        urlList = []
        urlList.append(Url("Logout", users.create_logout_url(self.request.uri)))
        
        template_values = {
            'messageList': messageList,
            'formFieldList': formFieldList,
            'urlList': urlList
        }
        return template_values

    def get(self):
        if Member.isFull():
            pageContent = "Oh Noes! Membership is full! " \
                          "Due to limited gym space, we are only able to accept a limited amount of members. " \
                          "<p>You can place your name and contact information on the waitlist in the scenario that room in the club is available.</p>"
 
            template_values = {
                'content' : pageContent
            }
       
            path = os.path.join(os.path.dirname(__file__), 'templates', 'basic.html')
            self.response.out.write(template.render(path, template_values))
        else:
            template_values = self.getFormTemplate('', '')
            path = os.path.join(os.path.dirname(__file__), 'templates', 'registerForm.html')
            self.response.out.write(template.render(path, template_values))

    def post(self):
        firstName = self.getInput('firstName')
        lastName = self.getInput('lastName')
        ubcAffliation = self.getInput('ubcAffliation')
        studentNo = self.getInput('studentNo')
        phoneNumber = db.PhoneNumber(self.getInput('phoneNumber'))
        email = db.Email(self.getInput('email'))
        memberType = self.getInput('memberType')
        level = self.getInput('level')

        # must be appended in same order as form
        formData = []
        formData.append(firstName)
        formData.append(lastName)
        formData.append(ubcAffliation)
        formData.append(studentNo)
        formData.append(phoneNumber)
        formData.append(email)
        formData.append(memberType)
        formData.append(level)

        error = False       
        message = [] 

        # Check email address
        if Member.isValidEmail(email) is False:
            message.append("Email is already in use by another member")
            error = True
        
        # Check for duplicate student number
        if Member.isValidStudentNo(studentNo) is False:
            message.append("Student Number already registered")
            error = True
        
        if error is True:
            # re-post page with previously inputted information
            template_values = self.getFormTemplate(formData, message)
            path = os.path.join(os.path.dirname(__file__), 'templates', 'registerForm.html')
            self.response.out.write(template.render(path, template_values))
        else:
            # otherwise, memcache Member object
            member = Member(firstName = firstName,
                            lastName = lastName,
                            ubcAffliation = ubcAffliation,
                            phoneNumber = phoneNumber,
                            email = email,
                            memberType = memberType,
                            level = level)

            if ubcAffliation == 'Student':
                member.studentNo = int(studentNo)

            if ubcAffliation == 'Other':
                member.memberType = "Non-AMS"

            key = str(datetime.now()) + str(member.email)
            hash = hashlib.md5()
            hash.update(key)
            hex = hash.hexdigest()
            
            memcache.add(hex, member, 300)
            logging.info('Memcache - Adding entry with key %s ' % hex)
            self.redirect("/register/confirm?id="+hex)

class ConfirmPage(webapp.RequestHandler):
    def get(self):
        key = self.request.get("id")        
        member = memcache.get(key)

        if member is None:
            # obtain Member object from memcache, if missing redirect to registration form
            logging.warn("Memcache - Miss")
            self.redirect("/register")
        else:
            urlList = []
            urlList.append(Url('Back', '/register'))

            template_values = {
                'key': key,
                'ubcAffliation': member.ubcAffliation,
                'memberType': member.memberType,
                'urlList': urlList
            }
       
            path = os.path.join(os.path.dirname(__file__), 'templates', 'registerConfirm.html')
            self.response.out.write(template.render(path, template_values)) 

    def post(self):
        key = self.request.get("id")
        member = memcache.get(key)

        if member is None:
            logging.warn("Memcache - Miss")
            self.redirect("/register")
        else:
            # Create the member and delete cached object
            member.memberNo = Member.nextAvailableMemberNo()
            member.Create()
            memcache.delete(key)
            logging.info('Memcache - Remove item with key %s ' % key)
            self.redirect("/register/done?key=" + str(member.key()))
        
class DonePage(webapp.RequestHandler):
    def get(self):
        key_name = self.request.get("key")

        try:
            key = db.Key(key_name)
            member = db.get(key)

            msgBody =   'Hello ' + member.firstName + ' (aka. member number <b>' + str(member.memberNo) + '</b>), \n\n' \
                        '<p>Welcome to the world of UBC Badminton! ' \
                        '<p>Here is some useful information regarding upcoming events:</p>' \
                        '<p><u>Gym Nights:</u></p>' \
                        '<p>For term 1, gym nights will be Tuesdays from 4-6pm and Fridays from 6:30-11pm. ' \
                        'The last gym night of Term 1 will be Friday, Dec. 4. '\
                        'Gym nights are held at the Osbourne Center - Gym A (next to Thunderbird Arena).</p>' \
                        '<p><u>New Members Orientation</u></p>' \
                        '<p>Happening Tuesday, Sept. 29th from 4-6pm at Osbourne, all new members are invited to attend the New Members Orientation where we\'ll be introducing the club to all you newbies. '\
                        'This event is for <i>new</i> members only.</p>' \
                        '<p><u>IceBreaker!</u></p>' \
                        '<p><i>Question:</i> how heavy is a polar bear? <i>Answer:</i> enough to break the ice! Hehe. '\
                        'Be sure to sign up for our IceBreaker event which is happening Friday, Oct. 2 at 4:30pm (2 hours before the first Friday gym night). '\
                        'If you want to sign up, let us know by emailing us!</p>'\
                        '<p>For more information about any of these events or about the club itself, check us out on our webpage. ' 

            email = SendMail(users.get_current_user().email(),
                            member.email, 
                            'Registration Confirmation ' + member.emailHash,
                            msgBody)
            email.send()
            
            pageContent = '<p>' + member.firstName + ' (aka member number <b>' + str(member.memberNo) + '</b>),</p>' \
                          '<p>Woot! Congratulations on becoming a member of the UBC Badminton Club! Your confirmation code is <i>' + member.emailHash + '</i>.</p>' \
                          '<p><b>Important Dates:</b></p>' \
                          '<ul><li><i>Tuesday, Sept. 29</i> - New Members Orientation. We\'ll introduce the world of UBC Badminton to all the newbies. As such, gym night will be open to <u>new</u> members only.</li>' \
                          '    <li><i>Friday, Oct. 2</i> - IceBreaker! and first gym night open to all members (new and returning)</li></ul>' \
                          '<p>Don\'t worry about copy all this information down. ' \
                          'An email was just sent to your inbox containing the same information.</p>'

            urlList = []
            urlList.append(Url('Back', '/register'))
 
            template_values = {
                'content' : pageContent,
                'urlList' : urlList
            }
       
            path = os.path.join(os.path.dirname(__file__), 'templates', 'basic.html')
            self.response.out.write(template.render(path, template_values)) 

        except:
            logging.warn('Error retreiving member with key %s' % key_name)

            template_values = {
                'content' : "404 - Uh Oh! We weren't able to retreive the member with that particular id. Please talk to your nearest executive for HeLp!" ,
            }

            path = os.path.join(os.path.dirname(__file__), 'templates', 'basic.html')
            self.response.out.write(template.render(path, template_values))

application = webapp.WSGIApplication(   [('/register', RegisterPage),
                                         ('/register/confirm', ConfirmPage),
                                         ('/register/done', DonePage)],
                                        debug=True)

def main():
    run_wsgi_app(application)

if __name__ == "__main__":
    main()
