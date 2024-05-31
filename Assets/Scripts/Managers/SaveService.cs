using System;
using System.Collections.Generic;
using System.IO;
using Constants;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class ItemEntry
    {
        [field: SerializeField] public string ObjectID { get; set; }
        [field: SerializeField] public int CurrentID { get; set; }
    }

    [Serializable]
    public class AllItemsState
    {
        [field: SerializeField] public List<ItemEntry> Items = new List<ItemEntry>();
    }

    [Serializable]
    public class UserMoney
    {
        [field: SerializeField] public int MoneyCount { get; set; }
    }

    public static class SaveService
    {
        public static async UniTask SaveItem(string objectId, int currentId, string path)
        {
            AllItemsState allItemsState;

            if (File.Exists(path))
            {
                string allItemsJson = await File.ReadAllTextAsync(path);
                allItemsState = JsonUtility.FromJson<AllItemsState>(allItemsJson) ?? new AllItemsState();
            }
            else
            {
                allItemsState = new AllItemsState();
            }

            var item = allItemsState.Items.Find(x => x.ObjectID == objectId);
            if (item == null)
            {
                item = new ItemEntry { ObjectID = objectId, CurrentID = currentId };
                allItemsState.Items.Add(item);
            }
            else
            {
                item.CurrentID = currentId;
            }

            var json = JsonUtility.ToJson(allItemsState, true);
            
            try
            {
                await File.WriteAllTextAsync(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save item: {ex.Message}");
            }
        }

        public static int LoadItem(string objectId, string path)
        {
            try
            {
                string allItemsJson = File.ReadAllText(path);
                AllItemsState allItemsState = JsonUtility.FromJson<AllItemsState>(allItemsJson);

                var item = allItemsState.Items.Find(x => x.ObjectID == objectId);
            
                return item.CurrentID;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return Consts.Values.DefaultUpgradeID;
            }
        }

        public static async UniTask SaveUserProgress(int moneyCount, string path)
        {
            UserMoney userMoney = new UserMoney { MoneyCount = moneyCount };
            string json = JsonUtility.ToJson(userMoney, true);
            try
            {
                await File.WriteAllTextAsync(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save user money: {ex.Message}");
            }
        }

        public static int LoadUserProgress(string path)
        {
            if (!HasFile(path))
            {
                return Consts.Values.DefaultMoneyCount;
            }
            
            string moneyJson = File.ReadAllText(path);
            UserMoney userMoney = JsonUtility.FromJson<UserMoney>(moneyJson);
            
            return userMoney.MoneyCount;
        }

        private static bool HasFile(string path)
        {
            return File.Exists(path);
        }
    }
}
