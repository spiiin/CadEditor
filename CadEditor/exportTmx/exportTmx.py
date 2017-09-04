import tmxlib
import clr
from math import ceil

clr.AddReference("CadEditor")
from CadEditor import ConfigScript, UtilsGDI

def calcScrNo(layout, noInLayout):
    return layout.layer[noInLayout] - 1

def export(formMain, curActiveLayout):  
    layout = ConfigScript.getLayout(curActiveLayout)
    levelRec = ConfigScript.getLevelRec(curActiveLayout)
    curScale = formMain.CurScale
    
    blockWidth = int(formMain.Layers[0].blockWidth * curScale);
    blockHeight = int(formMain.Layers[0].blockHeight * curScale);
    scrLevelNo = levelRec.levelNo;
    
    width = ConfigScript.getScreenWidth(scrLevelNo);
    height = ConfigScript.getScreenHeight(scrLevelNo);
    
    tileSize = (blockWidth, blockHeight)
    
    #generate tiles
    bigBlocksImages = formMain.BigBlocks
    ImWidthInBlocks = 16
    ImHeightInBlocks = int(ceil(len(bigBlocksImages)*1.0/ImWidthInBlocks))
    bigBlockImage = UtilsGDI.GlueImages(bigBlocksImages, ImWidthInBlocks, ImHeightInBlocks)
    bigBlockImage.Save("exportTmx/tiles.png")
    
    map = tmxlib.Map((layout.width * width, layout.height * height), tileSize , base_path=".")
    im = tmxlib.image.open("tiles.png", base_path="exportTmx")
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
                        index = y * width + x
                        tileNo = curScreen[index]
                        lx = sx * width + x
                        ly = sy * height + y
                        layer[lx,ly] = map.tilesets["Tiles"][tileNo]
    map.save('exportTmx/export.tmx')
    return True