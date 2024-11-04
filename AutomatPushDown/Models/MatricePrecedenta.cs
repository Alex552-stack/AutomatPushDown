using System.Runtime.CompilerServices;

namespace AutomatPushDown.Models;

public class MatricePrecedenta
{
    private Dictionary<string, Dictionary<string, int>> _matrix = new();
    public Dictionary<string, int> this[string key]
    {
        get
        {
            if (!_matrix.ContainsKey(key))
            {
                _matrix[key] = new Dictionary<string, int>();
            }
            return _matrix[key];
        }
    }

}