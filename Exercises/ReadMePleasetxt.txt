race condition
deadlock
memoryleask
stackoverflow
bufferoverflow
webfarm
webgarden 
mutex / monitor
crud same table :  webapp/iis, webapi/iis, wcf/iis, services, localapp  
crud same table :  webapp/iis, webapi/iis, wcf/iis, services, localapp 

signle thread
  sync  thread1 task1-> task2-> task3
  async  thread1 task2-> task1-> task3

multi thread
sync  
 thread1 task1-> task2-> task3
 thread2 task1-> task2-> task3
async  
 thread1 task1-> task3-> task2
 thread2 task3-> task2-> task1

  
Multi-threading : more thread

Asynchronous
sync : end wait task one by one  
async : dont wait end task


iis request queue -->clr trheadpool--->porcessing request
