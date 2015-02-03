import os, glob, shutil, zipfile

VERSION        = "33"
RELEASE_FOLDER = "../Release/cad_editor_v%s" % VERSION
ZIP_NAME       = "../Release/cad_editor_v%s.zip" % VERSION

COPY_FILE_LIST = [
  "CadEditor.exe",
  "Be.Windows.Forms.HexBox.dll",
  "CSScriptLibrary.v3.5.dll",
  "Newtonsoft.Json.dll",
  
  "Config.cs",
  "Settings_CapcomBase.cs",
  "Settings_Mermaid-Utils.cs",
  "Settings_TinyToon-Utils.cs",
  "Settings_Flintstones-Utils.cs",
  
  "readme.txt",
  "cad_editor_configs_manual.txt",
  "cad_editor_supported_games.txt",
  "cad_editor_structures.txt",
  
  "Plugin*.dll"
]

COPY_FOLDER_LIST = [
  "obj_sprites/",
  "scroll_sprites/",
  "settings_*/",
]

def removeAndCreate(path):
  if os.path.exists(path):
    print "Release folder already exists. Removing..."
    shutil.rmtree(path)
  print "Create release folder..."
  os.makedirs(path)
  
def zipdir(path, zip):
  for root, dirs, files in os.walk(path):
    for file in files:
      fp = os.path.join(root, file)
      #fp = fp.remove(ROOT_SUBPATH)
      zip.write(fp)

def makeRelease():
  print "Make release CadEditor v" + VERSION
  absRoot = os.path.abspath(RELEASE_FOLDER)
  print "Release folder: ", absRoot
  removeAndCreate(absRoot)
  print "Copying files:"
  for fn in COPY_FILE_LIST:
    if fn.find("*")==-1:
      print "  copy ", fn
      shutil.copy(fn, absRoot)
    else:
      fileList2 = glob.glob(fn)
      for fn2 in fileList2:
        print "  copy ", fn2
        shutil.copy(fn2, absRoot)
  print "Copying folders:"
  for dir in COPY_FOLDER_LIST:
    if dir.find("*") == -1:
      print "  copy ", dir
      shutil.copytree(dir, os.path.join(absRoot, dir))
    else:
      dirList2 = glob.glob(dir)
      for dir2 in dirList2:
        print "  copy ", dir2
        shutil.copytree(dir2, os.path.join(absRoot, dir2))
  print ""
  
  #print "Making release archive"
  #zip = zipfile.ZipFile(ZIP_NAME, 'w')
  #zipdir(absRoot, zip)
  #zip.close()
  print "DONE!"
    
#-----------------------------------------------------------------------
if __name__ == "__main__":
  makeRelease()
      
  