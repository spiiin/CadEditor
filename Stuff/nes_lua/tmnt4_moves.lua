--Script for make combos with one button
--Rom: TMNT Tournament Fighters NES (All versions)
--Author: spiiin

player1fighter = 0xA2;
direction = 0x07;
--player2fighter = 0xA3

comboNo = 1;

leoCombo = {
 --{ ["A"] = true},
 { ["down"] = true},
 { ["back"] = true, ["down"] = true },
 { ["back"] = true, ["A"] = true},
};

raphCombo1 = {
 { ["back"] = true},  --hold
 { ["forward"] = true, ["B"] = true},
};

raphCombo2 = {
 { ["back"] = true},  --hold
 { ["forward"] = true}, 
 { },
 { ["forward"] = true}, --start run
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true, ["A"] = true}, --jump kick from run
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true},
 { ["forward"] = true, ["B"] = true}, --start drill
};

mikeCombo1 = {
  { ["back"] = true, ["down"] = true }, --hold
  { ["forward"] = true, ["A"] = true }
}

mikeCombo2 = {
  { ["back"] = true, ["down"] = true }, --hold
  { ["forward"] = true, ["B"] = true }
}

donCombo = {
  { ["down"] = true},
  { },
  { ["up"] = true, ["A"] = true }
}

caseyCombo1 = {
  { ["down"] = true },
  { },
  { ["up"] = true, ["B"] = true }
}

caseyCombo2 = {
  { ["forward"] = true, ["down"] = true },
  { ["back"] = true, ["B"] = true},
}

hotheadCombo = {
  { ["down"]= true },
  { ["down"]= true, ["forward"] = true },
  { ["forward"] = true, ["A"] = true }
}

shredderCombo = {
  { ["forward"] = true },
  { ["forward"] = true, ["down"] = true },
  { ["down"] = true, ["B"] = true}
}

allCombos = {
  {leoCombo, leoCombo},
  {raphCombo1, raphCombo2},
  {mikeCombo1, mikeCombo2},
  {donCombo, donCombo},
  {caseyCombo1, caseyCombo2},
  {hotheadCombo, hotheadCombo},
  {shredderCombo, shredderCombo}
}

while true do
    local t = input.get()
    if t["J"] or t["U"] then
        startCombo = true;
        currentFrame = 1;
        if t["U"] then
            comboNo = 2;
        else
            comboNo = 1;
        end;
    end;
    
    if startCombo then
        local fighterNo = memory.readbyte(player1fighter)+1;
        local dir = memory.readbyte(direction);
        local combo = allCombos[fighterNo][comboNo];
        if currentFrame > #combo then
            startCombo = false;
        else
            local currentInput = combo[currentFrame]
            if dir == 0x00 then
                currentInput["left"] = currentInput["forward"];
                currentInput["right"] = currentInput["back"];
            else
                currentInput["left"] = currentInput["back"];
                currentInput["right"] = currentInput["forward"];
            end
            joypad.write(1, currentInput);
            currentFrame = currentFrame + 1;
        end;
    end;
    FCEU.frameadvance();
end;