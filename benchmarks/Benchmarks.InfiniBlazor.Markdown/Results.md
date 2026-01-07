# General Benchmark
| Method         |     Mean |     Error |    StdDev | Ratio | RatioSD |     Gen0 |     Gen1 |    Gen2 | Allocated | Alloc Ratio |
|----------------|---------:|----------:|----------:|------:|--------:|---------:|---------:|--------:|----------:|------------:|
| RenderMarkdown | 2.256 ms | 0.0420 ms | 0.1076 ms |  1.00 |    0.07 | 257.8125 | 250.0000 | 46.8750 |   2.14 MB |        1.00 |


# Individual Benchmarks

| Method                | InputCase            |      Mean |     Error |    StdDev |    Median |    Gen0 |   Gen1 | Allocated |
|-----------------------|----------------------|----------:|----------:|----------:|----------:|--------:|-------:|----------:|
| SerializeToSyntaxTree | BlockQuote_100Lines  | 96.459 us | 1.9014 us | 4.2528 us | 95.462 us | 12.8174 | 4.8828 | 104.88 KB |
| SerializeToSyntaxTree | BlockQuote_10Lines   | 10.900 us | 0.2178 us | 0.4594 us | 10.757 us |  1.4954 | 0.0763 |  12.31 KB |
| SerializeToSyntaxTree | BlockQuote_1Line     |  1.897 us | 0.0377 us | 0.0463 us |  1.902 us |  0.3090 | 0.0038 |   2.52 KB |
| SerializeToSyntaxTree | BlockQuote_2Lines    |  3.127 us | 0.0595 us | 0.0731 us |  3.118 us |  0.4501 | 0.0076 |   3.68 KB |
| SerializeToSyntaxTree | BlockQuote_3Lines    |  3.220 us | 0.0547 us | 0.0512 us |  3.222 us |  0.4578 | 0.0076 |   3.74 KB |
| SerializeToSyntaxTree | Bold                 |  1.788 us | 0.0344 us | 0.0435 us |  1.778 us |  0.3300 | 0.0038 |    2.7 KB |
| SerializeToSyntaxTree | BoldAndItalic        |  2.621 us | 0.0497 us | 0.0591 us |  2.636 us |  0.4234 | 0.0038 |   3.49 KB |
| SerializeToSyntaxTree | BoldA(...)Other [28] |  2.971 us | 0.0570 us | 0.0506 us |  2.973 us |  0.4425 | 0.0076 |   3.63 KB |
| SerializeToSyntaxTree | Bold_2InLine         |  2.687 us | 0.0467 us | 0.0591 us |  2.688 us |  0.4425 | 0.0076 |   3.64 KB |
| SerializeToSyntaxTree | Break                |  1.936 us | 0.0376 us | 0.0433 us |  1.939 us |  0.2937 |      - |   2.41 KB |
| SerializeToSyntaxTree | Callout              |  2.652 us | 0.0520 us | 0.0511 us |  2.643 us |  0.4501 | 0.0038 |   3.69 KB |
| SerializeToSyntaxTree | Callout_withoutBody  |  1.413 us | 0.0278 us | 0.0442 us |  1.404 us |  0.3223 | 0.0038 |   2.64 KB |
| SerializeToSyntaxTree | CodeBlock            |  2.609 us | 0.0511 us | 0.0764 us |  2.618 us |  0.3166 | 0.0038 |    2.6 KB |
| SerializeToSyntaxTree | CodeBlock_100Lines   | 42.710 us | 0.5774 us | 0.5119 us | 42.635 us |  1.5869 | 0.0610 |  13.14 KB |
| SerializeToSyntaxTree | CodeBlock_50Lines    | 22.091 us | 0.4346 us | 0.5173 us | 22.178 us |  0.9155 | 0.0305 |   7.67 KB |
| SerializeToSyntaxTree | CodeBlock_NoLanguage |  1.312 us | 0.0260 us | 0.0320 us |  1.307 us |  0.2728 | 0.0019 |   2.24 KB |
| SerializeToSyntaxTree | CodeInline           |  1.571 us | 0.0277 us | 0.0296 us |  1.568 us |  0.3071 | 0.0038 |   2.52 KB |
| SerializeToSyntaxTree | CodeInline_2ticks    |  1.599 us | 0.0275 us | 0.0282 us |  1.591 us |  0.3071 | 0.0038 |   2.52 KB |
| SerializeToSyntaxTree | CodeInline_3ticks    |  1.693 us | 0.0320 us | 0.0355 us |  1.681 us |  0.3071 | 0.0038 |   2.52 KB |
| SerializeToSyntaxTree | Emote                |  1.443 us | 0.0275 us | 0.0316 us |  1.433 us |  0.2422 | 0.0019 |   1.98 KB |
| SerializeToSyntaxTree | EscapedCharacters    |  2.220 us | 0.0541 us | 0.1535 us |  2.227 us |  0.3166 | 0.0038 |    2.6 KB |
| SerializeToSyntaxTree | FootnoteDescription  |  2.456 us | 0.0489 us | 0.1052 us |  2.429 us |  0.3586 | 0.0038 |   2.95 KB |
| SerializeToSyntaxTree | FootnoteReference    |  1.714 us | 0.0355 us | 0.1017 us |  1.686 us |  0.2899 | 0.0038 |   2.38 KB |
| SerializeToSyntaxTree | FrontMatter          |  1.119 us | 0.0222 us | 0.0433 us |  1.111 us |  0.2575 | 0.0019 |   2.12 KB |
| SerializeToSyntaxTree | FrontMatter_2Entries |  4.515 us | 0.0843 us | 0.1798 us |  4.494 us |  0.5646 | 0.0076 |   4.62 KB |
| SerializeToSyntaxTree | HeadingSimple        |  1.324 us | 0.0224 us | 0.0249 us |  1.328 us |  0.2708 | 0.0038 |   2.23 KB |
| SerializeToSyntaxTree | Heading_1            |  1.258 us | 0.0251 us | 0.0666 us |  1.254 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Heading_2            |  1.237 us | 0.0240 us | 0.0366 us |  1.231 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Heading_3            |  1.243 us | 0.0246 us | 0.0375 us |  1.251 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Heading_4            |  1.254 us | 0.0245 us | 0.0375 us |  1.255 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Heading_5            |  1.241 us | 0.0244 us | 0.0569 us |  1.240 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Heading_6            |  1.207 us | 0.0237 us | 0.0233 us |  1.208 us |  0.2613 | 0.0019 |   2.15 KB |
| SerializeToSyntaxTree | Highlight            |  2.317 us | 0.0460 us | 0.0565 us |  2.333 us |  0.3204 |      - |   2.63 KB |
| SerializeToSyntaxTree | HorizontalRule       |  1.014 us | 0.0201 us | 0.0216 us |  1.008 us |  0.2155 | 0.0019 |   1.77 KB |
| SerializeToSyntaxTree | HtmlBlock            |  2.298 us | 0.0458 us | 0.0510 us |  2.301 us |  0.3281 | 0.0038 |    2.7 KB |
| SerializeToSyntaxTree | Italic               |  2.160 us | 0.0431 us | 0.0670 us |  2.152 us |  0.3319 | 0.0038 |   2.73 KB |
| SerializeToSyntaxTree | Italic_2InLine       |  3.217 us | 0.0621 us | 0.0871 us |  3.229 us |  0.4539 | 0.0076 |   3.71 KB |
| SerializeToSyntaxTree | Link                 |  3.032 us | 0.0591 us | 0.0607 us |  3.012 us |  0.3738 | 0.0038 |   3.06 KB |
| SerializeToSyntaxTree | Link_Nested          |  4.203 us | 0.0802 us | 0.0823 us |  4.192 us |  0.3662 |      - |   2.99 KB |
| SerializeToSyntaxTree | List_Ordered         |  2.959 us | 0.0592 us | 0.1539 us |  2.909 us |  0.5722 | 0.0153 |   4.69 KB |
| SerializeToSyntaxTree | List_Ordered_100     | 87.148 us | 1.7098 us | 1.9005 us | 87.444 us | 16.8457 | 8.3008 |  138.3 KB |
| SerializeToSyntaxTree | List_Ordered_50      | 42.935 us | 0.8443 us | 1.3392 us | 42.541 us |  8.4839 | 2.2583 |  69.75 KB |
| SerializeToSyntaxTree | List_Task            |  2.078 us | 0.0416 us | 0.0812 us |  2.072 us |  0.4044 | 0.0076 |    3.3 KB |
| SerializeToSyntaxTree | List_Task_100        | 87.108 us | 1.5397 us | 2.3972 us | 86.658 us | 16.9678 | 7.3242 | 139.48 KB |
| SerializeToSyntaxTree | List_Task_50         | 44.031 us | 0.7148 us | 0.6336 us | 44.150 us |  8.6060 | 2.1362 |  70.34 KB |
| SerializeToSyntaxTree | List_Task_checked    |  2.053 us | 0.0354 us | 0.0485 us |  2.057 us |  0.4044 | 0.0076 |    3.3 KB |
| SerializeToSyntaxTree | List_(...)d_100 [21] | 87.349 us | 1.7365 us | 1.7054 us | 87.212 us | 16.9678 | 7.3242 | 139.48 KB |
| SerializeToSyntaxTree | List_Task_checked_50 | 42.327 us | 0.7895 us | 0.7385 us | 42.368 us |  8.6060 | 2.1362 |  70.34 KB |
| SerializeToSyntaxTree | List_UnOrdered       |  2.812 us | 0.0431 us | 0.0360 us |  2.820 us |  0.5569 | 0.0114 |   4.58 KB |
| SerializeToSyntaxTree | List_UnOrdered_100   | 82.840 us | 1.5435 us | 2.9738 us | 82.489 us | 16.1133 | 6.2256 | 132.44 KB |
| SerializeToSyntaxTree | List_UnOrdered_50    | 40.684 us | 0.7123 us | 1.4711 us | 40.342 us |  8.1787 | 2.0752 |  66.82 KB |
| SerializeToSyntaxTree | Mixed_RealWorld      | 18.576 us | 0.3666 us | 0.5257 us | 18.554 us |  2.1362 | 0.1221 |  17.62 KB |
| SerializeToSyntaxTree | NewLine              |  2.545 us | 0.0498 us | 0.0805 us |  2.527 us |  0.3738 | 0.0038 |   3.05 KB |
| SerializeToSyntaxTree | Paragraph            |  1.651 us | 0.0327 us | 0.0402 us |  1.649 us |  0.2422 | 0.0019 |   1.98 KB |
| SerializeToSyntaxTree | Paragraph_Base       |  1.670 us | 0.0285 us | 0.0253 us |  1.665 us |  0.2403 |      - |   1.98 KB |
| SerializeToSyntaxTree | Strikethrough        |  2.262 us | 0.0420 us | 0.0615 us |  2.259 us |  0.3319 | 0.0038 |   2.71 KB |
| SerializeToSyntaxTree | Strik(...)nLine [21] |  3.395 us | 0.0663 us | 0.0929 us |  3.377 us |  0.4387 | 0.0038 |    3.6 KB |
| SerializeToSyntaxTree | Subscript            |  2.100 us | 0.0410 us | 0.0686 us |  2.081 us |  0.3357 | 0.0038 |   2.74 KB |
| SerializeToSyntaxTree | Subscript_2InLine    |  3.111 us | 0.0612 us | 0.0705 us |  3.093 us |  0.4539 | 0.0076 |   3.72 KB |
| SerializeToSyntaxTree | Superscript          |  2.091 us | 0.0413 us | 0.0593 us |  2.084 us |  0.3357 | 0.0038 |   2.74 KB |
| SerializeToSyntaxTree | Superscript_2InLine  |  3.559 us | 0.0707 us | 0.1824 us |  3.587 us |  0.4539 | 0.0076 |   3.72 KB |
| SerializeToSyntaxTree | Table                |  3.975 us | 0.0784 us | 0.0991 us |  3.999 us |  0.4959 | 0.0076 |   4.11 KB |
| SerializeToSyntaxTree | Table_2Rows          |  4.379 us | 0.0865 us | 0.0961 us |  4.376 us |  0.6180 | 0.0153 |   5.06 KB |
| SerializeToSyntaxTree | Table_3Rows          |  5.331 us | 0.1030 us | 0.1510 us |  5.328 us |  0.7324 | 0.0229 |   6.02 KB |
| SerializeToSyntaxTree | Tag                  |  1.531 us | 0.0304 us | 0.0436 us |  1.522 us |  0.2918 | 0.0038 |   2.39 KB |
| SerializeToSyntaxTree | Template             |  1.924 us | 0.0382 us | 0.0616 us |  1.905 us |  0.3147 | 0.0038 |   2.58 KB |
| SerializeToSyntaxTree | Underline            |  2.150 us | 0.0414 us | 0.0460 us |  2.150 us |  0.3319 | 0.0038 |   2.73 KB |
| SerializeToSyntaxTree | User                 |  1.545 us | 0.0298 us | 0.0306 us |  1.545 us |  0.2918 | 0.0038 |   2.39 KB |
| SerializeToSyntaxTree | WikiLink             |  1.781 us | 0.0355 us | 0.0349 us |  1.779 us |  0.2918 | 0.0038 |    2.4 KB |
| SerializeToSyntaxTree | Wrapper              |  2.492 us | 0.0422 us | 0.0657 us |  2.486 us |  0.3700 | 0.0038 |   3.04 KB |

