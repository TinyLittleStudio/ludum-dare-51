using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class MenuUI : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform containerTransition;

        private void Awake()
        {
            AwakeContainer();
        }

        private void AwakeContainer()
        {
            if (container != null)
            {
                container.IsEnabled(true);
            }

            if (containerTransition != null)
            {
                containerTransition.IsEnabled(true);
            }
        }

        public override string ToString() => $"MenUI ()";
    }
}
