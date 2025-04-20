// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Workfloor.InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class Node {
    public string Name { get; set; } = string.Empty;
    public string NewContent { get; set; } = string.Empty;
    public Range OriginalContent { get; set; } = new();
    
    public List<Node> Children { get; } = new();
    public Node Parent { get; private set; }
    
    public Node AddChild(string? name = null, Range range = default) {
        var child = new Node {
            Name = name ?? string.Empty,
            OriginalContent = range,
        };
        Children.Add(child);
        child.Parent = this;
        return child;
    }
}

[Flags]
public enum Origin {
    Undefined = 0,
    DoNotSkip = 1 << 0,
    Bold = 1 << 1,
    Italic = 1 << 2,
}

public class DataBox {
    public Node Node { get; set; }
    public Origin Origin { get; set; }
    public Match? Match { get; set; }
    public string? RawInput { get; set; }

    [MemberNotNull(nameof(Match))] public bool IsMatch { get; set; }
    [MemberNotNull(nameof(RawInput))] public bool IsRawInput { get; set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructor
    // -----------------------------------------------------------------------------------------------------------------
    public DataBox(Match match, Node node, Origin origin) {
        Match = match;
        Node = node;
        Origin = origin;
        IsMatch = true;
        IsRawInput = false;
    }
    
    public DataBox(string rawInput, Node node, Origin origin) {
        RawInput = rawInput;
        Node = node;
        Origin = origin;
        IsMatch = false;
        IsRawInput = true;
    }
}

public abstract class GroupHandler {
    public abstract Origin SkipOnOrigin { get; }
    public abstract void HandleMatch(Match fullMatch, Group group, Node currentNode, Stack<DataBox> stack, Origin origin);
}

public class ParagraphHandler : GroupHandler {
    public override Origin SkipOnOrigin => Origin.DoNotSkip;

    public override void HandleMatch(Match fullMatch, Group group, Node currentNode, Stack<DataBox> stack, Origin origin) {
        Node node = currentNode.AddChild(name: "p");
        
        Program.ExtractMatchesToStack(group, stack, node, origin | SkipOnOrigin);
    }
}

public class BoldHandler : GroupHandler {
    public override Origin SkipOnOrigin => Origin.Bold;

    public override void HandleMatch(Match fullMatch, Group group, Node currentNode, Stack<DataBox> stack, Origin origin) {
        Node node = currentNode.AddChild(name: "strong");
        node.NewContent = fullMatch.Groups["b"].Value;
        Program.ExtractMatchesToStack(group, stack, node, origin | SkipOnOrigin);
    }
}

public class ItalicHandler : GroupHandler {
    public override Origin SkipOnOrigin => Origin.Italic;

    public override void HandleMatch(Match fullMatch, Group group, Node currentNode, Stack<DataBox> stack, Origin origin) {
        Node node = currentNode.AddChild(name: "em");
        node.NewContent = fullMatch.Groups["i"].Value;
        Program.ExtractMatchesToStack(group, stack, node, origin | SkipOnOrigin);
    }
}

public static class Program {
    public static void ExtractMatchesToStack(Group group, Stack<DataBox> stack, Node node, Origin origin) {        
        MatchCollection matches = MarkdownRegexLib.SinglelineStructuresRegex.Matches(group.Value);
        stack.EnsureCapacity(stack.Count + matches.Count);
    
        string value = group.Value;
        int currentIndex = group.Length;

        // Process matches in reverse order for stack
        IOrderedEnumerable<Match> matchesList = matches.ToImmutableArray().OrderByDescending(m => m.Index);
        foreach (Match match in matchesList) {
            int matchEnd = match.Index + match.Length;
        
            // If there's text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                AddRawInputToStack(value[matchEnd..currentIndex], stack, node, origin);
            }
        
            stack.Push(new DataBox(match, node, origin));
            currentIndex = match.Index;
        }

        // Handle any remaining text before the first match
        if (currentIndex > 0) {
            AddRawInputToStack(value[0..currentIndex], stack, node, origin);
        }
    }

    public static void AddRawInputToStack(string rawInput, Stack<DataBox> stack, Node node, Origin origin) {
        stack.Push(new DataBox(rawInput, node, origin)); 
    }
    
    private static readonly Dictionary<string, GroupHandler> GroupHandlers = new() {
        { "paragraph", new ParagraphHandler() },
        { "bold", new BoldHandler() },
        { "italic", new ItalicHandler() }
    };

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Main() {
        string markdown = "**bold** and *italics*";
        var rootNode = new Node();
        
        var matchesStack = new Stack<DataBox>();
        MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(markdown);
        matchesStack.EnsureCapacity(matches.Count);
        IEnumerable<Match> matchesList = matches.Reverse();
        foreach (Match match in matchesList) {
            matchesStack.Push(new DataBox(match, rootNode, Origin.Undefined));
        }
        
        while (matchesStack.TryPop(out DataBox? box)) {
            // Extract what we already have from the stack
            Node currentNode = box.Node;
            Origin origin = box.Origin;
            
            
        }
        
        
        Console.WriteLine(rootNode.ToString());
    }
}


