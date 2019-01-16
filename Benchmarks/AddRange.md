#### `.AddRange(keyValuePairs)`
|                  Method | InitialSize | AddSize |          Mean |           Error |         StdDev |    Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------- |--------------:|----------------:|---------------:|---------:|--------------------:|
|              Dictionary |           0 |       1 |      29.93 ns |       0.8173 ns |              - |     1.00 |               136 B |
|     ImmutableDictionary |           0 |       1 |     261.38 ns |       3.7988 ns |      3.5534 ns |     8.73 |               136 B |
| ImmutableHashDictionary |           0 |       1 |     246.82 ns |       1.6186 ns |      1.4348 ns |     8.25 |               288 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           0 |      10 |     253.58 ns |       2.1347 ns |              - |     1.00 |               912 B |
|     ImmutableDictionary |           0 |      10 |    2530.53 ns |       8.9753 ns |      7.4947 ns |     9.98 |               712 B |
| ImmutableHashDictionary |           0 |      10 |     585.18 ns |       5.1730 ns |      4.8388 ns |     2.31 |              1064 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           0 |     100 |    2283.28 ns |      10.0188 ns |              - |     1.00 |             10112 B |
|     ImmutableDictionary |           0 |     100 |   39609.38 ns |     189.4154 ns |    158.1704 ns |    17.35 |              6472 B |
| ImmutableHashDictionary |           0 |     100 |    3659.35 ns |      30.8113 ns |     28.8209 ns |     1.60 |             10264 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           0 |    1000 |   23649.73 ns |     117.6651 ns |              - |     1.00 |            102136 B |
|     ImmutableDictionary |           0 |    1000 |  578727.43 ns |    5092.8743 ns |   4763.8778 ns |    24.47 |             64072 B |
| ImmutableHashDictionary |           0 |    1000 |   35068.42 ns |     284.1185 ns |    251.8637 ns |     1.48 |            102288 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           0 |   10000 |  366725.39 ns |    2079.7787 ns |              - |     1.00 |            941928 B |
|     ImmutableDictionary |           0 |   10000 | 8058043.99 ns |   61363.4568 ns |  54397.1061 ns |    21.97 |            640072 B |
| ImmutableHashDictionary |           0 |   10000 |  464379.36 ns |   13402.0388 ns |  13762.9071 ns |     1.27 |            942080 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           1 |       1 |       7.16 ns |       2.5236 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |           1 |       1 |     356.99 ns |       2.7839 ns |      2.6041 ns |    49.86 |               200 B |
| ImmutableHashDictionary |           1 |       1 |     262.94 ns |       1.5480 ns |      1.2926 ns |    36.72 |               288 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           1 |      10 |     227.77 ns |       2.3950 ns |              - |     1.00 |               776 B |
|     ImmutableDictionary |           1 |      10 |    2896.76 ns |      11.8285 ns |     10.4856 ns |    12.72 |               776 B |
| ImmutableHashDictionary |           1 |      10 |     618.53 ns |       5.4599 ns |      5.1072 ns |     2.72 |              1064 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           1 |     100 |    2302.81 ns |      13.6586 ns |              - |     1.00 |              9976 B |
|     ImmutableDictionary |           1 |     100 |   39921.80 ns |     395.2395 ns |    369.7073 ns |    17.34 |              6536 B |
| ImmutableHashDictionary |           1 |     100 |    4223.49 ns |      38.7687 ns |     36.2643 ns |     1.83 |             10264 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           1 |    1000 |   24259.29 ns |     481.9970 ns |              - |     1.00 |            102000 B |
|     ImmutableDictionary |           1 |    1000 |  588893.03 ns |    6146.6896 ns |   5448.8803 ns |    24.27 |             64136 B |
| ImmutableHashDictionary |           1 |    1000 |   42857.54 ns |     576.6016 ns |    539.3535 ns |     1.77 |            102288 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |           1 |   10000 |  372084.09 ns |    3904.5997 ns |              - |     1.00 |            941792 B |
|     ImmutableDictionary |           1 |   10000 | 8063159.09 ns |   32707.9322 ns |  27312.6047 ns |    21.67 |            640136 B |
| ImmutableHashDictionary |           1 |   10000 |  537069.99 ns |    2619.1990 ns |   2321.8517 ns |     1.44 |            942080 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |          10 |       1 |       9.22 ns |       3.6755 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |          10 |       1 |     570.24 ns |       4.6396 ns |      4.3399 ns |    61.85 |               328 B |
| ImmutableHashDictionary |          10 |       1 |     362.93 ns |       1.3176 ns |      1.1680 ns |    39.36 |               512 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |          10 |      10 |     212.07 ns |       5.0475 ns |              - |     1.00 |               696 B |
|     ImmutableDictionary |          10 |      10 |    3559.02 ns |      20.7094 ns |     18.3583 ns |    16.78 |               904 B |
| ImmutableHashDictionary |          10 |      10 |     700.94 ns |       2.7882 ns |      2.4716 ns |     3.31 |              1208 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |          10 |     100 |    2412.14 ns |      25.4079 ns |              - |     1.00 |             11856 B |
|     ImmutableDictionary |          10 |     100 |   41525.88 ns |     378.4752 ns |    316.0440 ns |    17.22 |              6664 B |
| ImmutableHashDictionary |          10 |     100 |    4524.91 ns |      14.2613 ns |     11.9088 ns |     1.88 |             12368 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |          10 |    1000 |   18473.06 ns |     116.7562 ns |              - |     1.00 |             57432 B |
|     ImmutableDictionary |          10 |    1000 |  582677.26 ns |    5300.1219 ns |   4957.7373 ns |    31.54 |             64264 B |
| ImmutableHashDictionary |          10 |    1000 |   37684.11 ns |     334.2896 ns |    312.6947 ns |     2.04 |             57944 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |          10 |   10000 |  267593.92 ns |    2796.7988 ns |              - |     1.00 |            541904 B |
|     ImmutableDictionary |          10 |   10000 | 7924666.99 ns |   32183.5431 ns |  28529.8727 ns |    29.61 |            640264 B |
| ImmutableHashDictionary |          10 |   10000 |  430208.73 ns |    6191.2373 ns |   5791.2872 ns |     1.61 |            542416 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |         100 |       1 |       9.22 ns |       3.6755 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |       1 |    1039.50 ns |       4.8779 ns |      4.5628 ns |   112.74 |               584 B |
| ImmutableHashDictionary |         100 |       1 |    1327.08 ns |       8.6046 ns |      7.6278 ns |   143.93 |              3200 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |         100 |      10 |     921.02 ns |      27.6598 ns |              - |     1.00 |              6744 B |
|     ImmutableDictionary |         100 |      10 |    5113.37 ns |      23.8241 ns |     22.2851 ns |     5.55 |              1160 B |
| ImmutableHashDictionary |         100 |      10 |    2359.76 ns |      28.7931 ns |     26.9330 ns |     2.56 |              9944 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |         100 |     100 |    1808.58 ns |      40.5070 ns |              - |     1.00 |              6744 B |
|     ImmutableDictionary |         100 |     100 |   49665.01 ns |     199.6832 ns |    177.0140 ns |    27.46 |              6920 B |
| ImmutableHashDictionary |         100 |     100 |    4418.36 ns |      38.3222 ns |     35.8466 ns |     2.44 |              9944 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |         100 |    1000 |   17478.11 ns |     219.6867 ns |              - |     1.00 |             52320 B |
|     ImmutableDictionary |         100 |    1000 |  600360.26 ns |    1229.1039 ns |    959.6035 ns |    34.35 |             64520 B |
| ImmutableHashDictionary |         100 |    1000 |   38266.66 ns |     322.5733 ns |    301.7353 ns |     2.19 |             55520 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |         100 |   10000 |  265091.44 ns |    1741.4037 ns |              - |     1.00 |            536792 B |
|     ImmutableDictionary |         100 |   10000 | 8109644.78 ns |   59532.8977 ns |  55687.1095 ns |    30.59 |            640520 B |
| ImmutableHashDictionary |         100 |   10000 |  425607.45 ns |    3154.8203 ns |   2951.0209 ns |     1.61 |            539992 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |        1000 |       1 |       9.22 ns |       3.6755 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |       1 |    1392.05 ns |      27.8664 ns |     27.3685 ns |   150.98 |               776 B | 
| ImmutableHashDictionary |        1000 |       1 |   11150.18 ns |     221.3832 ns |    271.8785 ns |  1209.35 |             31088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |        1000 |      10 |      72.28 ns |     268.4138 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |      10 |    6942.81 ns |      71.7715 ns |     67.1351 ns |    96.05 |              1352 B |
| ImmutableHashDictionary |        1000 |      10 |   11360.19 ns |     200.3409 ns |    177.5970 ns |   157.17 |             31088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |        1000 |     100 |     821.38 ns |     850.2601 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |     100 |   61750.60 ns |     192.2241 ns |    160.5158 ns |    75.18 |              7112 B |
| ImmutableHashDictionary |        1000 |     100 |   13069.62 ns |      90.4216 ns |     84.5804 ns |    15.91 |             31088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |        1000 |    1000 |   16516.74 ns |     244.3363 ns |              - |     1.00 |             65376 B |
|     ImmutableDictionary |        1000 |    1000 |  673571.21 ns |    1476.8144 ns |   1309.1575 ns |    40.78 |             64712 B |
| ImmutableHashDictionary |        1000 |    1000 |   42179.45 ns |     227.3857 ns |    212.6967 ns |     2.55 |             96464 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |        1000 |   10000 |  424433.42 ns |    9851.9504 ns |              - |     1.00 |           1073168 B |
|     ImmutableDictionary |        1000 |   10000 | 8329490.80 ns |  159888.0700 ns | 157031.4636 ns |    19.62 |            640712 B |
| ImmutableHashDictionary |        1000 |   10000 |  589152.38 ns |    2855.6120 ns |   2671.1413 ns |     1.39 |           1104256 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |       10000 |       1 |       9.22 ns |       3.6755 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |       1 |    1732.75 ns |      26.0770 ns |     24.3924 ns |   187.93 |               968 B |
| ImmutableHashDictionary |       10000 |       1 |  147556.67 ns |    3411.6293 ns |   3024.3205 ns | 16003.98 |            283088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |       10000 |      10 |      72.28 ns |     268.4138 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |      10 |    8396.64 ns |      48.5728 ns |     45.4350 ns |   116.17 |              1544 B |
| ImmutableHashDictionary |       10000 |      10 |  146010.93 ns |    2470.8196 ns |   2845.3996 ns |  2020.07 |            283088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |       10000 |     100 |     821.38 ns |     850.2601 ns |              - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |     100 |   80150.10 ns |     343.8395 ns |    304.8047 ns |    97.58 |              7304 B |
| ImmutableHashDictionary |       10000 |     100 |  149126.16 ns |    1675.9748 ns |   1567.7079 ns |   191.56 |            283088 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |       10000 |    1000 |  179068.61 ns |    5474.0610 ns |              - |     1.00 |            588696 B |
|     ImmutableDictionary |       10000 |    1000 |  840334.77 ns |   16569.3282 ns |  17015.4802 ns |     4.69 |             64904 B |
| ImmutableHashDictionary |       10000 |    1000 |  255715.49 ns |    4968.8888 ns |   5102.6830 ns |     1.43 |            872934 B |
|                         |             |         |               |                 |                |          |                     |
|              Dictionary |       10000 |   10000 |  298948.18 ns |    6666.4771 ns |              - |     1.00 |            588696 B |
|     ImmutableDictionary |       10000 |   10000 | 9259572.75 ns |  133467.9538 ns | 124846.0068 ns |    30.97 |            640904 B |
| ImmutableHashDictionary |       10000 |   10000 |  519205.21 ns |    5639.4331 ns |   5275.1292 ns |     1.74 |            872640 B |
