# ImmutableHashDictionary
An implementation of `IImmutableDictionary<TKey, TValue>` that maintains O(1) value lookup, at the cost of mutation performance.

A good choice when working with dictionaries that remain moderately small and/or that are accessed for reading far more frequently than for modification.

This package also includes an `ImmutableHashDictionary` static factory class and an `ImmutableHashDictionary<TKey, TValue>.Builder` class that provide API''s identical to the `ImmutableDictionary` and `ImmutableDictionary<TKey, TValue>.Builder` classes.

Targets .NET Standard 2.0.

C#8 (Nullable Reference Types) Compliant.

## Benchmarks
The following benchmarks demonstrate the performance advantages and disadvantages of the `ImmutableHashDictionary<TKey, TValue>` class over the `Dictionary<TKey, TValue>` and `ImmutableDictionary<TKey, TValue>` classes.

The performance of value lookup by key for `ImmutableHashDictionary<TKey, TValue>` is, as desired, on par with that of `Dictionary<TKey, TValue>` and far exceeds that of `ImmutableDictionary<TKey, TValue>`, even for very large collections.

The performance of single add and remove operations for `ImmutableHashDictionary<TKey, TValue>` is, as expected, comparable to that of `ImmutableDictionary<TKey, TValue>` for smaller collections, but does not scale well (*O(N) vs O(log N)*).

Interestingly, the performance of ranged add and remove operations for `ImmutableHashDictionary<TKey, TValue>` tends to far exceed that of `ImmutableDictionary<TKey, TValue>`, and tends to be comparable to that of `Dictionary<TKey, TValue>` suggesting that the performance costs of mutating an `ImmutableHashDictionary` can be mitigated by performing mutations in batches, as much as possible.

### this[key]
|                  Method |  Size |      Mean |     Error |    StdDev | Ratio |
|------------------------ |------ |----------:|----------:|----------:|------:|
|              Dictionary |     1 |  6.000 ns | 0.0439 ns | 0.0390 ns |  1.00 |
|     ImmutableDictionary |     1 | 48.808 ns | 0.7066 ns | 0.6609 ns |  8.14 |
| ImmutableHashDictionary |     1 |  6.292 ns | 0.1233 ns | 0.1154 ns |  1.05 |
|                         |       |           |           |           |       |
|              Dictionary |    10 |  6.136 ns | 0.0864 ns | 0.0809 ns |  1.00 |
|     ImmutableDictionary |    10 | 51.734 ns | 0.4677 ns | 0.4146 ns |  8.44 |
| ImmutableHashDictionary |    10 |  6.266 ns | 0.1259 ns | 0.1178 ns |  1.02 |
|                         |       |           |           |           |       |
|              Dictionary |   100 |  6.268 ns | 0.1327 ns | 0.1241 ns |  1.00 |
|     ImmutableDictionary |   100 | 58.166 ns | 0.4842 ns | 0.4529 ns |  9.28 |
| ImmutableHashDictionary |   100 |  6.168 ns | 0.0582 ns | 0.0516 ns |  0.98 |
|                         |       |           |           |           |       |
|              Dictionary |  1000 |  6.215 ns | 0.1299 ns | 0.1215 ns |  1.00 |
|     ImmutableDictionary |  1000 | 58.608 ns | 1.2055 ns | 1.1840 ns |  9.44 |
| ImmutableHashDictionary |  1000 |  6.156 ns | 0.0503 ns | 0.0446 ns |  0.99 |
|                         |       |           |           |           |       |
|              Dictionary | 10000 |  6.199 ns | 0.1401 ns | 0.1242 ns |  1.00 |
|     ImmutableDictionary | 10000 | 77.493 ns | 1.6028 ns | 1.4993 ns | 12.53 |
| ImmutableHashDictionary | 10000 |  6.505 ns | 0.1405 ns | 0.1314 ns |  1.05 |

