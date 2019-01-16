#### `.SetItems(keyValuePairs)` *(replacement)*
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
