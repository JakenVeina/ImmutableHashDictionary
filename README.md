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

### .Add(key, value)
|                  Method | InitialSize |         Mean |         Error |        StdDev | Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------------:|--------------:|--------------:|------:|--------------------:|
|              Dictionary |           0 |     970.9 ns |    76.4143 ns |   203.9652 ns |  1.00 |               136 B |
|     ImmutableDictionary |           0 |   2,001.1 ns |   138.6443 ns |   379.5367 ns |  2.15 |               104 B |
| ImmutableHashDictionary |           0 |   2,547.9 ns |   190.5385 ns |   531.1451 ns |  2.66 |               256 B |
|                         |             |              |               |               |       |                     |
|              Dictionary |           1 |     184.1 ns |    13.2316 ns |    38.3872 ns |     ? |                   - |
|     ImmutableDictionary |           1 |   2,366.4 ns |   244.8573 ns |   686.6062 ns |     ? |               168 B |
| ImmutableHashDictionary |           1 |   2,443.1 ns |   164.5355 ns |   447.6308 ns |     ? |               256 B |
|                         |             |              |               |               |       |                     |
|              Dictionary |          10 |     136.5 ns |    21.7974 ns |    64.2702 ns |     ? |                   - |
|     ImmutableDictionary |          10 |   2,959.2 ns |   262.9957 ns |   728.7600 ns |     ? |               296 B |
| ImmutableHashDictionary |          10 |   2,873.2 ns |   260.1023 ns |   729.3548 ns |     ? |               480 B |
|                         |             |              |               |               |       |                     |
|              Dictionary |         100 |     179.3 ns |     0.0000 ns |     0.0000 ns |  1.00 |                   - |
|     ImmutableDictionary |         100 |   3,929.6 ns |   299.6802 ns |   845.2532 ns | 18.36 |               552 B |
| ImmutableHashDictionary |         100 |   4,235.0 ns |   335.7266 ns |   957.8469 ns | 20.29 |              3168 B |
|                         |             |              |               |               |       |                     |
|              Dictionary |        1000 |     154.3 ns |    15.0782 ns |    43.5041 ns |     ? |                   - |
|     ImmutableDictionary |        1000 |   4,941.8 ns |   298.1516 ns |   874.4271 ns |     ? |               744 B |
| ImmutableHashDictionary |        1000 |  16,407.6 ns | 1,134.2875 ns | 3,326.6689 ns |     ? |             31056 B |
|                         |             |              |               |               |       |                     |
|              Dictionary |       10000 |     249.3 ns |    55.8710 ns |   164.7369 ns |     ? |                   - |
|     ImmutableDictionary |       10000 |   6,181.5 ns |   382.8808 ns | 1,104.6987 ns |     ? |               936 B |
| ImmutableHashDictionary |       10000 | 111,847.3 ns | 2,220.8432 ns | 5,012.8133 ns |     ? |            283056 B |

### .Add(key, value) *(when .Count is prime)*
|                  Method | InitialSize |         Mean |       Error |      StdDev | Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------------:|------------:|------------:|------:|--------------------:|
|              Dictionary |           0 |     882.2 ns |    70.18 ns |    188.5 ns |  1.00 |               136 B |
|     ImmutableDictionary |           0 |   1,929.7 ns |    95.26 ns |    255.9 ns |  2.28 |               104 B |
| ImmutableHashDictionary |           0 |   2,360.0 ns |   105.09 ns |    280.5 ns |  2.79 |               256 B |
|                         |             |              |             |             |       |                     |
|              Dictionary |           3 |   1,066.0 ns |    59.27 ns |    153.0 ns |  1.00 |               248 B |
|     ImmutableDictionary |           3 |   2,328.0 ns |    72.56 ns |    188.6 ns |  2.22 |               232 B |
| ImmutableHashDictionary |           3 |   3,400.0 ns |   305.87 ns |    867.7 ns |  3.20 |               504 B |
|                         |             |              |             |             |       |                     |
|              Dictionary |          11 |   1,687.1 ns |   249.06 ns |    730.5 ns |  1.00 |               696 B |
|     ImmutableDictionary |          11 |   2,920.4 ns |   250.09 ns |    705.4 ns |  1.99 |               296 B |
| ImmutableHashDictionary |          11 |   3,081.8 ns |   251.42 ns |    709.1 ns |  2.09 |              1176 B |
|                         |             |              |             |             |       |                     |
|              Dictionary |         107 |   2,717.1 ns |   262.09 ns |    747.8 ns |  1.00 |              6744 B |
|     ImmutableDictionary |         107 |   3,497.1 ns |   283.46 ns |    799.5 ns |  1.39 |               488 B |
| ImmutableHashDictionary |         107 |   7,118.2 ns |   644.73 ns |  1,829.0 ns |  2.81 |              9912 B |
|                         |             |              |             |             |       |                     |
|              Dictionary |        1103 |  13,239.0 ns | 1,803.46 ns |  5,317.5 ns |  1.00 |             65376 B |
|     ImmutableDictionary |        1103 |   4,717.1 ns |   344.22 ns |    993.1 ns |  0.43 |               680 B |
| ImmutableHashDictionary |        1103 |  22,269.9 ns |   461.45 ns |    877.9 ns |  2.02 |             96432 B |
|                         |             |              |             |             |       |                     |
|              Dictionary |       10103 |  75,746.2 ns | 2,650.20 ns |  7,474.9 ns |  1.00 |            588696 B |
|     ImmutableDictionary |       10103 |   5,987.0 ns |   527.19 ns |  1,554.4 ns |  0.08 |              1000 B |
| ImmutableHashDictionary |       10103 | 185,595.8 ns | 4,734.19 ns | 13,583.3 ns |  2.48 |            871752 B |

