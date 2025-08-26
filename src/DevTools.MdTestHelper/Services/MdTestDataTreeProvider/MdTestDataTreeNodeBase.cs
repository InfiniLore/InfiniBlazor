// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.DataTrees;
using Microsoft.AspNetCore.Components;

namespace DevTools.MdTestHelper.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class MdTestDataTreeNodeBase : IInfiniDataTreeNode {
    public string VisibleName => throw new NotImplementedException();
    public EventCallback OnClickCallback => throw new NotImplementedException();
    public IEnumerable<IInfiniDataTreeNode> DataNodes => throw new NotImplementedException();
}
