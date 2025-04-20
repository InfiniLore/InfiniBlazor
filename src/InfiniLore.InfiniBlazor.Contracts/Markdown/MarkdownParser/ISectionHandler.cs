// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ISectionHandler {
    ParserOrigin SkipOnOrigin { get; }
    
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser);
}