### .CreateRange(keyValuePairs)
|                  Method |  Size |            Mean |           Error |         StdDev | Ratio | Allocated Memory/Op |
|------------------------ |------ |----------------:|----------------:|---------------:|------:|--------------------:|
|              Dictionary |     0 |        46.35 ns |       0.9763 ns |       1.269 ns |  1.00 |                80 B |
|     ImmutableDictionary |     0 |        65.03 ns |       1.3378 ns |       1.251 ns |  1.41 |                 0 B |
| ImmutableHashDictionary |     0 |       192.17 ns |       3.8251 ns |       3.757 ns |  4.17 |                40 B |
|                         |       |                 |                 |                |       |                     |
|              Dictionary |     1 |        79.24 ns |       1.3233 ns |       1.238 ns |  1.00 |               248 B |
|     ImmutableDictionary |     1 |       263.73 ns |       1.3972 ns |       1.167 ns |  3.32 |               136 B |
| ImmutableHashDictionary |     1 |       240.73 ns |       4.5442 ns |       4.463 ns |  3.04 |               288 B |
|                         |       |                 |                 |                |       |                     |
|              Dictionary |    10 |       241.57 ns |       4.4772 ns |       4.188 ns |  1.00 |               472 B |
|     ImmutableDictionary |    10 |     2,581.45 ns |      27.9192 ns |      24.750 ns | 10.71 |               712 B |
| ImmutableHashDictionary |    10 |       552.11 ns |       9.1460 ns |       8.555 ns |  2.29 |              1064 B |
|                         |       |                 |                 |                |       |                     |
|              Dictionary |   100 |     1,757.44 ns |      24.9881 ns |      22.151 ns |  1.00 |              3160 B |
|     ImmutableDictionary |   100 |    40,694.37 ns |     803.2922 ns |     788.940 ns | 23.16 |              6472 B |
| ImmutableHashDictionary |   100 |     3,361.60 ns |      45.3226 ns |      42.395 ns |  1.91 |             10264 B |
|                         |       |                 |                 |                |       |                     |
|              Dictionary |  1000 |    16,878.20 ns |     301.6920 ns |     282.203 ns |  1.00 |             31048 B |
|     ImmutableDictionary |  1000 |   596,376.87 ns |   9,221.7936 ns |   8,626.071 ns | 35.34 |             64072 B |
| ImmutableHashDictionary |  1000 |    32,284.29 ns |     608.5277 ns |     624.913 ns |  1.91 |            102288 B |
|                         |       |                 |                 |                |       |                     |
|              Dictionary | 10000 |   202,857.95 ns |   2,945.2097 ns |   2,754.951 ns |  1.00 |            283048 B |
|     ImmutableDictionary | 10000 | 8,182,551.46 ns | 120,133.2153 ns | 106,494.966 ns | 40.30 |            640072 B |
| ImmutableHashDictionary | 10000 |   430,420.22 ns |   3,323.5302 ns |   2,946.223 ns |  2.12 |            942080 B |

