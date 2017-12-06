import os, glob, shutil, zipfile, path

def applyToAllFilesInFolder ( folder, fileexts, func, *params):
  """apply function with params func to all files with extenstions from list fileext in folder
     example:
       applyToAllFilesInFolder ("C:", [".txt"], lambda filename:None)
  """
  def visit ( arg, dirname, names ):
    for name in names:
      shortName, ext = os.path.splitext(name)
      if ext.lower() in fileexts:
        func(os.path.join(dirname, name), *params)
  os.path.walk( folder, visit, 0)


FILE_LIST = [
  "Settings_CapcomBase.cs",
  "Settings_Megaman3Base.cs",
  "Settings_Mermaid-Utils.cs",
  "Settings_TinyToon-Utils.cs",
  "Settings_Flintstones-Utils.cs",
  "Settings_CHC-Utils.cs",  
]

FOLDER_LIST = [
  "settings_*/",
]

def func(filename):
    if filename.lower().find("settings_jungle")==-1:
      return
    lines = []
    with open(filename, "rt") as f:
      lines = f.readlines()
    ans = []
    for l in lines:
        #conditions
        pass
        #simple append
        ans.append(l)
    with open(filename("wt") as f:
        f.writelines(ans)

def makeWork():
  print "Replace strings in all configs"
  applyToAllFilesInFolder(".", [".cs"], func)
  print "DONE!"
    
#-----------------------------------------------------------------------
if __name__ == "__main__":
  makeWork()
      
  