using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class GameObjectUtils
    {
        public static bool Instantiate<T>(string id) where T : Object
        {
            return GameObjectUtils.Instantiate<T>(id, null);
        }

        public static bool Instantiate<T>(string id, out T t) where T : Object
        {
            return GameObjectUtils.Instantiate<T>(id, null, out t);
        }

        public static bool Instantiate<T>(string id, Transform root) where T : Object
        {
            return GameObjectUtils.Instantiate<T>(id, root, out _);
        }

        public static bool Instantiate<T>(string id, Transform root, out T t) where T : Object
        {
            t = null;

            if (PrefabUtils.HasPrefab<T>(id, out Prefab prefab))
            {
                return prefab.Instantiate<T>(root, out t);
            }

            return false;
        }

        public static bool IsEnabled(this GameObject gameObject)
        {
            if (gameObject is not null)
            {
                return gameObject.activeSelf;
            }

            return false;
        }

        public static bool IsEnabled(this GameObject gameObject, bool isEnabled)
        {
            if (gameObject is not null)
            {
                if (gameObject.activeSelf != isEnabled)
                {
                    gameObject.SetActive(isEnabled);
                }

                return gameObject.activeSelf;
            }

            return false;
        }
    }
}
