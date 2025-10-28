// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.CodeBlock;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniBlazorCodeBlockLanguageHandler(ILogger<InfiniBlazorCodeBlockLanguageHandler> logger) : InfiniComponentBase, ICodeBlockLanguageHandler {
    [Parameter] public string Content { get; set; } = string.Empty;
    
    [GeneratedRegex("""
    (?:<(?<tag>\w+)\ *(?<attr>\b.*?)/>)
    |(?:
      <(?<tag>\w+)\ *(?<attr>\b[^>]*?)/?>
      \n*?
      (?<body>
      (?>
        [^<]+
        | <(?<open>\k<tag>)\b[^>]*>
        | </(?<-open>\k<tag>)>
        | <(?!/?\k<tag>\b)[^>]+>
      )*)
      (?(open)(?!))
      (</\k<tag>>)
    )
    """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex HtmlExtractor { get; }
    
    [GeneratedRegex("""
    (?<key>[\w-]+)(?:=(?:
      (?:(?<a>["'])(?<value0>(?:.(?!\k<a>\s+(?:\S+)=|\s*\/?[>]|\k<a>))+.)\k<a>)
      |(?<value1>((?:.(?!\s+(?:\S+)=|\s*\/?[>"']))+.))
    ))?
    """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex AttributeExtractor { get; }
    
    private List<(Type, Dictionary<string, object?>)> _attributes = new();
    
    // TODO add other elements and also default html element handlers
    private static readonly FrozenDictionary<string, Type> ComponentTypes = new Dictionary<string, Type>() {
        [nameof(InfiniHeading)]  = typeof(InfiniHeading),
        [nameof(InfiniHeading1)] = typeof(InfiniHeading1),
        [nameof(InfiniHeading2)] = typeof(InfiniHeading2),
        [nameof(InfiniHeading3)] = typeof(InfiniHeading3),
        [nameof(InfiniHeading4)] = typeof(InfiniHeading4),
        [nameof(InfiniHeading5)] = typeof(InfiniHeading5),
        [nameof(InfiniHeading6)] = typeof(InfiniHeading6),
        [nameof(InfiniCodeBlock)] = typeof(InfiniCodeBlock),
    }.ToFrozenDictionary();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected override async Task OnInitializedAsync() {
        await base.OnInitializedAsync();

        _attributes = new List<(Type, Dictionary<string, object?>)>();
        MatchCollection matches = HtmlExtractor.Matches(Content);
        
        foreach (Match match in matches) {
            string tag = match.Groups["tag"].Value;
            string attr = match.Groups["attr"].Value;
            string body = match.Groups["body"].Value;

            if (!ComponentTypes.TryGetValue(tag, out Type? componentType)) {
                logger.Warning("Unknown tag: {tag}", tag);
                continue;           
            }
            
            MatchCollection attrMatches = AttributeExtractor.Matches(attr);
            Dictionary<string, object?> attributes = new();
            foreach (Match attrMatch in attrMatches) {
                string key = attrMatch.Groups["key"].Value;
                if (attrMatch.Groups["value0"].TryGetValue(out string? value0)) attributes.Add(key, value0);
                else if (attrMatch.Groups["value1"].TryGetValue(out string? value1)) attributes.Add(key, value1);
            }

            switch (tag) {
                case nameof(InfiniCodeBlock): {
                    attributes.Add(nameof(InfiniCodeBlock.DisableCustomLanguageHandling), true);
                    break;
                }

                case not null when body.IsNotNullOrWhiteSpace(): {
                    attributes.Add(
                        "ChildContent", 
                        new RenderFragment(builder => builder.AddContent(0, LineNormalization.NormalizeLineIndentation(body, out _)))
                    );
                    break;
                }
            }
            _attributes.Add((componentType, attributes));
        }

        if (!IsDisposed) await InvokeAsync(StateHasChanged);
    }
}

//  ```infiniblazor
//  <InfiniHeading1 class="text-red-500">Emote Data Editor</InfiniHeading1>
//  ```