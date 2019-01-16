#### `.SetItem(key, value)` *(replacement)*
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
