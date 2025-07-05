// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Text;

namespace InfiniLore.InfiniBlazor.Pooling.Policies;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StringBuilderPolicy : PooledObjectPolicy<StringBuilder> {
    private const int InitialCapacity = 1024;
    
    public override StringBuilder Create() => new(InitialCapacity);
    public override bool Return(StringBuilder obj) {
        obj.Clear();
        return true;
    }
}
