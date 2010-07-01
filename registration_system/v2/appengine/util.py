import logging
import datetime  
import time 
from google.appengine.api import mail
from google.appengine.api import users 
from google.appengine.ext import db 
from django.utils import simplejson

class GqlEncoder(simplejson.JSONEncoder): 

    """Extends JSONEncoder to add support for GQL results and properties. 
    Adds support to simplejson JSONEncoders for GQL results and properties by 
    overriding JSONEncoder's default method. 
    """ 

    # TODO Improve coverage for all of App Engine's Property types. 
    def default(self, obj): 
        """Tests the input object, obj, to encode as JSON.""" 

        if hasattr(obj, '__json__'): 
            return getattr(obj, '__json__')() 

        if isinstance(obj, db.GqlQuery): 
            return list(obj) 

        elif isinstance(obj, db.Model): 
            properties = obj.properties().items() 
            output = {} 
            for field, value in properties: 
                output[field] = getattr(obj, field) 
            return output 

        elif isinstance(obj, datetime.datetime): 
            output = {} 
            fields = ['day', 'hour', 'microsecond', 'minute', 'month', 'second', 'year'] 
            methods = ['ctime', 'isocalendar', 'isoformat', 'isoweekday', 'timetuple'] 
            for field in fields: 
                output[field] = getattr(obj, field) 
            for method in methods: 
                output[method] = getattr(obj, method)() 
            output['epoch'] = time.mktime(obj.timetuple()) 
            return output 

        elif isinstance(obj, time.struct_time): 
            return list(obj) 

        elif isinstance(obj, users.User): 
            output = {} 
            methods = ['nickname', 'email', 'auth_domain'] 
            for method in methods: 
                output[method] = getattr(obj, method)() 
            return output 

        return simplejson.JSONEncoder.default(self, obj) 

def send_mail(fromAddr, toAddr, subject, body):
    signature = '<br \><br \>' \
                '--<br \>' \
                'UBC Badminton Club Executive Team<br \>' \
                'Web: www.ams.ubc.ca/clubs/badminton<br \>' \
                'Email: ubc.badm@gmail.com<br \>' \
                'Office: SUB - Room 95G'
    msg = mail.EmailMessage()
    msg.sender = fromAddr
    msg.subject = '[UBC Badm] ' + subject
    msg.to = toAddr
    msg.html = body + signature

    try:
        logging.info('Sending email to %s' % msg.to)
        msg.check_initialized()
        msg.send()
    except mail.InvalidEmailError:
        logging.error('Invalid email address: (recipient %s) (sender %s)' % toEmail  % sender)
    except mail.MissingRecipientsError:
        logging.error('Missing email recipients')
    except mail.MissingSubjectError:
        logging.error('Missing email subject')
    except mail.MissingBodyError:
        logging.error('Missing body')
    except mail.InvalidSenderError:
        logging.error('Invalid sender email')
    except mail.InvalidSenderError:
        logging.error('Error sending email')

