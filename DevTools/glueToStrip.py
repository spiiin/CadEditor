#Script for glue all png images inside folder to one strip file with name "strip.png"
#Uses python clr module and PIL library

import sys, os

cadEditorDir = os.path.abspath("../CadEditor/") + "/"
sys.path.append(cadEditorDir)

import clr
clr.AddReference("CadEditor")
clr.AddReference("System")

import System
from CadEditor import UtilsGDI

tiles = [System.Drawing.Bitmap.FromFile(x) for x in os.listdir(".") if x.endswith(".png")]
import PIL
d = UtilsGDI.GlueImages(tiles, len(tiles), 1)
d.Save("strip.png")