### .AddRange(pairs)
|                  Method | InitialSize | AddSize |            Mean |           Error |          StdDev |  Ratio | Allocated Memory/Op |
|------------------------ |------------ |-------- |----------------:|----------------:|----------------:|-------:|--------------------:|
|    DictionaryForeachAdd |           0 |       0 |        69.01 ns |      23.9987 ns |      70.7606 ns |      ? |                   - |
|     ImmutableDictionary |           0 |       0 |       718.04 ns |      48.1781 ns |     141.2980 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |       0 |       332.89 ns |      37.7953 ns |     111.4402 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |       1 |       934.67 ns |       0.0000 ns |       0.0000 ns |   1.00 |               136 B |
|     ImmutableDictionary |           0 |       1 |     2,445.51 ns |     160.6347 ns |     434.2854 ns |   2.65 |               136 B |
| ImmutableHashDictionary |           0 |       1 |     2,839.75 ns |     189.7137 ns |     531.9775 ns |   2.89 |               288 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |      10 |     1,866.73 ns |     237.2479 ns |     692.0632 ns |   1.00 |               912 B |
|     ImmutableDictionary |           0 |      10 |     5,908.57 ns |     120.0956 ns |     261.0779 ns |   3.80 |               712 B |
| ImmutableHashDictionary |           0 |      10 |     3,516.57 ns |     214.2783 ns |     604.3757 ns |   2.10 |              1064 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |     100 |     6,127.85 ns |     540.0984 ns |   1,592.4924 ns |   1.00 |             10112 B |
|     ImmutableDictionary |           0 |     100 |    58,366.87 ns |   1,702.2483 ns |   4,828.9952 ns |  10.36 |              6472 B |
| ImmutableHashDictionary |           0 |     100 |     9,056.40 ns |     677.5656 ns |   1,976.4906 ns |   1.60 |             10264 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |    1000 |    27,204.44 ns |     533.3415 ns |     961.7248 ns |   1.00 |            102136 B |
|     ImmutableDictionary |           0 |    1000 |   665,705.40 ns |  14,015.9489 ns |  14,393.3476 ns |  24.63 |             64072 B |
| ImmutableHashDictionary |           0 |    1000 |    40,543.23 ns |     810.8403 ns |   1,927.0478 ns |   1.49 |            102288 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |   10000 |   273,535.45 ns |   4,231.8483 ns |   3,533.7850 ns |   1.00 |            941928 B |
|     ImmutableDictionary |           0 |   10000 | 8,505,411.19 ns |  63,297.5746 ns |  56,111.6512 ns |  31.10 |            640072 B |
| ImmutableHashDictionary |           0 |   10000 |   334,428.56 ns |   5,601.4726 ns |   5,239.6209 ns |   1.22 |            942080 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |       0 |        61.07 ns |      19.6034 ns |      57.8009 ns |      ? |                   - |
|     ImmutableDictionary |           1 |       0 |       642.20 ns |      29.2855 ns |      74.5409 ns |      ? |                   - |
| ImmutableHashDictionary |           1 |       0 |       304.73 ns |      43.9815 ns |     129.6804 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |       1 |       192.06 ns |       0.0000 ns |       0.0000 ns |   1.00 |                   - |
|     ImmutableDictionary |           1 |       1 |     2,638.99 ns |     203.0004 ns |     565.8839 ns |  12.45 |               200 B |
| ImmutableHashDictionary |           1 |       1 |     2,688.78 ns |      79.9404 ns |     206.3516 ns |  13.81 |               288 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |      10 |     1,879.58 ns |     255.2855 ns |     752.7150 ns |   1.00 |               776 B |
|     ImmutableDictionary |           1 |      10 |     6,400.29 ns |     137.6125 ns |     248.1438 ns |   4.60 |               776 B |
| ImmutableHashDictionary |           1 |      10 |     3,686.76 ns |     256.3485 ns |     718.8286 ns |   2.28 |              1064 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |     100 |     6,059.35 ns |     659.3858 ns |   1,902.4787 ns |   1.00 |              9976 B |
|     ImmutableDictionary |           1 |     100 |    55,440.05 ns |   1,100.5162 ns |   2,506.4322 ns |  10.71 |              6536 B |
| ImmutableHashDictionary |           1 |     100 |    11,184.69 ns |     943.5654 ns |   2,752.4245 ns |   2.02 |             10264 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |    1000 |    26,531.11 ns |     492.0184 ns |     436.1615 ns |   1.00 |            102000 B |
|     ImmutableDictionary |           1 |    1000 |   670,610.62 ns |  12,781.8026 ns |  11,330.7351 ns |  25.28 |             64136 B |
| ImmutableHashDictionary |           1 |    1000 |    48,670.79 ns |   1,152.9208 ns |   3,251.8337 ns |   1.84 |            102288 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |   10000 |   271,284.95 ns |   4,482.2929 ns |   3,973.4359 ns |   1.00 |            941792 B |
|     ImmutableDictionary |           1 |   10000 | 8,400,098.87 ns |  54,984.8827 ns |  48,742.6664 ns |  30.97 |            640136 B |
| ImmutableHashDictionary |           1 |   10000 |   411,046.85 ns |   7,434.4449 ns |   8,263.3658 ns |   1.52 |            942080 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |       0 |        58.90 ns |      21.7520 ns |      64.1363 ns |      ? |                   - |
|     ImmutableDictionary |          10 |       0 |       633.54 ns |      26.2678 ns |      68.2735 ns |      ? |                   - |
| ImmutableHashDictionary |          10 |       0 |       320.09 ns |      48.4032 ns |     142.7178 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |       1 |       194.41 ns |      15.5325 ns |      45.3090 ns |      ? |                   - |
|     ImmutableDictionary |          10 |       1 |     3,137.47 ns |     231.7370 ns |     653.6182 ns |      ? |               328 B |
| ImmutableHashDictionary |          10 |       1 |     3,142.00 ns |     265.9841 ns |     754.5527 ns |      ? |               512 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |      10 |     1,767.69 ns |     262.6672 ns |     770.3573 ns |   1.00 |               696 B |
|     ImmutableDictionary |          10 |      10 |     7,726.76 ns |     336.5521 ns |     949.2511 ns |   5.13 |               904 B |
| ImmutableHashDictionary |          10 |      10 |     3,485.70 ns |     211.1815 ns |     592.1757 ns |   2.32 |              1208 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |     100 |     5,514.57 ns |     389.9114 ns |   1,131.2036 ns |   1.00 |             11856 B |
|     ImmutableDictionary |          10 |     100 |    59,195.20 ns |   1,184.1005 ns |   2,882.2643 ns |  11.67 |              6664 B |
| ImmutableHashDictionary |          10 |     100 |     9,627.60 ns |     680.9578 ns |   1,986.3858 ns |   1.81 |             12368 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |    1000 |    26,831.53 ns |   1,413.8610 ns |   4,146.6097 ns |   1.00 |             57432 B |
|     ImmutableDictionary |          10 |    1000 |   651,547.73 ns |   6,934.4225 ns |   6,147.1850 ns |  26.08 |             64264 B |
| ImmutableHashDictionary |          10 |    1000 |    45,243.18 ns |   1,482.1873 ns |   4,370.2626 ns |   1.73 |             57944 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |   10000 |   193,977.71 ns |   3,863.5746 ns |  10,833.8774 ns |   1.00 |            541904 B |
|     ImmutableDictionary |          10 |   10000 | 8,501,298.77 ns | 168,894.1156 ns | 157,983.6605 ns |  43.63 |            640264 B |
| ImmutableHashDictionary |          10 |   10000 |   378,976.63 ns |   7,546.9418 ns |  13,988.7404 ns |   1.95 |            542416 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |       0 |        76.57 ns |      28.2773 ns |      83.3762 ns |      ? |                   - |
|     ImmutableDictionary |         100 |       0 |       635.06 ns |      43.9120 ns |     129.4756 ns |      ? |                   - |
| ImmutableHashDictionary |         100 |       0 |       289.36 ns |      43.9120 ns |     129.4756 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |       1 |       161.14 ns |      10.3098 ns |      29.4144 ns |      ? |                   - |
|     ImmutableDictionary |         100 |       1 |     4,228.67 ns |     342.0232 ns |     970.2629 ns |      ? |               584 B |
| ImmutableHashDictionary |         100 |       1 |     4,041.52 ns |     278.8850 ns |     786.6001 ns |      ? |              3200 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |      10 |     2,685.38 ns |     246.2686 ns |     718.3770 ns |   1.00 |              6744 B |
|     ImmutableDictionary |         100 |      10 |     8,918.36 ns |     177.6123 ns |     333.5989 ns |   4.22 |              1160 B |
| ImmutableHashDictionary |         100 |      10 |     7,034.68 ns |     580.0310 ns |   1,654.8610 ns |   2.84 |              9944 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |     100 |     3,438.51 ns |     286.2220 ns |     839.4395 ns |   1.00 |              6744 B |
|     ImmutableDictionary |         100 |     100 |    63,294.21 ns |     952.7879 ns |     844.6217 ns |  25.63 |              6920 B |
| ImmutableHashDictionary |         100 |     100 |     9,155.44 ns |     638.4627 ns |   1,852.2961 ns |   2.77 |              9944 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |    1000 |    25,717.53 ns |   1,301.4463 ns |   3,837.3439 ns |   1.00 |             52320 B |
|     ImmutableDictionary |         100 |    1000 |   679,774.42 ns |  10,782.8609 ns |  10,086.2948 ns |  27.03 |             64520 B |
| ImmutableHashDictionary |         100 |    1000 |    45,941.90 ns |   1,684.4496 ns |   4,940.1992 ns |   1.85 |             55520 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |   10000 |   189,724.67 ns |   3,789.5162 ns |   9,713.9786 ns |   1.00 |            536792 B |
|     ImmutableDictionary |         100 |   10000 | 8,471,761.22 ns |  91,729.0625 ns |  81,315.4246 ns |  44.53 |            640520 B |
| ImmutableHashDictionary |         100 |   10000 |   376,840.54 ns |   7,459.3013 ns |  10,697.9070 ns |   1.98 |            539992 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |       0 |        54.16 ns |      19.6034 ns |      57.8009 ns |      ? |                   - |
|     ImmutableDictionary |        1000 |       0 |       613.84 ns |      40.8962 ns |     111.9528 ns |      ? |                   - |
| ImmutableHashDictionary |        1000 |       0 |       248.39 ns |      45.2788 ns |     133.5054 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |       1 |       166.45 ns |       0.0000 ns |       0.0000 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |       1 |     4,827.52 ns |     295.6932 ns |     867.2170 ns |  26.77 |               776 B |
| ImmutableHashDictionary |        1000 |       1 |    16,050.42 ns |     980.6639 ns |   2,876.1175 ns |  97.77 |             31088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |      10 |       266.32 ns |      42.9311 ns |     126.5832 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |      10 |    11,893.06 ns |     312.8289 ns |     907.5733 ns |  61.18 |              1352 B |
| ImmutableHashDictionary |        1000 |      10 |    16,408.14 ns |     973.0044 ns |   2,853.6534 ns |  83.49 |             31088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |     100 |     1,138.22 ns |      47.6866 ns |     139.1040 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |     100 |    76,300.25 ns |     787.9745 ns |     657.9944 ns |  68.66 |              7112 B |
| ImmutableHashDictionary |        1000 |     100 |    18,826.62 ns |   1,172.8205 ns |   3,439.6796 ns |  16.82 |             31088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |    1000 |    22,037.74 ns |   1,295.1646 ns |   3,818.8219 ns |   1.00 |             65376 B |
|     ImmutableDictionary |        1000 |    1000 |   754,151.15 ns |   8,699.4522 ns |   8,137.4730 ns |  35.44 |             64712 B |
| ImmutableHashDictionary |        1000 |    1000 |    45,030.64 ns |     875.8906 ns |   1,008.6769 ns |   2.12 |             96464 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |   10000 |   275,837.63 ns |   5,504.3153 ns |  12,974.3188 ns |   1.00 |           1073168 B |
|     ImmutableDictionary |        1000 |   10000 | 8,888,406.95 ns | 177,064.4680 ns | 196,806.6870 ns |  32.02 |            640712 B |
| ImmutableHashDictionary |        1000 |   10000 |   451,658.92 ns |   9,029.6481 ns |  11,089.2188 ns |   1.63 |           1104256 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |       0 |        94.88 ns |      28.0888 ns |      82.8205 ns |      ? |                   - |
|     ImmutableDictionary |       10000 |       0 |       926.55 ns |     112.8137 ns |     320.0336 ns |      ? |                   - |
| ImmutableHashDictionary |       10000 |       0 |       408.06 ns |      76.7541 ns |     217.7387 ns |      ? |                   - |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |       1 |       219.34 ns |      41.2224 ns |     120.8980 ns |      ? |                   - |
|     ImmutableDictionary |       10000 |       1 |     6,267.35 ns |     538.1074 ns |   1,561.1473 ns |      ? |               968 B |
| ImmutableHashDictionary |       10000 |       1 |   108,855.29 ns |   2,274.6448 ns |   4,896.4129 ns |      ? |            283088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |      10 |       409.07 ns |      51.9462 ns |     152.3492 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |      10 |    14,543.56 ns |     571.3143 ns |   1,657.4862 ns |  41.82 |              1544 B |
| ImmutableHashDictionary |       10000 |      10 |   111,234.34 ns |   2,297.7977 ns |   5,139.3539 ns | 320.56 |            283088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |     100 |     1,188.76 ns |      51.4012 ns |     149.1241 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |     100 |    96,497.27 ns |   2,263.7866 ns |   2,117.5473 ns |  84.81 |              7304 B |
| ImmutableHashDictionary |       10000 |     100 |   112,059.39 ns |   2,240.7380 ns |   3,865.1630 ns |  96.76 |            283088 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |    1000 |    86,435.69 ns |   2,372.2588 ns |   6,844.5090 ns |   1.00 |            588696 B |
|     ImmutableDictionary |       10000 |    1000 |   903,992.94 ns |   8,187.1713 ns |   7,658.2851 ns |  10.64 |             64904 B |
| ImmutableHashDictionary |       10000 |    1000 |   198,409.64 ns |   4,156.3253 ns |  12,124.1961 ns |   2.31 |            871784 B |
|                         |             |         |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |   10000 |   188,480.53 ns |   3,612.9119 ns |   3,865.7753 ns |   1.00 |            588696 B |
|     ImmutableDictionary |       10000 |   10000 | 9,779,930.94 ns | 195,524.6434 ns | 200,789.4133 ns |  51.92 |            640904 B |
| ImmutableHashDictionary |       10000 |   10000 |   478,187.52 ns |  12,273.6107 ns |  35,412.1733 ns |   2.61 |            871784 B |

