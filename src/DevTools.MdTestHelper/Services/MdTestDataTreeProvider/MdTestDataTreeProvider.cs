// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.DataTrees;

namespace DevTools.MdTestHelper.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<MdTestDataTreeProvider>]
public class MdTestDataTreeProvider : IInfiniDataTreeProvider<MdTestDataTree> {
    
    public async ValueTask<MdTestDataTree?> TryGetDataTreeAsync(object? id = null) {
        throw new NotImplementedException();
    }
}
