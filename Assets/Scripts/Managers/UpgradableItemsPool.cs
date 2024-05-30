using System.Collections.Generic;
using System.Linq;
using Data;
using Environment;
using UnityEngine;

namespace Managers
{
    public class UpgradableItemsPool : MonoBehaviour
    {
        [SerializeField] private List<UpgradableItem> Items  = new List<UpgradableItem>();

        public UpgradableItem GetItem(ObjectData data)
        {
            return Items.FirstOrDefault((item) => item.GetItemID() == data.ID);
        }

        public List<UpgradableItem> GetAllItems()
        {
            return Items;
        }
    }
}
