# General Benchmark
| Method         |     Mean |     Error |    StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |    Gen2 | Allocated | Alloc Ratio |
|----------------|---------:|----------:|----------:|------:|--------:|---------:|---------:|--------:|----------:|------------:|
| RenderMarkdown | 2.256 ms | 0.0420 ms | 0.1076 ms |  1.00 |    0.07 | 257.8125 | 250.0000 | 46.8750 |   2.14 MB |        1.00 |


| Method         |     Mean |     Error |    StdDev | Ratio |    Gen0 |    Gen1 |    Gen2 | Allocated | Alloc Ratio |
|----------------|---------:|----------:|----------:|------:|--------:|--------:|--------:|----------:|------------:|
| RenderMarkdown | 1.981 ms | 0.0153 ms | 0.0128 ms |  1.00 | 89.8438 | 85.9375 | 42.9688 |   1.51 MB |        1.00 |

# Individual Benchmarks

| Method                | InputCase            |         Mean |       Error |       StdDev |       Median |    Gen0 |   Gen1 |   Gen2 | Allocated |
|-----------------------|----------------------|-------------:|------------:|-------------:|-------------:|--------:|-------:|-------:|----------:|
| SerializeToSyntaxTree | BlockQuote_100Lines  | 104,823.0 ns | 2,087.82 ns |  4,022.52 ns | 105,504.4 ns | 13.0615 | 5.0049 |      - | 107.17 KB |
| SerializeToSyntaxTree | BlockQuote_10Lines   |  11,833.7 ns |   235.91 ns |    394.16 ns |  11,803.4 ns |  1.5259 | 0.0763 |      - |  12.49 KB |
| SerializeToSyntaxTree | BlockQuote_1Line     |   1,738.4 ns |    33.07 ns |     35.38 ns |   1,730.8 ns |  0.3033 | 0.0038 |      - |   2.48 KB |
| SerializeToSyntaxTree | BlockQuote_2Lines    |   4,276.1 ns |   366.70 ns |  1,081.21 ns |   4,119.1 ns |  0.4463 | 0.0076 |      - |   3.67 KB |
| SerializeToSyntaxTree | BlockQuote_3Lines    |   3,461.2 ns |    63.81 ns |    145.32 ns |   3,457.7 ns |  0.4539 | 0.0076 |      - |   3.73 KB |
| SerializeToSyntaxTree | Bold                 |   1,797.7 ns |    35.52 ns |     46.19 ns |   1,804.6 ns |  0.3242 | 0.0038 |      - |   2.66 KB |
| SerializeToSyntaxTree | BoldAndItalic        |   2,632.7 ns |    38.95 ns |     69.24 ns |   2,645.4 ns |  0.4196 | 0.0076 |      - |   3.45 KB |
| SerializeToSyntaxTree | BoldA(...)Other [28] |   3,086.9 ns |    59.35 ns |     70.65 ns |   3,085.1 ns |  0.4425 | 0.0076 |      - |   3.62 KB |
| SerializeToSyntaxTree | Bold_2InLine         |   2,836.9 ns |    47.08 ns |     44.04 ns |   2,852.4 ns |  0.4425 | 0.0076 |      - |   3.63 KB |
| SerializeToSyntaxTree | Break                |   2,049.5 ns |    39.98 ns |     57.34 ns |   2,060.3 ns |  0.2899 | 0.0038 |      - |   2.38 KB |
| SerializeToSyntaxTree | Callout              |   2,797.3 ns |    55.42 ns |     75.86 ns |   2,813.5 ns |  0.4463 | 0.0076 |      - |   3.67 KB |
| SerializeToSyntaxTree | Callout_withoutBody  |   1,509.3 ns |    29.77 ns |     42.69 ns |   1,509.6 ns |  0.3185 | 0.0038 |      - |    2.6 KB |
| SerializeToSyntaxTree | CodeBlock            |   2,275.9 ns |    45.41 ns |    105.26 ns |   2,271.9 ns |  0.3090 | 0.0038 |      - |   2.55 KB |
| SerializeToSyntaxTree | CodeBlock_100Lines   |  40,663.0 ns | 1,476.87 ns |  4,261.10 ns |  39,504.6 ns |  1.5869 | 0.0610 |      - |  13.09 KB |
| SerializeToSyntaxTree | CodeBlock_50Lines    |  21,371.0 ns |   500.93 ns |  1,453.28 ns |  21,215.9 ns |  0.9155 | 0.0305 |      - |   7.62 KB |
| SerializeToSyntaxTree | CodeBlock_NoLanguage |   1,164.9 ns |    44.62 ns |    131.57 ns |   1,163.4 ns |  0.2670 | 0.0038 |      - |   2.19 KB |
| SerializeToSyntaxTree | CodeInline           |   1,330.2 ns |    26.01 ns |     42.01 ns |   1,338.5 ns |  0.3014 | 0.0038 |      - |   2.47 KB |
| SerializeToSyntaxTree | CodeInline_2ticks    |   1,368.1 ns |    24.08 ns |     52.34 ns |   1,355.8 ns |  0.3014 | 0.0038 |      - |   2.47 KB |
| SerializeToSyntaxTree | CodeInline_3ticks    |   1,363.9 ns |    20.05 ns |     44.01 ns |   1,365.1 ns |  0.3014 | 0.0038 |      - |   2.47 KB |
| SerializeToSyntaxTree | Emote                |   1,187.8 ns |    23.25 ns |     34.80 ns |   1,199.8 ns |  0.2365 | 0.0019 |      - |   1.94 KB |
| SerializeToSyntaxTree | EscapedCharacters    |   1,768.0 ns |    34.07 ns |     48.86 ns |   1,789.2 ns |  0.3147 | 0.0038 |      - |   2.57 KB |
| SerializeToSyntaxTree | FootnoteDescription  |   1,716.7 ns |    17.13 ns |     15.19 ns |   1,722.4 ns |  0.3357 | 0.0038 |      - |   2.75 KB |
| SerializeToSyntaxTree | FootnoteReference    |   1,201.7 ns |    22.91 ns |     22.50 ns |   1,202.1 ns |  0.2842 | 0.0038 |      - |   2.34 KB |
| SerializeToSyntaxTree | FrontMatter          |     763.8 ns |    11.74 ns |     10.98 ns |     762.4 ns |  0.2518 | 0.0010 |      - |   2.06 KB |
| SerializeToSyntaxTree | FrontMatter_2Entries |   3,521.8 ns |    67.13 ns |     74.61 ns |   3,513.4 ns |  0.5608 | 0.0114 |      - |   4.61 KB |
| SerializeToSyntaxTree | HeadingSimple        |     971.1 ns |    19.26 ns |     20.61 ns |     978.1 ns |  0.2651 | 0.0038 |      - |   2.18 KB |
| SerializeToSyntaxTree | Heading_1            |     931.7 ns |    18.63 ns |     17.43 ns |     932.3 ns |  0.2565 | 0.0029 |      - |    2.1 KB |
| SerializeToSyntaxTree | Heading_2            |   1,009.0 ns |    31.89 ns |     91.49 ns |     982.4 ns |  0.2565 | 0.0029 |      - |    2.1 KB |
| SerializeToSyntaxTree | Heading_3            |   1,002.5 ns |    19.47 ns |     29.13 ns |   1,006.0 ns |  0.2556 | 0.0019 |      - |    2.1 KB |
| SerializeToSyntaxTree | Heading_4            |   1,023.8 ns |    19.41 ns |     37.40 ns |   1,022.5 ns |  0.2556 | 0.0019 |      - |    2.1 KB |
| SerializeToSyntaxTree | Heading_5            |   1,039.7 ns |    20.60 ns |     38.70 ns |   1,029.7 ns |  0.2556 | 0.0019 |      - |    2.1 KB |
| SerializeToSyntaxTree | Heading_6            |   1,048.9 ns |    21.03 ns |     27.35 ns |   1,045.3 ns |  0.2556 | 0.0019 |      - |    2.1 KB |
| SerializeToSyntaxTree | Highlight            |   2,515.1 ns |   157.21 ns |    463.55 ns |   2,493.6 ns |  0.3166 | 0.0038 |      - |   2.59 KB |
| SerializeToSyntaxTree | HorizontalRule       |     971.5 ns |    65.16 ns |    192.12 ns |     956.4 ns |  0.2079 | 0.0019 |      - |   1.71 KB |
| SerializeToSyntaxTree | HtmlBlock            |   2,211.7 ns |    51.54 ns |    146.21 ns |   2,205.3 ns |  0.3262 | 0.0038 |      - |   2.66 KB |
| SerializeToSyntaxTree | Italic               |   2,160.8 ns |    74.38 ns |    213.42 ns |   2,109.4 ns |  0.3281 | 0.0038 |      - |    2.7 KB |
| SerializeToSyntaxTree | Italic_2InLine       |   3,939.5 ns |   253.46 ns |    747.33 ns |   3,960.8 ns |  0.4501 | 0.0076 |      - |    3.7 KB |
| SerializeToSyntaxTree | Link                 |   3,508.9 ns |   124.84 ns |    368.10 ns |   3,442.3 ns |  0.3662 |      - |      - |   3.02 KB |
| SerializeToSyntaxTree | Link_Nested          |   5,467.7 ns |   108.85 ns |    214.85 ns |   5,483.7 ns |  0.3586 |      - |      - |   2.97 KB |
| SerializeToSyntaxTree | List_Ordered         |   3,610.1 ns |    71.50 ns |    189.61 ns |   3,616.5 ns |  0.5646 | 0.0153 |      - |   4.67 KB |
| SerializeToSyntaxTree | List_Ordered_100     | 124,239.3 ns | 6,202.89 ns | 18,289.38 ns | 129,261.8 ns | 17.0898 | 8.0566 |      - | 139.82 KB |
| SerializeToSyntaxTree | List_Ordered_50      |  66,607.1 ns | 1,329.86 ns |  3,640.46 ns |  66,409.9 ns |  8.6060 | 2.1973 |      - |  70.49 KB |
| SerializeToSyntaxTree | List_Task            |   2,385.7 ns |   119.07 ns |    351.09 ns |   2,549.2 ns |  0.3967 | 0.0076 |      - |   3.27 KB |
| SerializeToSyntaxTree | List_Task_100        | 125,710.5 ns | 4,045.63 ns | 11,542.41 ns | 128,810.2 ns | 17.2119 | 7.9346 |      - | 140.99 KB |
| SerializeToSyntaxTree | List_Task_50         |  70,300.3 ns | 1,404.52 ns |  1,875.00 ns |  69,753.2 ns |  8.6670 | 2.3193 |      - |  71.08 KB |
| SerializeToSyntaxTree | List_Task_checked    |   2,762.5 ns |    54.61 ns |     95.64 ns |   2,777.2 ns |  0.3967 | 0.0076 |      - |   3.27 KB |
| SerializeToSyntaxTree | List_(...)d_100 [21] | 119,430.4 ns | 2,377.28 ns |  5,964.13 ns | 120,232.6 ns | 17.0898 | 7.8125 |      - |    141 KB |
| SerializeToSyntaxTree | List_Task_checked_50 |  71,094.4 ns | 1,323.01 ns |  2,484.94 ns |  70,931.7 ns |  8.6670 | 2.3193 |      - |  71.08 KB |
| SerializeToSyntaxTree | List_UnOrdered       |   4,420.9 ns |   127.70 ns |    376.53 ns |   4,502.9 ns |  0.5569 | 0.0153 |      - |   4.56 KB |
| SerializeToSyntaxTree | List_UnOrdered_100   | 128,540.7 ns | 2,962.19 ns |  8,687.59 ns | 128,347.2 ns | 16.3574 | 7.5684 |      - | 133.96 KB |
| SerializeToSyntaxTree | List_UnOrdered_50    |  65,977.7 ns | 2,163.15 ns |  6,378.09 ns |  65,821.2 ns |  8.1787 | 1.9531 |      - |  67.56 KB |
| SerializeToSyntaxTree | Mixed_RealWorld      |  29,987.4 ns |   596.90 ns |  1,572.48 ns |  29,819.9 ns |  2.1362 | 0.1221 |      - |  17.84 KB |
| SerializeToSyntaxTree | NewLine              |   4,136.9 ns |    78.82 ns |     93.83 ns |   4,129.8 ns |  0.3662 |      - |      - |   3.03 KB |
| SerializeToSyntaxTree | Paragraph            |   2,360.7 ns |    47.03 ns |    117.13 ns |   2,358.6 ns |  0.2365 |      - |      - |   1.94 KB |
| SerializeToSyntaxTree | Paragraph_Base       |   2,063.3 ns |   117.09 ns |    345.23 ns |   2,197.4 ns |  0.2365 |      - |      - |   1.94 KB |
| SerializeToSyntaxTree | Strikethrough        |   3,232.4 ns |    96.97 ns |    285.91 ns |   3,244.3 ns |  0.3242 | 0.0038 |      - |   2.67 KB |
| SerializeToSyntaxTree | Strik(...)nLine [21] |   5,440.2 ns |   107.91 ns |    236.86 ns |   5,429.4 ns |  0.4349 | 0.0076 |      - |   3.59 KB |
| SerializeToSyntaxTree | Subscript            |   2,684.4 ns |    53.44 ns |     90.74 ns |   2,678.2 ns |  0.3281 | 0.0038 |      - |    2.7 KB |
| SerializeToSyntaxTree | Subscript_2InLine    |   4,477.2 ns |    89.46 ns |    131.13 ns |   4,480.8 ns |  0.4501 | 0.0076 |      - |    3.7 KB |
| SerializeToSyntaxTree | Superscript          |   2,762.5 ns |    54.95 ns |    154.98 ns |   2,734.4 ns |  0.3281 | 0.0038 |      - |    2.7 KB |
| SerializeToSyntaxTree | Superscript_2InLine  |   5,117.6 ns |   102.96 ns |    303.57 ns |   5,181.1 ns |  0.4501 | 0.0076 |      - |    3.7 KB |
| SerializeToSyntaxTree | Table                |   5,861.7 ns |   134.17 ns |    395.61 ns |   5,825.8 ns |  0.5035 | 0.0076 |      - |   4.13 KB |
| SerializeToSyntaxTree | Table_100Rows        | 142,121.2 ns | 5,273.80 ns | 15,549.92 ns | 143,746.1 ns | 12.4512 | 2.1973 | 0.9766 | 102.68 KB |
| SerializeToSyntaxTree | Table_2Rows          |   7,917.2 ns |   347.31 ns |  1,024.04 ns |   7,996.3 ns |  0.6256 | 0.0153 |      - |   5.13 KB |
| SerializeToSyntaxTree | Table_3Rows          |   7,426.9 ns |   396.97 ns |  1,157.99 ns |   7,125.2 ns |  0.7477 | 0.0153 |      - |   6.12 KB |
| SerializeToSyntaxTree | Table_50Rows         |  50,289.8 ns | 1,459.92 ns |  4,235.50 ns |  48,934.8 ns |  6.4697 | 0.6104 |      - |  53.04 KB |
| SerializeToSyntaxTree | Tag                  |   1,178.3 ns |    23.45 ns |     47.37 ns |   1,180.6 ns |  0.2861 | 0.0038 |      - |   2.34 KB |
| SerializeToSyntaxTree | Template             |   1,530.5 ns |    45.19 ns |    127.46 ns |   1,494.4 ns |  0.3090 | 0.0038 |      - |   2.53 KB |
| SerializeToSyntaxTree | Underline            |   1,846.8 ns |    46.27 ns |    132.00 ns |   1,817.0 ns |  0.3300 | 0.0038 |      - |    2.7 KB |
| SerializeToSyntaxTree | User                 |   1,271.7 ns |    41.44 ns |    118.24 ns |   1,233.1 ns |  0.2861 | 0.0038 |      - |   2.34 KB |
| SerializeToSyntaxTree | WikiLink             |   1,352.8 ns |    23.71 ns |     19.80 ns |   1,358.0 ns |  0.2861 | 0.0038 |      - |   2.35 KB |
| SerializeToSyntaxTree | Wrapper              |   2,165.3 ns |    59.70 ns |    171.29 ns |   2,124.9 ns |  0.3662 | 0.0038 |      - |      3 KB |