### .Add(key, value)
|                  Method | InitialSize |          Mean |        Error |        StdDev |    Ratio | Allocated Memory/Op |
|------------------------ |------------ |--------------:|-------------:|--------------:|---------:|--------------------:|
|              Dictionary |           0 |      28.36 ns |    1.4888 ns |             - |     1.00 |               136 B |
|     ImmutableDictionary |           0 |     214.02 ns |    0.8531 ns |     0.7124 ns |     7.55 |               104 B |
| ImmutableHashDictionary |           0 |     224.06 ns |    0.6326 ns |     0.5608 ns |     7.90 |               256 B |
|                         |             |               |              |               |          |                     |
|              Dictionary |           1 |       9.15 ns |    1.5643 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |           1 |     389.29 ns |    2.6709 ns |     2.4984 ns |    42.55 |               168 B |
| ImmutableHashDictionary |           1 |     242.94 ns |    2.5039 ns |     2.3421 ns |    26.55 |               256 B |
|                         |             |               |              |               |          |                     |
|              Dictionary |          10 |       9.15 ns |    1.5643 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |          10 |     551.94 ns |    7.3001 ns |     6.8285 ns |    60.32 |               296 B |
| ImmutableHashDictionary |          10 |     342.65 ns |    3.2559 ns |     3.0456 ns |    37.45 |               480 B |
|                         |             |               |              |               |          |                     |
|              Dictionary |         100 |       9.15 ns |    1.5643 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |    1025.23 ns |    7.3588 ns |     5.7453 ns |   112.05 |               552 B |
| ImmutableHashDictionary |         100 |    1346.02 ns |    9.7505 ns |     9.1206 ns |   147.11 |              3168 B |
|                         |             |               |              |               |          |                     |
|              Dictionary |        1000 |       9.15 ns |    1.5643 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |    1353.79 ns |    5.7833 ns |     5.1267 ns |   147.96 |               744 B |
| ImmutableHashDictionary |        1000 |   11217.29 ns |   89.2151 ns |    83.4519 ns |  1225.93 |             31056 B |
|                         |             |               |              |               |          |                     |
|              Dictionary |       10000 |       9.15 ns |    1.5643 ns |             - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |    1788.01 ns |   33.7117 ns |    31.5340 ns |   195.41 |               936 B |
| ImmutableHashDictionary |       10000 |  144916.55 ns | 1375.6121 ns | 1,286.7484 ns | 15837.87 |            283056 B |

### .Add(key, value) *(when .Count is prime)*
|                  Method | InitialSize |         Mean |         Error |       StdDev | Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------------:|--------------:|-------------:|------:|--------------------:|
|              Dictionary |           0 |     25.53 ns |     1.5619 ns |            - |  1.00 |               136 B |
|     ImmutableDictionary |           0 |    222.85 ns |     4.4379 ns |    6.5051 ns |  8.73 |               104 B |
| ImmutableHashDictionary |           0 |    224.38 ns |     1.7037 ns |    1.5936 ns |  8.79 |               256 B |
|                         |             |              |               |              |       |                     |
|              Dictionary |           3 |     68.68 ns |     1.6596 ns |            - |  1.00 |               248 B |
|     ImmutableDictionary |           3 |    431.89 ns |     2.6507 ns |    2.4795 ns |  6.29 |               232 B |
| ImmutableHashDictionary |           3 |    318.72 ns |     1.2154 ns |    1.1369 ns |  4.64 |               504 B |
|                         |             |              |               |              |       |                     |
|              Dictionary |          11 |    131.62 ns |     6.0206 ns |            - |  1.00 |               696 B |
|     ImmutableDictionary |          11 |    537.82 ns |     2.4469 ns |    2.0432 ns |  4.09 |               296 B |
| ImmutableHashDictionary |          11 |    470.75 ns |     1.2720 ns |    1.1276 ns |  3.58 |              1176 B |
|                         |             |              |               |              |       |                     |
|              Dictionary |         107 |    812.79 ns |    35.9507 ns |            - |  1.00 |              6744 B |
|     ImmutableDictionary |         107 |    879.47 ns |    16.8856 ns |   17.3403 ns |  1.08 |               488 B |
| ImmutableHashDictionary |         107 |   2210.48 ns |    47.5006 ns |   44.4321 ns |  2.72 |              9912 B |
|                         |             |              |               |              |       |                     |
|              Dictionary |        1103 |   7796.77 ns |   432.4643 ns |            - |  1.00 |             65376 B |
|     ImmutableDictionary |        1103 |   1210.01 ns |     6.3420 ns |    5.9323 ns |  0.16 |               680 B |
| ImmutableHashDictionary |        1103 |  20224.73 ns |   140.9945 ns |  131.8863 ns |  2.59 |             96432 B |
|                         |             |              |               |              |       |                     |
|              Dictionary |       10103 | 171343.34 ns |  4703.2988 ns |            - |  1.00 |            588696 B |
|     ImmutableDictionary |       10103 |   1834.92 ns |    12.3591 ns |   10.9560 ns |  0.01 |              1000 B |
| ImmutableHashDictionary |       10103 | 235576.27 ns |  4431.4051 ns | 4925.4950 ns |  1.37 |            872822 B |

