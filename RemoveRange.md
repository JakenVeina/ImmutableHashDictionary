#### `.RemoveRange(keys)`
|                  Method | InitialSize | RemoveSize |          Mean |         Error |        StdDev |    Ratio | Allocated Memory/Op |
|------------------------ |------------ |----------- |--------------:|--------------:|--------------:|---------:|--------------------:|
|              Dictionary |           1 |          1 |      10.84 ns |     2.8977 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |           1 |          1 |      85.48 ns |     1.5698 ns |     1.3109 ns |     7.89 |                32 B |
| ImmutableHashDictionary |           1 |          1 |     257.00 ns |     2.2996 ns |     2.1510 ns |    23.71 |               288 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |          10 |          1 |      10.84 ns |     2.8977 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |          10 |          1 |     331.24 ns |     3.7113 ns |     3.4715 ns |    30.56 |               200 B |
| ImmutableHashDictionary |          10 |          1 |     349.22 ns |     1.6443 ns |     1.4576 ns |    32.22 |               512 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |          10 |         10 |      78.77 ns |     2.9633 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |          10 |         10 |    1228.54 ns |    16.8345 ns |    15.7470 ns |    15.60 |               416 B |
| ImmutableHashDictionary |          10 |         10 |     520.94 ns |     5.1750 ns |     4.8407 ns |     6.61 |               512 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |         100 |          1 |      10.84 ns |     2.8977 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |          1 |     816.84 ns |     2.7010 ns |     2.2555 ns |    75.35 |               520 B |
| ImmutableHashDictionary |         100 |          1 |    1335.08 ns |    15.4244 ns |    14.4280 ns |   123.16 |              3200 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |         100 |         10 |      78.77 ns |     2.9633 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |         10 |    4360.89 ns |    34.2788 ns |    32.0644 ns |    55.36 |              1928 B |
| ImmutableHashDictionary |         100 |         10 |    1502.34 ns |    16.4430 ns |    14.5763 ns |    19.07 |              3200 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |         100 |        100 |     762.38 ns |    32.1583 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |        100 |   24377.33 ns |   104.9935 ns |    98.2109 ns |    31.98 |              4576 B |
| ImmutableHashDictionary |         100 |        100 |    2953.79 ns |    15.1387 ns |    14.1607 ns |     3.87 |              3200 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |        1000 |          1 |      10.84 ns |     2.8977 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |          1 |    1007.32 ns |     2.3980 ns |     1.8722 ns |    92.93 |               648 B |
| ImmutableHashDictionary |        1000 |          1 |   10854.37 ns |    40.0517 ns |    33.4450 ns |  1001.33 |             31088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |        1000 |         10 |      78.77 ns |     2.9633 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |         10 |    7506.96 ns |    76.1022 ns |    67.4626 ns |    95.30 |              3720 B |
| ImmutableHashDictionary |        1000 |         10 |   11014.09 ns |    49.8697 ns |    44.2082 ns |   139.83 |             31088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |        1000 |        100 |     762.38 ns |    32.1583 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |        100 |   61366.99 ns |   224.3389 ns |   209.8467 ns |    80.49 |             21256 B |
| ImmutableHashDictionary |        1000 |        100 |   12537.88 ns |    32.9016 ns |    29.1664 ns |    16.45 |             31088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |        1000 |       1000 |    7770.40 ns |   109.9582 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |       1000 |  392332.96 ns |   709.1645 ns |   628.6559 ns |    50.49 |             43936 B |
| ImmutableHashDictionary |        1000 |       1000 |   27692.39 ns |    54.7783 ns |    51.2396 ns |     3.56 |             31088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |       10000 |          1 |      10.84 ns |     2.8977 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |          1 |    1561.37 ns |    17.3950 ns |    16.2712 ns |   144.04 |               968 B |
| ImmutableHashDictionary |       10000 |          1 |  145525.98 ns |  1552.3729 ns |  1452.0905 ns | 13424.91 |            283088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |       10000 |         10 |      78.77 ns |     2.9633 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |         10 |   11912.58 ns |    29.2890 ns |    24.4576 ns |   151.23 |              6408 B |
| ImmutableHashDictionary |       10000 |         10 |  146347.88 ns |  1368.5549 ns |  1280.1471 ns |  1857.91 |            283088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |       10000 |        100 |     762.38 ns |    32.1583 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |        100 |  107969.54 ns |  1118.5056 ns |  1046.2508 ns |   141.62 |             41928 B |
| ImmutableHashDictionary |       10000 |        100 |  148658.41 ns |   666.9246 ns |   623.8417 ns |   194.99 |            283088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |       10000 |       1000 |    7770.40 ns |   109.9582 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |       1000 |  890583.28 ns |  6380.1838 ns |  5968.0279 ns |   114.61 |            198728 B |
| ImmutableHashDictionary |       10000 |       1000 |  168863.69 ns |   880.4764 ns |   735.2376 ns |    21.73 |            283088 B |
|                         |             |            |               |               |               |          |                     |
|              Dictionary |       10000 |      10000 |  122491.37 ns |  3855.8868 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |      10000 | 5747143.74 ns | 78103.8402 ns | 73058.3806 ns |    46.92 |            444896 B |
| ImmutableHashDictionary |       10000 |      10000 |  372475.31 ns |   737.9089 ns |   654.1370 ns |     3.04 |            283088 B |