### .Remove(key)
|                  Method | InitialSize |          Mean |       Error |      StdDev | Ratio | Allocated Memory/Op |
|------------------------ |------------ |--------------:|------------:|------------:|------:|--------------------:|
|              Dictionary |           1 |      98.21 ns |    27.90 ns |    82.28 ns |     ? |                   - |
|     ImmutableDictionary |           1 |   1,066.33 ns |    51.44 ns |   150.88 ns |     ? |                   - |
| ImmutableHashDictionary |           1 |   2,549.09 ns |   172.17 ns |   477.07 ns |     ? |               256 B |
|                         |             |               |             |             |       |                     |
|              Dictionary |          10 |     142.12 ns |    28.72 ns |    84.67 ns |     ? |                   - |
|     ImmutableDictionary |          10 |   2,706.04 ns |   247.25 ns |   697.37 ns |     ? |               168 B |
| ImmutableHashDictionary |          10 |   3,165.13 ns |   297.48 ns |   843.91 ns |     ? |               480 B |
|                         |             |               |             |             |       |                     |
|              Dictionary |         100 |     118.18 ns |    25.74 ns |    75.91 ns |     ? |                   - |
|     ImmutableDictionary |         100 |   4,509.13 ns |   317.50 ns |   895.52 ns |     ? |               552 B |
| ImmutableHashDictionary |         100 |   4,062.77 ns |   308.64 ns |   875.57 ns |     ? |              3168 B |
|                         |             |               |             |             |       |                     |
|              Dictionary |        1000 |     112.16 ns |    23.25 ns |    68.55 ns |     ? |                   - |
|     ImmutableDictionary |        1000 |   4,514.92 ns |   301.86 ns |   885.29 ns |     ? |               616 B |
| ImmutableHashDictionary |        1000 |  16,728.77 ns | 1,232.38 ns | 3,575.36 ns |     ? |             31056 B |
|                         |             |               |             |             |       |                     |
|              Dictionary |       10000 |     116.26 ns |    40.10 ns |   118.24 ns |     ? |                   - |
|     ImmutableDictionary |       10000 |   6,226.87 ns |   382.83 ns | 1,122.78 ns |     ? |               936 B |
| ImmutableHashDictionary |       10000 | 110,299.67 ns | 2,346.97 ns | 5,249.33 ns |     ? |            283056 B |

