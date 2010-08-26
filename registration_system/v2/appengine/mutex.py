from google.appengine.api.memcache import Client as mcClient
import time
import random

class Mutex:
    
    def __init__(self, key, maxTimeOut=1.5):
        self.key = key
        self.maxTimeOut = maxTimeOut
        self.locked = False
        
    def lock(self):
        if self.locked == True:
            return
        import datetime as dt
        mc = mcClient()
        mc.add(key=self.key + "@lockedAt", value=dt.datetime.now(), namespace="Mutex")
        v = mc.incr(self.key, namespace="Mutex", initial_value=1)
        while v != 1:
            mc.decr(self.key, namespace="Mutex")
            lastLockedAt = mc.get(self.key + "@lockedAt", namespace="Mutex")
            if lastLockedAt is None or \
                (dt.datetime.now() - lastLockedAt > dt.timedelta(seconds=self.maxTimeOut*4)):
                mc.set(key=self.key, value=0, namespace="Mutex")
            else:
                random.seed(time.time())
                time.sleep(random.random() * self.maxTimeOut)
            v = mc.incr(self.key, namespace="Mutex", initial_value=1)
        mc.set(key=self.key + "@lockedAt", value=dt.datetime.now(), namespace="Mutex")
        self.locked = True
    def unlock(self):
        if self.locked == True:
            mc = mcClient()
            mc.decr(self.key, namespace="Mutex")
            self.locked = False
    def __del__(self):
        if self.locked == True:
            self.unlock()
