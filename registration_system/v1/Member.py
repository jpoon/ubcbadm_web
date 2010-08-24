import hashlib
import logging
from google.appengine.ext import db

class Member(db.Model):
    maxMembers = 150

    createDate = db.DateTimeProperty(auto_now_add=True)
    firstName = db.StringProperty(required=True)
    lastName = db.StringProperty(required=True)
    ubcAffliation = db.StringProperty(required=True, choices=set(["Student", "Faculty", "Other (Non-AMS)"]))
    studentNo = db.IntegerProperty()
    phoneNumber = db.PhoneNumberProperty(required=True)
    email = db.EmailProperty(required=True)
    memberType = db.StringProperty(required=True, choices=set(["New", "Returning", "Non-AMS"]))
    level = db.StringProperty(required=True, choices=set(["Beginner", "Intermediate I", "Intermediate II", "Advanced"]))
    memberNo = db.IntegerProperty()
    emailHash = db.StringProperty()

    def Create(self):
        hash = hashlib.md5()
        hash.update(self.email)
        self.emailHash = hash.hexdigest()[:10]
        self.put()
        logging.info('Creating member with id %i' % self.memberNo)

    @staticmethod
    def getAllEmails():
        memberList = Member.gql("WHERE emailVerified = TRUE")

        separator = ", "
        emailList = ""

        for member in memberList:
            emailList += member.email
            emailList += separator
        return emailList
                

    @staticmethod
    def verifyEmail(hash):
        query = Member.gql("WHERE emailHash= :hash", hash=hash)
        if query.count() == 1:
            member = query.get()
            member.emailVerified = True
            member.put()
            return member
        else:
            logging.info('Unable to verify hash %s' % hash)
            return None

    @staticmethod
    def getMember(emailHash):
        query = Member.gql("WHERE emailHash = :hash", hash=emailHash)
        if query.count() == 1:
            return query.get()
        else:
            return None

    @staticmethod
    def getMemberList(sortMethod):
        memberList = Member.all()

        try:
            memberList.order(sortMethod)
        except:
            memberList.order('memberNo')

        return memberList

    @staticmethod
    def nextAvailableMemberNo():
        # Bug: If an entry is deleted, there will be duplicate membership numbers
        memberList = Member.all()
        return memberList.count() + 1;

    @staticmethod
    def isFull():
        memberList = Member.all()
        if memberList.count() < Member.maxMembers:
            return False
        else:
            logging.warn("Membership Full")
            return True

    @staticmethod
    def isValidEmail(email):
        query = Member.gql("WHERE email= :email", email=email)
        if query.count() > 0:
            return False
        else:
            return True

    @staticmethod
    def isValidStudentNo(number):
        query = Member.gql("WHERE studentNo= :number", number=number)
        if query.count() > 0:
            return False
        else:
            return True
