local notSave = true;
while (notSave) do
  --gui.text(80,5,""..emu.framecount());
  if emu.framecount() > 3000 then
    gui.savescreenshot();
    notSave = false;
  end;
  FCEU.frameadvance();
end;