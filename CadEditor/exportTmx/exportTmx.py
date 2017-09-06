import tmxlib
import clr
import os
from math import ceil

clr.AddReference("CadEditor")
from CadEditor import ConfigScript, UtilsGDI

def calcScrNo(layout, noInLayout):
    return layout.layer[noInLayout] - 1

def export(filename, formMain, curActiveLayout):
    path, fname = os.path.split(filename)
    fnameWithoutExt, fnameExt = os.path.splitext(fname)
    
    layout = ConfigScript.getLayout(curActiveLayout)
    levelRec = ConfigScript.getLevelRec(curActiveLayout)
    curScale = 1#formMain.CurScale
    
    blockWidth = int(formMain.Layers[0].blockWidth * curScale);
    blockHeight = int(formMain.Layers[0].blockHeight * curScale);
    scrLevelNo = levelRec.levelNo;
    
    width = ConfigScript.getScreenWidth(scrLevelNo);
    height = ConfigScript.getScreenHeight(scrLevelNo);
    if ConfigScript.getScreenVertical():
        mapSize = (layout.width * height, layout.height * width)
    else:
        mapSize = (layout.width * width, layout.height * height)
    
    tileSize = (blockWidth, blockHeight)
    
    #generate tiles
    #TODO: rescale it to curScale
    bigBlocksImages = formMain.BigBlocks
    ImWidthInBlocks = 16
    ImHeightInBlocks = int(ceil(len(bigBlocksImages)*1.0/ImWidthInBlocks))
    bigBlockImage = UtilsGDI.GlueImages(bigBlocksImages, ImWidthInBlocks, ImHeightInBlocks)
    #bigBlockImage = UtilsGDI.GlueImages(bigBlocksImages, len(bigBlocksImages), 1)
    bigBlockImage.Save(os.path.join(path, fnameWithoutExt + ".png"))
    
    map = tmxlib.Map(mapSize, tileSize , base_path=".")
    im = tmxlib.image.open(fnameWithoutExt + ".png", base_path=path)
    tileset = tmxlib.ImageTileset("Tiles", tileSize, im, base_path = ".")
    map.tilesets.append(tileset)
    layer = map.add_layer("Layer1")
    for sy in xrange(layout.height):
        for sx in xrange(layout.width):
            scrIndex = sy * layout.width + sx
            scrNo = calcScrNo(layout, scrIndex)
            if scrNo >= 0 and scrNo < len(formMain.Layers[0].screens):
                curScreen = formMain.Layers[0].screens[scrNo]
                for y in xrange(height):
                    for x in xrange(width):
                        if ConfigScript.getScreenVertical():
                            cx, cy = y, x
                        else:
                            cx, cy = x, y
                        index = y * width + x
                        tileNo =  ConfigScript.getBigTileNoFromScreen(curScreen, index)
                        lx = sx * width + cx
                        ly = sy * height + cy
                        layer[lx,ly] = map.tilesets["Tiles"][tileNo]
    map.save(filename)
    return True