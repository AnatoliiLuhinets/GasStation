using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class ItemEntry
    {
        public string ObjectID;
        public int CurrentID;
    }

    [Serializable]
    public class AllItemsState
    {
        public List<ItemEntry> Items = new List<ItemEntry>();
    }

    [Serializable]
    public class UserMoney
    {
        public int MoneyCount;
    }

    public static class SaveService
    {
        public static void SaveItem(string objectId, int currentId, string path)
        {
            AllItemsState allItemsState;

            if (File.Exists(path))
            {
                string allItemsJson = File.ReadAllText(path);
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
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save item: {ex.Message}");
            }
        }

        public static int? LoadItem(string objectId, string path)
        {
            if (File.Exists(path))
            {
                string allItemsJson = File.ReadAllText(path);
                AllItemsState allItemsState = JsonUtility.FromJson<AllItemsState>(allItemsJson);
                if (allItemsState != null)
                {
                    var item = allItemsState.Items.Find(x => x.ObjectID == objectId);
                    if (item != null)
                    {
                        return item.CurrentID;
                    }
                }
            }
            return null;
        }

        public static void SaveUserMoney(int moneyCount, string path)
        {
            UserMoney userMoney = new UserMoney { MoneyCount = moneyCount };
            string json = JsonUtility.ToJson(userMoney, true);
            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save user money: {ex.Message}");
            }
        }

        public static int? LoadUserMoney(string path)
        {
            if (File.Exists(path))
            {
                string moneyJson = File.ReadAllText(path);
                UserMoney userMoney = JsonUtility.FromJson<UserMoney>(moneyJson);
                if (userMoney != null)
                {
                    return userMoney.MoneyCount;
                }
            }
            return null;
        }
    }
}
