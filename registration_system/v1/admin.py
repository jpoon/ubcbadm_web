import os
import cgi
import logging
import urllib
from Member import *
from Template import *
from google.appengine.ext.webapp import template
from google.appengine.ext.webapp.util import run_wsgi_app
from google.appengine.ext import webapp
from google.appengine.api import users

class MemberEdit(object):
    def __init__(self, attributeName, prevValue, newValue):
        self.name = attributeName
        self.prev = prevValue
        self.new = newValue

class AdminPage(webapp.RequestHandler):
    def get(self):
        content = """<p><a href="/admin/memberList">Member List</a></p>
                     <p><a href="/admin/mailingList">Member Emails</a></p>"""

        urlList = []
        urlList.append(Url('Home', '/admin'))
        urlList.append(Url('Logout', users.create_logout_url(self.request.uri)))

        template_values = {
            'content' : content,
            'urlList' : urlList
        }

        path = os.path.join(os.path.dirname(__file__), 'templates', 'basic.html')
        self.response.out.write(template.render(path, template_values))

class MemberMailingListPage(webapp.RequestHandler):
    def get(self):
        content = """<span id="emails">""" + Member.getAllEmails() + """</span>"""

        urlList = []
        urlList.append(Url('Home', '/admin'))
        urlList.append(Url('Logout', users.create_logout_url(self.request.uri)))

        template_values = {
            'content' : content,
            'urlList' : urlList
        }

        path = os.path.join(os.path.dirname(__file__), 'templates', 'basic.html')
        self.response.out.write(template.render(path, template_values))

class MemberListPage(webapp.RequestHandler):
    def get(self):
        msg = self.request.get("msg")
        sortMethod = self.request.get("sort")

        urlList = []
        urlList.append(Url('Home', '/admin'))
        urlList.append(Url('Logout', users.create_logout_url(self.request.uri)))

        template_values = {
            'message': msg,
            'memberList' : Member.getMemberList(sortMethod),
            'urlList' : urlList
        }

        path = os.path.join(os.path.dirname(__file__), 'templates', 'memberList.html')
        self.response.out.write(template.render(path, template_values))

class MemberEditPage(webapp.RequestHandler):
    def getInput(self, formId):
        return cgi.escape(self.request.get(formId))

    def get(self):
        emailHash = self.request.get("id")
    
        message = "Action Canceled"
        urllib.quote(message)

        urlList = []
        urlList.append(Url('Cancel', '/admin/memberList?msg='+message))
        urlList.append(Url('Logout', users.create_logout_url(self.request.uri)))

        template_values = {
            'id': emailHash,
            'member': Member.getMember(emailHash),
            'urlList' : urlList
        }

        path = os.path.join(os.path.dirname(__file__), 'templates', 'memberEdit.html')
        self.response.out.write(template.render(path, template_values))

    def post(self):
        emailHash = self.request.get("id")
        member = Member.getMember(emailHash)

        confirmEdit = self.request.get("confirm")

        if not confirmEdit:
            firstName = self.getInput('firstName')
            lastName = self.getInput('lastName')
            email = self.getInput('email')

            memberEditList = []

            if member.firstName != firstName:
                memberEditList.append(MemberEdit("first name", member.firstName, firstName))            

            if member.lastName != lastName:
                memberEditList.append(MemberEdit("last name", member.lastName, lastName))            

            if member.email != email:
                memberEditList.append(MemberEdit("email", member.email, email))            

            if memberEditList:
                message = "Action Canceled"
                urllib.quote(message)

                urlList = []
                urlList.append(Url('Cancel', '/admin/memberList?msg='+message))
                urlList.append(Url('Logout', users.create_logout_url(self.request.uri)))

                template_values = {
                    'emailHash': emailHash,
                    'memberEditList': memberEditList,
                    'urlList' : urlList
                }

                path = os.path.join(os.path.dirname(__file__), 'templates', 'memberEditConfirm.html')
                self.response.out.write(template.render(path, template_values))
            else:
                message = "No modifications made"
                urllib.quote(message)

                self.redirect("/admin/memberList?msg="+message)

        else:
            firstName = self.getInput('first name')
            lastName = self.getInput('last name')
            email = self.getInput('email')

            if firstName:
                member.firstName = firstName

            if lastName:
                member.lastName = lastName

            if email:
                member.email = email

            member.put()
            
            message = "Saved member information"
            urllib.quote(message)

            self.redirect("/admin/memberList?msg="+message)

application = webapp.WSGIApplication(   [('/admin', AdminPage),
                                         ('/admin/mailingList', MemberMailingListPage),
                                         ('/admin/memberList', MemberListPage),
                                         ('/admin/memberEdit', MemberEditPage)],
                                        debug=True)

def main():
    run_wsgi_app(application)

if __name__ == "__main__":
    main()