### .AddRange(keyValuePairs)
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

### .SetItem(key, value) *(replace)*
|                  Method |  Size |           Mean |         Error |        StdDev |     Ratio | Allocated Memory/Op |
|------------------------ |------ |---------------:|--------------:|--------------:|----------:|--------------------:|
|              Dictionary |     1 |       7.640 ns |     0.0790 ns |     0.0739 ns |      1.00 |                   - |
|     ImmutableDictionary |     1 |     290.443 ns |     2.8913 ns |     2.4144 ns |     38.09 |               104 B |
| ImmutableHashDictionary |     1 |     250.868 ns |     5.2318 ns |     6.0249 ns |     33.00 |               256 B |
|                         |       |                |               |               |           |                     |
|              Dictionary |    10 |       7.782 ns |     0.1371 ns |     0.1282 ns |      1.00 |                   - |
|     ImmutableDictionary |    10 |     522.905 ns |    10.1501 ns |     9.4944 ns |     67.22 |               232 B |
| ImmutableHashDictionary |    10 |     349.176 ns |     2.5976 ns |     2.3027 ns |     44.82 |               480 B |
|                         |       |                |               |               |           |                     |
|              Dictionary |   100 |       7.825 ns |     0.1751 ns |     0.1638 ns |      1.00 |                   - |
|     ImmutableDictionary |   100 |     947.042 ns |     7.0417 ns |     6.5868 ns |    121.08 |               488 B |
| ImmutableHashDictionary |   100 |   1,339.356 ns |    20.0410 ns |    18.7463 ns |    171.21 |              3168 B |
|                         |       |                |               |               |           |                     |
|              Dictionary |  1000 |       7.684 ns |     0.0976 ns |     0.0913 ns |      1.00 |                   - |
|     ImmutableDictionary |  1000 |     956.664 ns |    15.0128 ns |    14.0430 ns |    124.51 |               488 B |
| ImmutableHashDictionary |  1000 |  10,981.291 ns |   133.5296 ns |   124.9036 ns |  1,429.27 |             31056 B |
|                         |       |                |               |               |           |                     |
|              Dictionary | 10000 |       7.650 ns |     0.1428 ns |     0.1335 ns |      1.00 |                   - |
|     ImmutableDictionary | 10000 |   1,650.127 ns |    34.7196 ns |    43.9092 ns |    215.39 |               872 B |
| ImmutableHashDictionary | 10000 | 145,914.089 ns | 2,605.8473 ns | 2,437.5112 ns | 19,079.30 |            283056 B |

