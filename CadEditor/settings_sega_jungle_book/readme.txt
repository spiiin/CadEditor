Jungle Book [SMD]

В игре используется обычная блочная система - карты уровней составлены из блоков размером 2x2.

Карты переднего и заднего слоёв пожаты с помощью алгоритма RNC, скрипт извлечения архивов и парсинга конфигов уровней (для поиска соотвествия адресов массивов и номеров уровней) находится в директории JupyterCadEditor\RNC\ExtractRNC.ipynb (Jupyter-блокнот). 

Объекты зашифрованы в отдельном слое и также пожаты RNC (кроме уровня 4, в нём карта уровня не сжата совсем), архив с ними чуть меньше архива с картой, из-за того, что в нём выброшены завершающие нули. Для наложения слоя объектов на карту необходимо добавить в конец распакованного массива объектов необходимое количество нулей.

Результат работы скрипта:
Config for level 0
0x1a17ec
  Map: 0x122a26L
  Map size  : 72 x 18
  LayerB    : 0x123136
  Pal       : 0xe94ee
  Tilemap   : 0x123a5e
  Enemy       : 0x1239cc
Config for level 1
0x1a1830
  Map: 0x12502aL
  Map size  : 194 x 42
  LayerB    : 0x127ad4
  Pal       : 0xe958e
  Tilemap   : 0x128842
  Enemy       : 0x128360
Config for level 2
0x1a1874
  Map: 0x1302f6L
  Map size  : 53 x 165
  LayerB    : 0x132c90
  Pal       : 0xe962e
  Tilemap   : 0x13373e
  Enemy       : 0x133114
Config for level 3
0x1a18b8
  Map: 0x13afc2L
  Map size  : 339 x 30
  LayerB    : 0x13d278
  Pal       : 0xe96ce
  Tilemap   : 0x13da46
  Enemy       : 0x13d6a0
Config for level 4
0x1a18fc
  Map: 0x146b08
  Map size  : 336 x 39
  LayerB    : 0x14d1c0
  Pal       : 0xe974e
  Tilemap   : 0x14de94
  Enemy       : 0x14da50
Config for level 5
0x1a1940
  Map: 0x1554b0L
  Map size  : 114 x 86
  LayerB    : 0x1577b2
  Pal       : 0xe974e
  Tilemap   : 0x1583bc
  Enemy       : 0x158044
Config for level 6
0x1a1984
  Map: 0x15daf8
  Map size  : 139 x 85
  LayerB    : 0x16377a
  Pal       : 0xe97ce
  Tilemap   : 0x1642f0
  Enemy       : 0x163bf4
Config for level 7
0x1a19c8
  Map: 0x16fca2L
  Map size  : 128 x 82
  LayerB    : 0x172b5a
  Pal       : 0xe986e
  Tilemap   : 0x173546
  Enemy       : 0x1730c2
Config for level 8
0x1a1a0c
  Map: 0x17af22L
  Map size  : 69 x 140
  LayerB    : 0x17e05e
  Pal       : 0xe98ee
  Tilemap   : 0x17e81e
  Enemy       : 0x17e60c
Config for level 9
0x1a1a50
  Map: 0x185f22L
  Map size  : 131 x 81
  LayerB    : 0x1885f6
  Pal       : 0xe996e
  Tilemap   : 0x1892a6
  Enemy       : 0x188e7e
Config for level 10
0x1a1a94
  Map: 0x18f67aL
  Map size  : 132 x 80
  LayerB    : 0x190dfe
  Pal       : 0xe99ee
  Tilemap   : 0x1915e0
  Enemy       : 0x1911c0
Config for level 11
0x1a1ad8
  Map: 0x194cd6L
  Map size  : 53 x 48
  LayerB    : 0x1954d2
  Pal       : 0xe9a8e
  Tilemap   : 0x195d88
  Enemy       : 0x195c0e
Config for level 12
0x1a1b1c
  Map: 0x19722cL
  Map size  : 58 x 46
  LayerB    : 0x197b16
  Pal       : 0xe9a8e
  Tilemap   : 0x1983d4
  Enemy       : 0x19824e
Config for level 13
0x1a1b60
  Map: 0x199c08L
  Map size  : 38 x 56
  LayerB    : 0x19a3c6
  Pal       : 0xe9a8e
  Tilemap   : 0x19ac54
  Enemy       : 0x19aafa
Config for level 14
0x1a1ba4
  Map: 0x19bff8L
  Map size  : 56 x 43
  LayerB    : 0x19c9c2
  Pal       : 0xe9a8e
  Tilemap   : 0x19d2b6
  Enemy       : 0x19d0fc
Config for level 15
0x1a1be8
  Map: 0x19edd2L
  Map size  : 39 x 44
  LayerB    : 0x19f476
  Pal       : 0xe9a8e
  Tilemap   : 0x19fcec
  Enemy       : 0x19fbaa
  
Уровень 0    - финальная заставка
Уровни 1-10  - игровые уровни
Уровни 11-15 - бонусы