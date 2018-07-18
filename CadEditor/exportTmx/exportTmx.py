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
    
    blockWidth = int(formMain.bigBlocks[0].Width * curScale);
    blockHeight = int(formMain.bigBlocks[0].Height * curScale);
    
    scrNo = calcScrNo(layout, 0)
    width = formMain.screens[scrNo].width;
    height = formMain.screens[scrNo].height;
    if ConfigScript.getScreenVertical():
        mapSize = (layout.width * height, layout.height * width)
    else:
        mapSize = (layout.width * width, layout.height * height)
    
    tileSize = (blockWidth, blockHeight)
    
    #generate tiles
    #TODO: rescale it to curScale
    bigBlocksImages = formMain.bigBlocks
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
            if scrNo >= 0 and scrNo < len(formMain.screens):
                curScreen = formMain.screens[scrNo].layers[0].data
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