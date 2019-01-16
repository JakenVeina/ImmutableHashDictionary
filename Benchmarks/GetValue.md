#### `value = this[key]`
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
