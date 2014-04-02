nes_opcodes = [
  ("BRK             ",1),       #0
  ("ORA ($%X,X)     ",2),       #1
  ("XXX             ",1),       #2
  ("XXX             ",1),       #3
  ("XXX             ",1),       #4
  ("ORA $%X         ",2),       #5
  ("ASL $%X         ",2),       #6
  ("XXX             ",1),       #7
  ("PHP             ",1),       #8
  ("ORA #$%X        ",2),       #9
  ("ASL A           ",1),       #A
  ("XXX             ",1),       #B
  ("XXX             ",1),       #C
  ("ORA $%X%X       ",3),       #D
  ("ASL $%X%X       ",3),       #E
  ("XXX             ",1),       #F
  ("XXX             ",1),       #10
  ("ORA ($%X),Y     ",2),       #11
  ("XXX             ",1),       #12
  ("XXX             ",1),       #13
  ("XXX             ",1),       #14
  ("ORA $%X,X       ",2),       #15
  ("ASL $%X,X       ",2),       #16
  ("XXX             ",1),       #17
  ("CLC             ",1),       #18
  ("ORA $%X%X,Y     ",3),       #19
  ("XXX             ",1),       #1A
  ("XXX             ",1),       #1B
  ("XXX             ",1),       #1C
  ("ORA $%X%X,X     ",3),       #1D
  ("ASL $%X%X,X     ",3),       #1E
  ("XXX             ",1),       #1F
  ("JSR $%X%X       ",3),       #20
  ("AND ($%X,X)     ",2),       #21
  ("XXX             ",1),       #22
  ("XXX             ",1),       #23
  ("BIT $%X         ",2),       #24
  ("AND $%X         ",2),       #25
  ("ROL $%X         ",2),       #26
  ("XXX             ",1),       #27
  ("PLP             ",1),       #28
  ("AND #$%X        ",2),       #29
  ("ROL A           ",1),       #2A
  ("XXX             ",1),       #2B
  ("BIT $%X%X       ",3),       #2C
  ("AND $%X%X       ",3),       #2D
  ("ROL $%X%X       ",3),       #2E
  ("XXX             ",1),       #2F
  ("BMI addr+%X     ",2),       #30
  ("AND ($%X),Y     ",2),       #31
  ("XXX             ",1),       #32
  ("XXX             ",1),       #33
  ("XXX             ",1),       #34
  ("AND $%X,X       ",2),       #35
  ("ROL $%X,X       ",2),       #36
  ("XXX             ",1),       #37
  ("SEC             ",1),       #38
  ("AND $%X%X,Y     ",3),       #39
  ("LUA #%X         ",2),       #3A
  ("XXX             ",1),       #3B
  ("XXX             ",1),       #3C
  ("AND $%X%X,X     ",3),       #3D
  ("ROL $%X%X,X     ",3),       #3E
  ("XXX             ",1),       #3F
  ("RTI             ",1),       #40
  ("EOR ($%X,X)     ",2),       #41
  ("XXX             ",1),       #42
  ("XXX             ",1),       #43
  ("XXX             ",1),       #44
  ("EOR $%X         ",2),       #45
  ("LSR $%X         ",2),       #46
  ("XXX             ",1),       #47
  ("PHA             ",1),       #48
  ("EOR #$%X        ",2),       #49
  ("LSR A           ",1),       #4A
  ("XXX             ",1),       #4B
  ("JMP $%X%X       ",3),       #4C
  ("EOR $%X%X       ",3),       #4D
  ("LSR $%X%X       ",3),       #4E
  ("XXX             ",1),       #4F
  ("BVC addr+%X     ",2),       #50
  ("EOR ($%X),Y     ",2),       #51
  ("XXX             ",1),       #52
  ("XXX             ",1),       #53
  ("XXX             ",1),       #54
  ("EOR $%X,X       ",2),       #55
  ("LSR $%X,X       ",2),       #56
  ("XXX             ",1),       #57
  ("CLI             ",1),       #58
  ("EOR $%X%X,Y     ",3),       #59
  ("XXX             ",1),       #5A
  ("XXX             ",1),       #5B
  ("XXX             ",1),       #5C
  ("EOR $%X%X,X     ",3),       #5D
  ("LSR $%X%X,X     ",3),       #5E
  ("XXX             ",1),       #5F
  ("RTS             ",1),       #60
  ("ADC ($%X,X)     ",2),       #61
  ("XXX             ",1),       #62
  ("XXX             ",1),       #63
  ("XXX             ",1),       #64
  ("ADC $%X         ",2),       #65
  ("ROR $%X         ",2),       #66
  ("XXX             ",1),       #67
  ("PLA             ",1),       #68
  ("ADC #$%X        ",2),       #69
  ("ROR A           ",1),       #6A
  ("XXX             ",1),       #6B
  ("JMP ($%X%X)     ",3),       #6C
  ("ADC $%X%X       ",3),       #6D
  ("ROR $%X%X       ",3),       #6E
  ("XXX             ",1),       #6F
  ("BVS addr+%X     ",2),       #70
  ("ADC ($%X),Y     ",2),       #71
  ("XXX             ",1),       #72
  ("XXX             ",1),       #73
  ("XXX             ",1),       #74
  ("ADC $%X,X       ",2),       #75
  ("ROR $%X,X       ",2),       #76
  ("XXX             ",1),       #77
  ("SEI             ",1),       #78
  ("ADC $%X%X,Y     ",3),       #79
  ("XXX             ",1),       #7A
  ("XXX             ",1),       #7B
  ("XXX             ",1),       #7C
  ("ADC $%X%X,X     ",3),       #7D
  ("ROR $%X%X,X     ",3),       #7E
  ("XXX             ",1),       #7F
  ("XXX             ",1),       #80
  ("STA ($%X,X)     ",2),       #81
  ("XXX             ",1),       #82
  ("XXX             ",1),       #83
  ("STY $%X         ",2),       #84
  ("STA $%X         ",2),       #85
  ("STX $%X         ",2),       #86
  ("XXX             ",1),       #87
  ("DEY             ",1),       #88
  ("XXX             ",1),       #89
  ("TXA             ",1),       #8A
  ("XXX             ",1),       #8B
  ("STY $%X%X       ",3),       #8C
  ("STA $%X%X       ",3),       #8D
  ("STX $%X%X       ",3),       #8E
  ("XXX             ",1),       #8F
  ("BCC addr+%X     ",2),       #90
  ("STA ($%X),Y     ",2),       #91
  ("XXX             ",1),       #92
  ("XXX             ",1),       #93
  ("STY $%X,X       ",2),       #94
  ("STA $%X,X       ",2),       #95
  ("STX $%X,Y       ",2),       #96
  ("XXX             ",1),       #97
  ("TYA             ",1),       #98
  ("STA $%X%X,Y     ",3),       #99
  ("TXS             ",1),       #9A
  ("XXX             ",1),       #9B
  ("XXX             ",1),       #9C
  ("STA $%X%X,X     ",3),       #9D
  ("XXX             ",1),       #9E
  ("XXX             ",1),       #9F
  ("LDY #$%X        ",2),       #A0
  ("LDA ($%X,X)     ",2),       #A1
  ("LDX #$%X        ",2),       #A2
  ("XXX             ",1),       #A3
  ("LDY $%X         ",2),       #A4
  ("LDA $%X         ",2),       #A5
  ("LDX $%X         ",2),       #A6
  ("XXX             ",1),       #A7
  ("TAY             ",1),       #A8
  ("LDA #$%X        ",2),       #A9
  ("TAX             ",1),       #AA
  ("XXX             ",1),       #AB
  ("LDY $%X%X       ",3),       #AC
  ("LDA $%X%X       ",3),       #AD
  ("LDX $%X%X       ",3),       #AE
  ("XXX             ",1),       #AF
  ("BVC addr+%X     ",2),       #B0
  ("LDA ($%X),Y     ",2),       #B1
  ("XXX             ",1),       #B2
  ("XXX             ",1),       #B3
  ("LDY $%X,X       ",2),       #B4
  ("LDA $%X,X       ",2),       #B5
  ("LDX $%X,Y       ",2),       #B6
  ("XXX             ",1),       #B7
  ("CLV             ",1),       #B8
  ("LDA $%X%X,Y     ",3),       #B9
  ("TSX             ",1),       #BA
  ("XXX             ",1),       #BB
  ("LDY $%X%X,X     ",3),       #BC
  ("LDA $%X%X,X     ",3),       #BD
  ("LDX $%X%X,Y     ",3),       #BE
  ("XXX             ",1),       #BF
  ("CPY #$%X        ",2),       #C0
  ("CMP ($%X,X)     ",2),       #C1
  ("XXX             ",1),       #C2
  ("XXX             ",1),       #C3
  ("CPY $%X         ",2),       #C4
  ("CMP $%X         ",2),       #C5
  ("DEC $%X         ",2),       #C6
  ("XXX             ",1),       #C7
  ("INY             ",1),       #C8
  ("CMP #$%X        ",2),       #C9
  ("XXX             ",1),       #CA
  ("XXX             ",1),       #CB
  ("CPY $%X%X       ",3),       #CC
  ("CMP $%X%X       ",3),       #CD
  ("DEC $%X%X       ",3),       #CE
  ("XXX             ",1),       #CF
  ("BVS addr+%X     ",2),       #D0
  ("CMP ($%X),Y     ",2),       #D1
  ("XXX             ",1),       #D2
  ("XXX             ",1),       #D3
  ("XXX             ",1),       #D4
  ("CMP $%X,X       ",2),       #D5
  ("DEC $%X,X       ",2),       #D6
  ("XXX             ",1),       #D7
  ("CLD             ",1),       #D8
  ("CMP $%X%X,Y     ",3),       #D9
  ("XXX             ",1),       #DA
  ("XXX             ",1),       #DB
  ("XXX             ",1),       #DC
  ("CMP $%X%X,X     ",3),       #DD
  ("DEC $%X%X,X     ",3),       #DE
  ("XXX             ",1),       #DF
  ("CPX #$%X        ",2),       #E0
  ("SBC ($%X,X)     ",2),       #E1
  ("XXX             ",1),       #E2
  ("XXX             ",1),       #E3
  ("CPX $%X         ",2),       #E4
  ("SBC $%X         ",2),       #E5
  ("INC $%X         ",2),       #E6
  ("XXX             ",1),       #E7
  ("INX             ",1),       #E8
  ("SBC #$%X        ",2),       #E9
  ("DEX             ",1),       #EA
  ("XXX             ",1),       #EB
  ("CPX $%X%X       ",3),       #EC
  ("SBC $%X%X       ",3),       #ED
  ("INC $%X%X       ",3),       #EE
  ("XXX             ",1),       #EF
  ("BCC addr+%X     ",2),       #F0
  ("SBC ($%X),Y     ",2),       #F1
  ("XXX             ",1),       #F2
  ("XXX             ",1),       #F3
  ("XXX             ",1),       #F4
  ("SBC $%X,X       ",2),       #F5
  ("INC $%X,X       ",2),       #F6
  ("XXX             ",1),       #F7
  ("SED             ",1),       #F8
  ("SBC $%X%X,Y     ",3),       #F9
  ("XXX             ",1),       #FA
  ("XXX             ",1),       #FB
  ("XXX             ",1),       #FC
  ("SBC $%X%X,X     ",3),       #FD
  ("INC $%X%X,X     ",3),       #FE
  ("XXX             ",1),       #FF
]