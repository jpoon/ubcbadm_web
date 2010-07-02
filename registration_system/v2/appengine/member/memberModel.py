import hashlib
import logging
from google.appengine.ext import db

class MemberModel(db.Model):
    createDate = db.DateTimeProperty(auto_now_add=True)
    firstName = db.StringProperty(required=True)
    lastName = db.StringProperty(required=True)
    affiliation = db.StringProperty(required=True)
    studentNo = db.StringProperty()
    phoneNumber = db.PhoneNumberProperty(required=True)
    email = db.EmailProperty(required=True)
    memberType = db.StringProperty(required=True)
    skillLevel = db.StringProperty(required=True)

class Member:
    MAX_MEMBERS = 150

    def __init__(self, dict):
        for key in dict.keys():
            setattr(self, key, dict[key])

    def persist(self):
        if not Member.isFull:
            member = MemberModel(firstName = self.firstName,
                                 lastName = self.lastName,
                                 affiliation = self.affiliation,
                                 studentNo = self.studentNo,
                                 phoneNumber = self.phoneNumber,
                                 email = self.email,
                                 memberType = self.memberType,
                                 skillLevel = self.skillLevel)
                                
            member.put()
            logging.info('Creating member %s %s' % (member.firstName, member.lastName))

    @staticmethod
    def get(memberNo=None):
        if memberNo is None:
            return MemberModel.all()
        else:
            query = MemberModel.gql("WHERE memberNo = :num", num=memberNo)
            if query.count() == 1:
                return query.get()
            else:
                return None

    @staticmethod
    def isFull():
        memberList = MemberModel.all()
        if memberList.count() < Member.MAX_MEMBERS:
            return False
        else:
            logging.warn("Membership Full")
            return True

    @staticmethod
    def isValidEmail(email):
        query = MemberModel.gql("WHERE email= :email", email=email)
        if query.count() > 0:
            return False
        else:
            return True

    @staticmethod
    def isValidStudentNo(number):
        query = MemberModel.gql("WHERE studentNo= :number", number=number)
        if query.count() > 0:
            return False
        else:
            return True
