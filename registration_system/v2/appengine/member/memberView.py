import os
import cgi
import logging
import itertools 
import hashlib
import util
from member.memberModel import *
from django.utils import simplejson
from google.appengine.ext import webapp
from google.appengine.ext.webapp import template
from google.appengine.ext.webapp.util import run_wsgi_app

class MemberView(webapp.RequestHandler):
    def get(self):
        memberList = Member.get()
            
        list = []
        for member in memberList:
            list.append(member)

        self.response.headers['Content-type'] = 'text/javascript; charset=utf-8'
        self.response.out.write(util.GqlEncoder().encode(list))

    def post(self):
        # Hack: Ideally would like to return a non 200 status code
        # however, silverlight client unable to read status code message
        self.response.set_status(200)

        if Member.isFull():
           self.response.out.write("Membership Full")
           return

        try:
            data = simplejson.loads(self.request.body)
            member = Member(data)
            member.persist()
        except Exception, e:
            logging.error(e)
            self.response.out.write(type(e))
        else:
            self.response.out.write(member.memberNo)

            fromAddr = 'UBC Badminton Club <ubc.badm@gmail.com>'
            msgBody = member.firstName + ' ' + member.lastName + ', \n\n' \
                  '<p>Welcome to the UBC Badminton Club! ' \
                  'Your membership number is <b>' + str(member.memberNo) + '</b>. ' \
                  'In order to stay up-to-date with the latest club news, ' \
                  'we recommend you either subscribe to our email newsletter, RSS feed, or follow us on Twitter.</p>' \
                  '<table>' \
                        '<tr>' \
                            '<td>Email Newsletter:</td>' \
                            '<td><a href="http://feedburner.google.com/fb/a/mailverify?uri=UBCBadmintonClub&amp;loc=en_US">subscribe</a></td>' \
                        '</tr>' \
                        '<tr>' \
                            '<td>RSS:</td>' \
                            '<td><a href="http://ubcbadm.vlexofree.com/feed/">subscribe</a></td>' \
                        '</tr>' \
                        '<tr>' \
                            '<td>Twitter:</td>' \
                            '<td><a href="http://twitter.com/ubcbadm">http://twitter.com/ubcbadm</a></td>' \
                        '</tr>' \
                  '</table>'

            util.send_mail(fromAddr,
                  member.email, 
                  '[UBC Badm] Welcome ' + member.firstName + '!',
                  msgBody)

application = webapp.WSGIApplication(   [('/member', MemberView)],
                                        debug=True)

def main():
    run_wsgi_app(application)

if __name__ == "__main__":
    main()
