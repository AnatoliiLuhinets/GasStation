using System;
using System.Text.RegularExpressions;
using Environment;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ObjectData", menuName = "ScriptableObjects/Data/ObjectData")]
    public class ObjectData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon;
        [field: SerializeField] public string Name;
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public UpgradableItem Prefab;
        
        [ContextMenu("Update ID")]
        private void UpdateID()
        {
            ID = "";
            
            var guid = Guid.NewGuid();
            var base64Id = Convert.ToBase64String(guid.ToByteArray());
            base64Id = Regex.Replace(base64Id, "[^a-zA-Z0-9]", "");
            ID = base64Id.Substring(0, Math.Min(15, base64Id.Length));
        }
    }
}