### .SetItems(keyValuePairs) *(replace)*
|                  Method | InitialSize | ReplaceSize |             Mean |           Error |          StdDev |     Ratio | Allocated Memory/Op |
|------------------------ |------------ |------------ |-----------------:|----------------:|----------------:|----------:|--------------------:|
|              Dictionary |           1 |           1 |         9.440 ns |       0.1923 ns |       0.1606 ns |      1.00 |                 0 B |
|     ImmutableDictionary |           1 |           1 |       351.779 ns |       4.5582 ns |       3.8063 ns |     37.27 |               200 B |
| ImmutableHashDictionary |           1 |           1 |       265.332 ns |       5.2382 ns |       7.8403 ns |     28.24 |               288 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |          10 |           1 |         9.855 ns |       0.2211 ns |       0.2366 ns |      1.00 |                 0 B |
|     ImmutableDictionary |          10 |           1 |       576.821 ns |      10.9868 ns |      10.2771 ns |     58.72 |               328 B |
| ImmutableHashDictionary |          10 |           1 |       369.588 ns |       7.2780 ns |       9.7160 ns |     37.74 |               512 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |          10 |          10 |        83.457 ns |       1.3435 ns |       1.1219 ns |      1.00 |                 0 B |
|     ImmutableDictionary |          10 |          10 |     3,500.155 ns |      33.6915 ns |      31.5151 ns |     41.89 |               904 B |
| ImmutableHashDictionary |          10 |          10 |       702.887 ns |      12.5832 ns |      11.7704 ns |      8.42 |              1208 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |         100 |           1 |         9.562 ns |       0.2097 ns |       0.2059 ns |      1.00 |                 0 B |
|     ImmutableDictionary |         100 |           1 |     1,044.838 ns |      20.6068 ns |      25.3070 ns |    109.31 |               584 B |
| ImmutableHashDictionary |         100 |           1 |     1,364.839 ns |      25.1187 ns |      23.4960 ns |    142.67 |              3200 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |         100 |          10 |        82.735 ns |       0.6290 ns |       0.5252 ns |      1.00 |                 0 B |
|     ImmutableDictionary |         100 |          10 |     5,175.783 ns |     103.1257 ns |     118.7597 ns |     62.26 |              1160 B |
| ImmutableHashDictionary |         100 |          10 |     2,384.388 ns |      54.1202 ns |      50.6241 ns |     28.82 |              9944 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |         100 |         100 |       790.299 ns |       5.4498 ns |       4.8311 ns |      1.00 |                 0 B |
|     ImmutableDictionary |         100 |         100 |    50,024.830 ns |     499.9229 ns |     467.6281 ns |     63.36 |              6920 B |
| ImmutableHashDictionary |         100 |         100 |     4,313.287 ns |      49.0558 ns |      43.4867 ns |      5.46 |              9944 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |        1000 |           1 |         9.302 ns |       0.1038 ns |       0.0971 ns |      1.00 |                 0 B |
|     ImmutableDictionary |        1000 |           1 |     1,309.966 ns |       8.2506 ns |       7.7176 ns |    140.83 |               776 B |
| ImmutableHashDictionary |        1000 |           1 |    10,766.519 ns |      83.6922 ns |      74.1910 ns |  1,158.59 |             31088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |        1000 |          10 |        82.083 ns |       0.9220 ns |       0.8173 ns |      1.00 |                 0 B |
|     ImmutableDictionary |        1000 |          10 |     6,725.176 ns |      47.7554 ns |      44.6705 ns |     81.93 |              1352 B |
| ImmutableHashDictionary |        1000 |          10 |    10,983.614 ns |      62.1225 ns |      58.1094 ns |    133.85 |             31088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |        1000 |         100 |       860.153 ns |      17.0830 ns |      36.0339 ns |      1.00 |                 0 B |
|     ImmutableDictionary |        1000 |         100 |    65,398.141 ns |   1,269.1538 ns |   1,694.2839 ns |     75.40 |              7112 B |
| ImmutableHashDictionary |        1000 |         100 |    14,548.614 ns |     342.0154 ns |   1,008.4400 ns |     17.25 |             31088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |        1000 |        1000 |     8,667.570 ns |     239.9110 ns |     707.3831 ns |      1.00 |                 0 B |
|     ImmutableDictionary |        1000 |        1000 |   668,391.837 ns |   1,207.7611 ns |   1,070.6488 ns |     77.00 |             64712 B |
| ImmutableHashDictionary |        1000 |        1000 |    42,924.334 ns |     653.8727 ns |     611.6329 ns |      4.98 |             96464 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |       10000 |           1 |         9.485 ns |       0.2045 ns |       0.1913 ns |      1.00 |                 0 B |
|     ImmutableDictionary |       10000 |           1 |     1,695.116 ns |      20.0399 ns |      18.7453 ns |    178.78 |               968 B |
| ImmutableHashDictionary |       10000 |           1 |   141,532.143 ns |     717.2506 ns |     635.8240 ns | 14,900.83 |            283088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |       10000 |          10 |        83.408 ns |       1.2424 ns |       1.1621 ns |      1.00 |                 0 B |
|     ImmutableDictionary |       10000 |          10 |     8,386.312 ns |      90.6181 ns |      80.3306 ns |    100.62 |              1544 B |
| ImmutableHashDictionary |       10000 |          10 |   142,472.826 ns |     999.0194 ns |     885.6047 ns |  1,709.29 |            283088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |       10000 |         100 |       784.880 ns |       8.2941 ns |       7.7583 ns |      1.00 |                 0 B |
|     ImmutableDictionary |       10000 |         100 |    79,747.713 ns |     488.7767 ns |     457.2021 ns |    101.61 |              7304 B |
| ImmutableHashDictionary |       10000 |         100 |   147,678.708 ns |   2,676.8561 ns |   2,503.9329 ns |    188.17 |            283088 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |       10000 |        1000 |     8,124.191 ns |     161.6262 ns |     422.9475 ns |      1.00 |                 0 B |
|     ImmutableDictionary |       10000 |        1000 |   833,338.735 ns |   8,722.7407 ns |   8,159.2570 ns |     99.91 |             64904 B |
| ImmutableHashDictionary |       10000 |        1000 |   244,919.905 ns |   5,197.4487 ns |   4,861.6967 ns |     29.36 |            872871 B |
|                         |             |             |                  |                 |                 |           |                     |
|              Dictionary |       10000 |       10000 |    82,662.034 ns |   1,183.5433 ns |     988.3123 ns |      1.00 |                 0 B |
|     ImmutableDictionary |       10000 |       10000 | 8,995,166.093 ns |  77,294.8756 ns |  68,519.8939 ns |    108.90 |            640904 B |
| ImmutableHashDictionary |       10000 |       10000 |   498,028.761 ns |   6,140.5503 ns |   5,127.6376 ns |      6.03 |            872625 B |