### .RemoveRange(keys)
|                  Method | InitialSize | RemoveSize |            Mean |           Error |          StdDev |  Ratio | Allocated Memory/Op |
|------------------------ |------------ |----------- |----------------:|----------------:|----------------:|-------:|--------------------:|
|    DictionaryForeachAdd |           0 |          0 |        44.81 ns |      20.8168 ns |      61.3789 ns |      ? |                   - |
|     ImmutableDictionary |           0 |          0 |       355.23 ns |      36.9310 ns |     105.3662 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |          0 |       254.14 ns |      70.2602 ns |     111.4402 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |          1 |        55.31 ns |      25.1376 ns |      74.1187 ns |      ? |                   - |
|     ImmutableDictionary |           0 |          1 |       325.21 ns |      41.0887 ns |     121.1510 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |          1 |       322.66 ns |      45.3459 ns |     133.7034 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |         10 |        52.11 ns |      23.1780 ns |      68.3407 ns |      ? |                   - |
|     ImmutableDictionary |           0 |         10 |       291.92 ns |      44.6689 ns |     131.7073 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |         10 |       258.63 ns |      42.9311 ns |     126.5832 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |        100 |        67.47 ns |      34.3133 ns |     101.1734 ns |      ? |                   - |
|     ImmutableDictionary |           0 |        100 |       297.04 ns |      42.5730 ns |     125.5273 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |        100 |       317.53 ns |      43.3268 ns |     127.7502 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |       1000 |        66.07 ns |      25.9271 ns |      76.4466 ns |      ? |                   - |
|     ImmutableDictionary |           0 |       1000 |       335.46 ns |      42.5746 ns |     125.5322 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |       1000 |       317.53 ns |      43.6335 ns |     128.6543 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           0 |      10000 |        44.81 ns |      26.4568 ns |      78.0085 ns |      ? |                   - |
|     ImmutableDictionary |           0 |      10000 |       284.59 ns |      39.8561 ns |     109.7753 ns |      ? |                   - |
| ImmutableHashDictionary |           0 |      10000 |       253.51 ns |      42.5746 ns |     125.5322 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |          0 |        66.58 ns |      27.7950 ns |      81.9543 ns |      ? |                   - |
|     ImmutableDictionary |           1 |          0 |       435.33 ns |       0.0000 ns |       0.0000 ns |      ? |                   - |
| ImmutableHashDictionary |           1 |          0 |       274.00 ns |      42.1429 ns |     124.2594 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |          1 |       126.76 ns |      31.0118 ns |      91.4388 ns |      ? |                   - |
|     ImmutableDictionary |           1 |          1 |     1,553.00 ns |      74.2595 ns |     196.9257 ns |      ? |                32 B |
| ImmutableHashDictionary |           1 |          1 |     2,852.27 ns |     223.4028 ns |     626.4454 ns |      ? |               288 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |         10 |       102.94 ns |      24.6250 ns |      72.6074 ns |      ? |                   - |
|     ImmutableDictionary |           1 |         10 |     1,700.45 ns |      97.4246 ns |     261.7248 ns |      ? |                32 B |
| ImmutableHashDictionary |           1 |         10 |     2,678.79 ns |     209.3510 ns |     587.0426 ns |      ? |               288 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |        100 |       120.10 ns |      28.7297 ns |      84.7102 ns |      ? |                   - |
|     ImmutableDictionary |           1 |        100 |     1,639.46 ns |     128.8420 ns |     354.8684 ns |      ? |                32 B |
| ImmutableHashDictionary |           1 |        100 |     2,873.12 ns |     264.6486 ns |     750.7641 ns |      ? |               288 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |       1000 |       141.48 ns |      35.3873 ns |     104.3402 ns |      ? |                   - |
|     ImmutableDictionary |           1 |       1000 |     1,553.00 ns |      71.0914 ns |     188.5244 ns |      ? |                32 B |
| ImmutableHashDictionary |           1 |       1000 |     3,013.50 ns |     272.1276 ns |     776.3954 ns |      ? |               288 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |           1 |      10000 |       137.26 ns |      32.8344 ns |      96.8131 ns |      ? |                   - |
|     ImmutableDictionary |           1 |      10000 |     1,555.73 ns |      58.2370 ns |     155.4462 ns |      ? |                32 B |
| ImmutableHashDictionary |           1 |      10000 |     2,820.76 ns |     254.9620 ns |     714.9408 ns |      ? |               288 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |          0 |        89.63 ns |      30.5497 ns |      90.0765 ns |      ? |                   - |
|     ImmutableDictionary |          10 |          0 |       380.83 ns |      11.2323 ns |      28.9942 ns |      ? |                   - |
| ImmutableHashDictionary |          10 |          0 |       284.24 ns |      43.6352 ns |     128.6593 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |          1 |       109.47 ns |      32.4106 ns |      95.5633 ns |      ? |                   - |
|     ImmutableDictionary |          10 |          1 |     2,444.39 ns |     197.2365 ns |     556.3089 ns |      ? |               200 B |
| ImmutableHashDictionary |          10 |          1 |     2,924.26 ns |     262.7953 ns |     741.2189 ns |      ? |               512 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |         10 |       220.70 ns |      24.4543 ns |      70.9463 ns |      ? |                   - |
|     ImmutableDictionary |          10 |         10 |     4,034.59 ns |     194.9219 ns |     543.3645 ns |      ? |               416 B |
| ImmutableHashDictionary |          10 |         10 |     2,860.62 ns |     251.8242 ns |     722.5309 ns |      ? |               512 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |        100 |       177.91 ns |      17.5810 ns |      50.4431 ns |      ? |                   - |
|     ImmutableDictionary |          10 |        100 |     3,935.39 ns |     171.3730 ns |     472.0111 ns |      ? |               416 B |
| ImmutableHashDictionary |          10 |        100 |     3,095.83 ns |     284.1847 ns |     819.9379 ns |      ? |               512 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |       1000 |       220.46 ns |      24.7054 ns |      71.2807 ns |      ? |                   - |
|     ImmutableDictionary |          10 |       1000 |     3,602.74 ns |      72.9777 ns |     142.3374 ns |      ? |               416 B |
| ImmutableHashDictionary |          10 |       1000 |     3,008.87 ns |     272.8745 ns |     787.3052 ns |      ? |               512 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |          10 |      10000 |       171.46 ns |      13.0313 ns |      36.7550 ns |      ? |                   - |
|     ImmutableDictionary |          10 |      10000 |     3,845.38 ns |     164.3563 ns |     458.1597 ns |      ? |               416 B |
| ImmutableHashDictionary |          10 |      10000 |     2,829.20 ns |     233.0132 ns |     661.0197 ns |      ? |               512 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |          0 |        84.89 ns |      36.1871 ns |     106.6985 ns |      ? |                   - |
|     ImmutableDictionary |         100 |          0 |       439.59 ns |      16.5818 ns |      46.2235 ns |      ? |                   - |
| ImmutableHashDictionary |         100 |          0 |       115.23 ns |       0.0000 ns |       0.0000 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |          1 |        96.54 ns |      28.0027 ns |      82.5667 ns |      ? |                   - |
|     ImmutableDictionary |         100 |          1 |     3,678.36 ns |     321.3746 ns |     932.3660 ns |      ? |               520 B |
| ImmutableHashDictionary |         100 |          1 |     4,096.64 ns |     303.5395 ns |     866.0151 ns |      ? |              3200 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |         10 |       230.47 ns |       0.0000 ns |       0.0000 ns |   1.00 |                   - |
|     ImmutableDictionary |         100 |         10 |     9,826.71 ns |     367.0084 ns |   1,047.0956 ns |  40.59 |              1928 B |
| ImmutableHashDictionary |         100 |         10 |     4,333.04 ns |     326.1308 ns |     935.7304 ns |  16.56 |              3200 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |        100 |       945.22 ns |      37.3220 ns |     104.6550 ns |   1.00 |                   - |
|     ImmutableDictionary |         100 |        100 |    35,222.01 ns |     412.3044 ns |     344.2928 ns |  38.08 |              4576 B |
| ImmutableHashDictionary |         100 |        100 |     6,022.54 ns |     383.4126 ns |   1,106.2331 ns |   6.42 |              3200 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |       1000 |       952.94 ns |      25.0174 ns |      69.3232 ns |   1.00 |                   - |
|     ImmutableDictionary |         100 |       1000 |    35,656.35 ns |     230.3234 ns |     192.3305 ns |  38.65 |              4576 B |
| ImmutableHashDictionary |         100 |       1000 |     5,784.39 ns |     282.1089 ns |     800.2959 ns |   6.11 |              3200 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |         100 |      10000 |       929.60 ns |      24.1190 ns |      65.6176 ns |   1.00 |                   - |
|     ImmutableDictionary |         100 |      10000 |    35,819.85 ns |     518.5133 ns |     432.9821 ns |  39.96 |              4576 B |
| ImmutableHashDictionary |         100 |      10000 |     6,018.43 ns |     341.5723 ns |     968.9836 ns |   6.48 |              3200 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |          0 |        91.42 ns |      30.5436 ns |      90.0585 ns |      ? |                   - |
|     ImmutableDictionary |        1000 |          0 |       435.33 ns |       0.0000 ns |       0.0000 ns |      ? |                   - |
| ImmutableHashDictionary |        1000 |          0 |       258.63 ns |      42.9311 ns |     126.5832 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |          1 |       145.45 ns |      31.6857 ns |      93.4260 ns |      ? |                   - |
|     ImmutableDictionary |        1000 |          1 |     4,338.91 ns |     297.2255 ns |     871.7111 ns |      ? |               648 B |
| ImmutableHashDictionary |        1000 |          1 |    15,988.47 ns |   1,005.4469 ns |   2,948.8018 ns |      ? |             31088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |         10 |       166.45 ns |       0.0000 ns |       0.0000 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |         10 |    14,500.13 ns |     339.1209 ns |     962.0296 ns |  83.93 |              3720 B |
| ImmutableHashDictionary |        1000 |         10 |    16,951.32 ns |   1,257.5026 ns |   3,668.1941 ns | 103.50 |             31088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |        100 |     1,518.84 ns |     138.5203 ns |     399.6628 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |        100 |    91,165.35 ns |   1,586.6220 ns |   1,324.9012 ns |  53.12 |             21256 B |
| ImmutableHashDictionary |        1000 |        100 |    17,647.95 ns |     933.7577 ns |   2,723.8152 ns |  12.38 |             31088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |       1000 |     8,333.25 ns |     147.3176 ns |     123.0169 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |       1000 |   449,550.96 ns |   8,774.2517 ns |   7,778.1456 ns |  54.03 |             43936 B |
| ImmutableHashDictionary |        1000 |       1000 |    32,714.25 ns |   1,159.7120 ns |   3,401.2344 ns |   3.95 |             31088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |        1000 |      10000 |     8,280.80 ns |     156.9792 ns |     154.1746 ns |   1.00 |                   - |
|     ImmutableDictionary |        1000 |      10000 |   456,135.73 ns |   9,115.2545 ns |  10,851.0621 ns |  55.29 |             43936 B |
| ImmutableHashDictionary |        1000 |      10000 |    33,169.10 ns |   1,112.1016 ns |   3,261.6015 ns |   4.12 |             31088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |          0 |        85.15 ns |      34.5110 ns |     101.7565 ns |      ? |                   - |
|     ImmutableDictionary |       10000 |          0 |       941.14 ns |     116.2081 ns |     329.6632 ns |      ? |                   - |
| ImmutableHashDictionary |       10000 |          0 |       443.69 ns |      67.4564 ns |     196.7734 ns |      ? |                   - |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |          1 |       163.01 ns |      29.2739 ns |      83.0452 ns |      ? |                   - |
|     ImmutableDictionary |       10000 |          1 |     5,604.21 ns |     436.8212 ns |   1,246.2753 ns |      ? |               968 B |
| ImmutableHashDictionary |       10000 |          1 |   110,357.52 ns |   2,252.1545 ns |   4,750.5582 ns |      ? |            283088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |         10 |       318.93 ns |      51.8404 ns |     152.0388 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |         10 |    22,591.13 ns |     516.3534 ns |   1,481.5146 ns |  88.62 |              6408 B |
| ImmutableHashDictionary |       10000 |         10 |   110,265.53 ns |   2,187.6245 ns |   5,026.4311 ns | 451.35 |            283088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |        100 |     1,863.93 ns |     189.5309 ns |     549.8636 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |        100 |   155,934.75 ns |   3,052.2373 ns |   4,751.9658 ns |  84.64 |             41928 B |
| ImmutableHashDictionary |       10000 |        100 |   117,561.38 ns |   2,476.6462 ns |   6,695.7570 ns |  67.79 |            283088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |       1000 |    17,553.21 ns |   2,507.0484 ns |   7,193.1918 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |       1000 | 1,043,280.53 ns |  14,593.1980 ns |  12,185.9812 ns |  59.07 |            198728 B |
| ImmutableHashDictionary |       10000 |       1000 |   138,745.01 ns |   3,666.9615 ns |  10,521.1998 ns |   8.95 |            283088 B |
|                         |             |            |                 |                 |                 |        |                     |
|    DictionaryForeachAdd |       10000 |      10000 |    99,708.65 ns |   1,963.7320 ns |   2,878.4129 ns |   1.00 |                   - |
|     ImmutableDictionary |       10000 |      10000 | 6,118,282.07 ns | 119,710.6865 ns | 151,395.6887 ns |  61.18 |            444896 B |
| ImmutableHashDictionary |       10000 |      10000 |   337,474.86 ns |   7,521.1271 ns |  13,172.6475 ns |   3.40 |            283088 B |
