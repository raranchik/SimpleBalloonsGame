using UnityEngine;

namespace Core.Base.Save
{
    public class SaveHelper
    {
        public void SaveAll()
        {
            PlayerPrefs.Save();
        }

        public void Write<T>(string key, T value)
        {
            var json = JsonUtility.ToJson(value);
            PlayerPrefs.SetString(key, json);
        }

        public T Read<T>(string key)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                return default;
            }

            var json = PlayerPrefs.GetString(key);
            var value = JsonUtility.FromJson<T>(json);
            return value;
        }
    }
}