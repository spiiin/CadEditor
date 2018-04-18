import sys
from PIL import Image, ImageDraw

images = [Image.open('%s.png'%hex(i)[2:]) for i in range(16)]

for i in range(16):
  for j in range(16):
    for im in images[:2]:
      istr = hex(i)[2:]
      jstr = hex(j)[2:]
      new_im = Image.new('RGB', (8, 8))
      new_im.paste(images[i], (0,0))
      new_im.paste(images[j], (4,0))
      d = ImageDraw.Draw(new_im)
      d.rectangle([-1,0,7,7], outline="red")
      new_im.save('%s%s.png'%(istr,jstr))