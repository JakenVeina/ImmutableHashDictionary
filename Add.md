#### `.Add(key, value)`
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
