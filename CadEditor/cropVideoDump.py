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
  
def crop(fname):
  with open(fname, "rb") as f:
    d = f.read()
  with open(fname, "wb") as f:
    f.write(d[:0x1000])
    
applyToAllFilesInFolder(".", [".bin"], crop)