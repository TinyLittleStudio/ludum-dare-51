using System.Collections.Generic;
using UnityEngine;

using Object = UnityEngine.Object;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class PrefabUtils
    {
        private static List<Prefab> prefabs;

        public static Prefab GetPrefab<T>(string id) where T : Object
        {
            if (PrefabUtils.prefabs == null)
            {
                PrefabUtils.prefabs = new List<Prefab>();
            }

            if (PrefabUtils.prefabs != null)
            {
                foreach (Prefab prefab in PrefabUtils.prefabs)
                {
                    if (StringUtils.EqualsIgnoreCase(prefab.Id, id))
                    {
                        return prefab;
                    }
                }
            }

            return null;
        }

        public static Prefab GetPrefabOrLoadFromMemory<T>(string id) where T : Object
        {
            if (PrefabUtils.prefabs == null)
            {
                PrefabUtils.prefabs = new List<Prefab>();
            }

            if (PrefabUtils.prefabs != null)
            {
                Prefab prefab = PrefabUtils.GetPrefab<T>(id);

                if (prefab == null)
                {
                    prefab = new Prefab(id);
                    prefab.SetSource(Resources.Load<T>(id));

                    PrefabUtils.prefabs.Add(prefab);
                }

                if (prefab != null)
                {
                    return prefab;
                }
            }

            return null;
        }

        public static bool HasPrefab<T>(string id) where T : Object
        {
            Prefab prefab;

            prefab = PrefabUtils.GetPrefabOrLoadFromMemory<T>(id);

            return prefab != null;
        }

        public static bool HasPrefab<T>(string id, out Prefab prefab) where T : Object
        {
            prefab = PrefabUtils.GetPrefabOrLoadFromMemory<T>(id);

            return prefab != null;
        }
    }
}
