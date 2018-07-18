v_9091 - minor_blocks_ptrs;
v_901b - major_blocks_ptrs;
v_8fa5 - maj_major_blocks_ptrs;
v_8f2f - maj_maj_major_blocks_ptrs;
v_8eb9 - room_ptrs

√де:
minor_blocks - блок из 2х2 тайла + 1 байт #id палитры
major_blocks_ptrs - блок из 2х2 minor_block
maj_major_blocks_ptrs - блок из 2х2 major_block
maj_maj_major_blocks_ptrs - блок из 2х1 maj_major_block
room - блок из 2x3 maj_maj_major_block

—оответственно:
room_ptrs: $8eb9 + 0xa010 = 0x12EC9
minor_blocks_ptrs: $9091 + 0xa010 = 0x130A1;
major_blocks_ptrs: $901b + 0xa010 = 0x1302B;
maj_major_blocks_ptrs: $8fa5 + 0xa010 = 0x12FB5;
maj_maj_major_blocks_ptrs: $8f2f + 0xa010 = 0x12F3F;

¬сего ( $8fa5 - $8f2f ) / 2 = 0x3b (59) актов

 аждый уровень состоит из какого-то количества актов, где свои палитра, блоки, номера CHR-банков и набор объектов.
------------------------------------------------------------------------------------------------------------------

$85a4 (0x85b4) - 4 byte config:
	1 byte - act id
	2 byte - exit room id (see $cb54 below)
	3 byte - begin - 0, continue - 1?
	4 byte - next act id

$c7c6 (0x1c7d6) - level direction (00 - horz, 80 - vert)
$c801 (0x1c811) - scroll data set #1
$c83c (0x1c84c) - scroll data set #2

$8509 (0x12519) - chr-banks set
$88ad (0x128bd) - chr-bank #1
$88ac (0x128bc) - chr-bank #2
$890d (0x1291d) - chr-bank #3
$890c (0x1291c) - chr-bank #4
$8544 (0x12554) - palettes pointers
$87a6 (0x127b6) - pointers to...?

$8349 (0x12359) - act rooms offset


$9af5 (0x9b05) - player position (lo nibble - X*16, hi nibble - Y*16)

$800c (0x1801c) - act sound (0x01-0x13 (except 0x12 - silence) - music, 0x14 etc - sounds)

$c33f (0x1c34f) - ptrs to acts scripts (for example: 1st ptr - does nothing, 2nd - players slides on the floor)

$9107 (0x130a1) - ptrs to minor blocks styles (such as 'space', 'rock', 'platform' etc)

$b43d (0x344d) - ptrs to acts objects.
Objects in 4 byte format:
	1 byte - room id,
	2 byte - position (lo nibble: x*16, hi nibble: y*16),
	3 byte - useful data (lo nibble: 1 or 3, hi nibble - ??),
	4 byte - object id

$cb54 (0x1cb64) - acts length (in rooms)

$84f3 (0x08503) - levels begin acts

$e909 + act_id (0x1e919 + act_id) - index of boss (#ff - if no boss in act)
$e944 + index_of_boss (0x1e954 + index_of_boss) - boss config 2 bytes:
	1 byte - boss room
	2 byte - boss id (00, 02, 04... - half bosses, 01, 03, 05 - level bosses)


ѕлюс игра в некоторых местах заскриптована. Ќапример, после второго по счету акта включаетс€ акт #2b, где поезд уезжает от игрока).

небольшое уточнение:
$8509 (0x12519) - chr-banks set - содержит индексы (0, 1, 2...)

дл€ массивов
$88ad (0x128bd) - chr-bank #1
$88ac (0x128bc) - chr-bank #2
$890d (0x1291d) - chr-bank #3
$890c (0x1291c) - chr-bank #4


“.е., к примеру, дл€ 1-го акта банки будут такие:
chr1 = $88ad[ $8509[ 1 ] ];
chr2 = $88ac[ $8509[ 1 ] ];
chr3 = $880d[ $8509[ 1 ] ];
chr4 = $880c[ $8509[ 1 ] ];

=====================================================================
Search 2x2 blocks, left-right
1314E (10 blocks around)
rom[0x130A1] = 9107 -> 13117 - начало маленьких блоков.
rom[0x1302B] = 9373 -> 13383 - начало больших блоков.
rom[0x12fb5] = 9537 -> 13547 - начало больших больших блоков.
rom[0x13F3F] = 96D3 -> 136E3 - начало больших больших больших блоков.
rom[0x12EC9] = 9769 -> 13779 - начало экранов.

дл€ перевода поинтера в адрес ром:
>04:8DFD:B1 0E     LDA ($0E),Y @ $A167 = #$05 , и посмотреть, какой банк по этому адресу
номер акта:
RAM 0x480