### .Remove(key)
|                  Method | InitialSize |         Mean |        Error |      StdDev |    Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------------:|-------------:|------------:|---------:|--------------------:|
|              Dictionary |           1 |      7.52 ns |    2.0462 ns |           - |     1.00 |                 0 B |
|     ImmutableDictionary |           1 |     96.99 ns |    0.5850 ns |   0.5472 ns |    12.90 |                 0 B |
| ImmutableHashDictionary |           1 |    236.96 ns |    1.8249 ns |   1.6177 ns |    31.51 |               256 B |
|                         |             |              |              |             |          |                     |
|              Dictionary |          10 |      7.52 ns |    2.0462 ns |           - |     1.00 |                 0 B |
|     ImmutableDictionary |          10 |    344.37 ns |    1.6720 ns |   1.3962 ns |    45.79 |               168 B |
| ImmutableHashDictionary |          10 |    337.89 ns |    1.7456 ns |   1.5474 ns |    44.93 |               480 B |
|                         |             |              |              |             |          |                     |
|              Dictionary |         100 |      7.52 ns |    2.0462 ns |           - |     1.00 |                 0 B |
|     ImmutableDictionary |         100 |    957.12 ns |   12.4433 ns |  11.6395 ns |   127.28 |               552 B |
| ImmutableHashDictionary |         100 |   1327.62 ns |   26.4600 ns |  36.2188 ns |   176.55 |              3168 B |
|                         |             |              |              |             |          |                     |
|              Dictionary |        1000 |      7.52 ns |    2.0462 ns |           - |     1.00 |                 0 B |
|     ImmutableDictionary |        1000 |   1037.99 ns |   19.9332 ns |  23.7290 ns |   138.03 |               616 B |
| ImmutableHashDictionary |        1000 |  10928.93 ns |   89.9645 ns |  84.1529 ns |  1453.32 |             31056 B |
|                         |             |              |              |             |          |                     |
|              Dictionary |       10000 |      7.52 ns |    2.0462 ns |           - |     1.00 |                 0 B |
|     ImmutableDictionary |       10000 |   1566.88 ns |    6.9433 ns |   5.7980 ns |   208.36 |               936 B |
| ImmutableHashDictionary |       10000 | 154010.98 ns |  926.6922 ns | 866.8284 ns | 20480.18 |            283056 B |

### .RemoveRange(keys)
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
