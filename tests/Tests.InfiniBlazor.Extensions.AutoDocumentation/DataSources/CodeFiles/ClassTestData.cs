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
    private const string ExpectedSource_Class = """
        public class Class {
            
        }
        """;
    private const string ExpectedSource_ClassInClass = """
        public class Class {
            
        }
        """;
    private const string ExpectedSource_ClassInClassInClass = """
        public class Class {
            
        }
        """;
    

    public static IEnumerable<Func<AutoDocmentationTestData>> GetTestData() {
        yield return () => new AutoDocmentationTestData("ClassTest", true, new AutoDocumentationFragment([], [ExpectedSource_Class]));
        yield return () => new AutoDocmentationTestData("ClassInClass", true, new AutoDocumentationFragment([], [ExpectedSource_ClassInClass]));
        yield return () => new AutoDocmentationTestData("ClassInClassInClass", true, new AutoDocumentationFragment([], [ExpectedSource_ClassInClassInClass]));
    }
}
