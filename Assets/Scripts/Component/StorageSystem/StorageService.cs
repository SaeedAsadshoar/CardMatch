using Extension;
using UnityEngine;

namespace Component.StorageSystem
{
    public static class StorageService
    {
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }

        public static void SetData(string key, string value)
        {
            PlayerPrefs.SetString(EncryptExtension.Encrypt(key), EncryptExtension.Encrypt(value));
            PlayerPrefs.Save();
        }

        public static void SetData(string key, int value)
        {
            PlayerPrefs.SetString(EncryptExtension.Encrypt(key), EncryptExtension.Encrypt(value.ToString()));
            PlayerPrefs.Save();
        }

        public static void SetData(string key, float value)
        {
            PlayerPrefs.SetString(EncryptExtension.Encrypt(key), EncryptExtension.Encrypt(value.ToString()));
            PlayerPrefs.Save();
        }

        public static string GetData(string key, string defaultValue = "-1")
        {
            key = EncryptExtension.Encrypt(key);
            return PlayerPrefs.HasKey(key) ? EncryptExtension.Decrypt(PlayerPrefs.GetString(key)) : defaultValue;
        }

        public static int GetData(string key, int defaultValue = -1)
        {
            key = EncryptExtension.Encrypt(key);
            return PlayerPrefs.HasKey(key) ? int.Parse(EncryptExtension.Decrypt(PlayerPrefs.GetString(key))) : defaultValue;
        }

        public static float GetData(string key, float defaultValue = -1)
        {
            key = EncryptExtension.Encrypt(key);
            return PlayerPrefs.HasKey(key) ? float.Parse(EncryptExtension.Decrypt(PlayerPrefs.GetString(key))) : defaultValue;
        }
    }
}