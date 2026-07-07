using UnityEngine;

namespace Core.Services.Storage
{
    public class PlayerPrefsStorage : IStorage
    {
        public T Get<T>(string key, T defaultValue = default)
        {
            if (PlayerPrefs.HasKey(key) is false)
            {
                return defaultValue;
            }

            if (typeof(T) == typeof(int))
            {
                return (T)(object)PlayerPrefs.GetInt(key);
            }
            if (typeof(T) == typeof(float))
            {
                return (T)(object)PlayerPrefs.GetFloat(key);
            }
            if (typeof(T) == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(key);
            }
            if (typeof(T) == typeof(bool))
            {
                return (T)(object)(PlayerPrefs.GetInt(key) != 0);
            }


            return JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }

        public void Set<T>(string key, T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(key, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(key, s);
                    break;
                case bool b:
                    PlayerPrefs.SetInt(key, b ? 1 : 0);
                    break;
                default:
                    PlayerPrefs.SetString(key, JsonUtility.ToJson(value));
                    break;
            }
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void DeleteKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}
