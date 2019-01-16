##### `.Add(key, value)` *(when .Count is prime)*
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
