// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;

namespace InfiniBlazorTests.Extensions.AutoDocumentation.DataSources.CodeFiles;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[AutoDocument("ClassTest")] public class Class {
    
}

public class ClassInClass {
    [AutoDocument("ClassInClass")] public class Class {
    
    }
}

public class ClassInClassInClass {
    public class ClassInClass {
        [AutoDocument("ClassInClassInClass")] public class Class {
    
        }
    }
}

public class ClassTestData {
    private const string ExpectedSourceClass = """
        public class Class {
        }
        """;
    private const string ExpectedSourceClassInClass = """
        public class Class {
        }
        """;
    private const string ExpectedSourceClassInClassInClass = """
        public class Class {
        }
        """;
    

    public static IEnumerable<Func<AutoDocumentationTestData>> GetTestData() {
        yield return () => new AutoDocumentationTestData("ClassTest", true, new AutoDocumentationFragment([], [ExpectedSourceClass]));
        yield return () => new AutoDocumentationTestData("ClassInClass", true, new AutoDocumentationFragment([], [ExpectedSourceClassInClass]));
        yield return () => new AutoDocumentationTestData("ClassInClassInClass", true, new AutoDocumentationFragment([], [ExpectedSourceClassInClassInClass]));
    }
}
