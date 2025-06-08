// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CommentBuilder(GeneratorStringBuilder builder) {
    public GeneratorStringBuilder AppendCSharpCommentBlock(string comment, int maxLineLength = 120) {
        string lineString = new('-',  maxLineLength - builder.IndentAmount - 3); // 3 for the // 
        builder.AppendLine($"// {lineString}");
        builder.AppendLine($"// {comment}");
        builder.AppendLine($"// {lineString}");
        return builder;
    }

    public GeneratorStringBuilder AppendCSharpCommentLine(string comment) {
        return builder.AppendLine($"// {comment}");
    }

    public GeneratorStringBuilder AppendRazorCommentBlock(string comment, int maxLineLength = 120) {
        string lineString = new('-', maxLineLength - builder.IndentAmount - 6); // 3 for the @* and *@
        builder.AppendLine($"@* {lineString} *@");
        builder.AppendLine($"@* {comment} *@");
        builder.AppendLine($"@* {lineString} *@");
        return builder;
    }
    
    public GeneratorStringBuilder AppendRazorCommentLine(string comment) {
        return builder.AppendLine($"@* {comment} *@");
    }
}
