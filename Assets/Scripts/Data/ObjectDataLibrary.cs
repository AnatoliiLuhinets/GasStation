using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObjectDataLibrary", menuName = "ScriptableObjects/Data/ObjectDataLibrary")]
    public class ObjectDataLibrary : ScriptableObject
    {
        [field: SerializeField] public List<ObjectData> ObjectDatas;
    }
}
