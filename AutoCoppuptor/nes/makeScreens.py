import subprocess
import win32ui
import win32gui
import win32con
import time
import os.path

DIRECT_ROM_NAME = "ROM_NAME"


def takeScreen(hwnd, scrname):
  wDC = win32gui.GetWindowDC(hwnd)
  dcObj=win32ui.CreateDCFromHandle(wDC)
  cDC=dcObj.CreateCompatibleDC()
  dataBitMap = win32ui.CreateBitmap()
  dataBitMap.CreateCompatibleBitmap(dcObj, 250, 210) #~=
  cDC.SelectObject(dataBitMap)
  cDC.BitBlt((0,0),(250, 210) , dcObj, (10,40), win32con.SRCCOPY) #~=
  dataBitMap.SaveBitmapFile(cDC, scrname)
  
def doWork(romname, savename, screenname, no):
  print 'fceux -loadstate %s %s'%(savename, romname)
  p = subprocess.Popen('fceux -lua script.lua -turbo 1 -loadstate %s %s'%(savename, romname)) #start game
  screenReady = False
  while not screenReady:
    screenReady = os.path.exists("snaps/"+DIRECT_ROM_NAME+"%04d-0.png"%no)
    time.sleep(0.01)                                                          #wait for game loaded
  hwnd = win32gui.FindWindow(None, "FCEUX 2.2.0: %s"%DIRECT_ROM_NAME)         #find window
  #takeScreen(hwnd, screenname)                                               #take screenshot
  p.kill()                                                                    #kill proccess

for x in xrange(256):
#for x in xrange(1200, 1400, 2):
  doWork('"rom/'+DIRECT_ROM_NAME+'%04d.nes"'%x,'"'+DIRECT_ROM_NAME+'.fc1"', "scr/%04d.bmp"%x, x)

