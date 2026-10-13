namespace GameFrameX.Setting.Runtime
{
    internal sealed class KuaiShouMiniGameSettingStorage : ISettingStorageBackend
    {
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Register()
        {
            SettingStorageBackendFactory.RegisterMiniGameBackend(() => new KuaiShouMiniGameSettingStorage());
        }

        public bool Save()
        {
            return true;
        }

        public bool HasKey(string key)
        {
            return KSWASM.KSBase.StorageHasKeySync(key);
        }

        public bool DeleteKey(string key)
        {
            if (!KSWASM.KSBase.StorageHasKeySync(key))
            {
                return false;
            }

            KSWASM.KSBase.StorageDeleteKeySync(key);
            return true;
        }

        public void DeleteAll()
        {
            KSWASM.KSBase.StorageDeleteAllSync();
        }

        public int GetInt(string key, int defaultValue)
        {
            return KSWASM.KSBase.StorageGetIntSync(key, defaultValue);
        }

        public void SetInt(string key, int value)
        {
            KSWASM.KSBase.StorageSetIntSync(key, value);
        }

        public float GetFloat(string key, float defaultValue)
        {
            return KSWASM.KSBase.StorageGetFloatSync(key, defaultValue);
        }

        public void SetFloat(string key, float value)
        {
            KSWASM.KSBase.StorageSetFloatSync(key, value);
        }

        public string GetString(string key, string defaultValue)
        {
            return KSWASM.KSBase.StorageGetStringSync(key, defaultValue);
        }

        public void SetString(string key, string value)
        {
            KSWASM.KSBase.StorageSetStringSync(key, value);
        }
    }
}
