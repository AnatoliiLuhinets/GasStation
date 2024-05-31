using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interfaces
{
    public interface ISavable
    {
        UniTask Save();
        UniTask Load();
    }
}
