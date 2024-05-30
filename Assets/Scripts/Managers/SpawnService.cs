using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    public class SpawnService<T> where T : Component
    {
        public async UniTask<T> SpawnPrefab(T prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null)
            {
                return null;
            }
            
            T instance = GameObject.Instantiate(prefab, position, rotation);
            
            await UniTask.Yield();

            return instance;
        }
    }
}