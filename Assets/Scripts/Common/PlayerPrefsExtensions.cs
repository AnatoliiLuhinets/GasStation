using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Common
{
    public class PlayerPrefsExtensions
    {
        private const char DELIMITER = '|';

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;
            return PlayerPrefs.GetFloat(key);
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            if (!PlayerPrefs.HasKey(key)) return defaultValue;
            return PlayerPrefs.GetInt(key) == 1;
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public static void SetArray<T>(string key, T[] array)
        {
            string serializedArray = string.Join(DELIMITER.ToString(), array.Select(a => a.ToString()));
            PlayerPrefs.SetString(key, serializedArray);
        }

        public static T[] GetArray<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key)) return new T[0];

            string serializedArray = PlayerPrefs.GetString(key);
            string[] stringArray = serializedArray.Split(DELIMITER);

            T[] resultArray = new T[stringArray.Length];
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (typeof(T) == typeof(float))
                {
                    float.TryParse(stringArray[i], out float value);
                    resultArray[i] = (T)(object)value;
                }
                else if (typeof(T) == typeof(int))
                {
                    int.TryParse(stringArray[i], out int value);
                    resultArray[i] = (T)(object)value;
                }
                else if (typeof(T) == typeof(string))
                {
                    resultArray[i] = (T)(object)stringArray[i];
                }
            }

            return resultArray;
        }

        public static void SetDictionary<K, V>(string key, Dictionary<K, V> dictionary)
        {
            K[] keys = dictionary.Keys.ToArray();
            V[] values = dictionary.Values.ToArray();

            SetArrayPair(key, keys, values);
        }

        public static Dictionary<K, V> GetDictionary<K, V>(string key)
        {
            if (TryGetArrayPair(key, out K[] keys, out V[] values))
            {
                return keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            }

            return new Dictionary<K, V>();
        }

        public static void SetArrayPair<K, V>(string key, K[] keys, V[] values)
        {
            SetObject(key, new DictionaryWrap<K, V>(keys, values));
        }

        public static bool TryGetArrayPair<K, V>(string key, out K[] keys, out V[] values)
        {
            DictionaryWrap<K, V> obj = GetObject<DictionaryWrap<K, V>>(key);
            if (obj == null)
            {
                keys = null;
                values = null;
                return false;
            }
            else
            {
                keys = obj.Keys;
                values = obj.Values;
                return true;
            }
        }

        private static void SetObject<T>(string key, T obj)
        {
            string jsonString = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key, jsonString);
        }

        private static T GetObject<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key)) return default(T);

            string jsonString = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(jsonString);
        }

        [Serializable]
        private class DictionaryWrap<K, V>
        {
            public K[] keys;
            public V[] values;

            public DictionaryWrap(K[] keys, V[] values)
            {
                this.keys = keys;
                this.values = values;
            }

            public K[] Keys => keys;
            public V[] Values => values;
        }
    }
}
