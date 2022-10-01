using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class GameObjectComponentUtils
    {
        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return GameObjectComponentUtils.HasComponent<T>(gameObject, out _);
        }

        public static bool HasComponent<T>(this GameObject gameObject, out T component) where T : Component
        {
            if (gameObject is not null)
            {
                component = gameObject.GetComponent<T>();
            }
            else
            {
                component = null;
            }

            return component != null;
        }

        public static bool IsEnabled(this Component component)
        {
            if (component is not null)
            {
                if (component.gameObject is not null)
                {
                    return component.gameObject.activeSelf;
                }
            }

            return false;
        }

        public static bool IsEnabled(this Component component, bool isEnabled)
        {
            if (component is not null)
            {
                if (component.gameObject is not null)
                {
                    if (component.gameObject.activeSelf != isEnabled)
                    {
                        component.gameObject.SetActive(isEnabled);
                    }

                    return component.gameObject.activeSelf;
                }
            }

            return false;
        }
    }
}
