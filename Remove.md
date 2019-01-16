#### `.Remove(key)`
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
