// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.CodeFiles;

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
    

    public static IEnumerable<Func<AutoDocmentationTestData>> GetTestData() {
        yield return () => new AutoDocmentationTestData("ClassTest", true, new AutoDocumentationFragment([], [ExpectedSourceClass]));
        yield return () => new AutoDocmentationTestData("ClassInClass", true, new AutoDocumentationFragment([], [ExpectedSourceClassInClass]));
        yield return () => new AutoDocmentationTestData("ClassInClassInClass", true, new AutoDocumentationFragment([], [ExpectedSourceClassInClassInClass]));
    }
}
