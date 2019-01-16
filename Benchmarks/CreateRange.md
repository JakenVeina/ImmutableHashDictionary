#### 1.CreateRange(keyValuePairs)1
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
