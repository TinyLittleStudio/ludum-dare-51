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

        private void Start()
        {
            StartMenu();
        }

        private void StartMenu()
        {
            AudioUtils.Play(IDs.AUDIO_ID__START_APPLICATION_MENU, 0.0f, 0.0f);
        }

        public void InvokeMenu()
        {
            AudioUtils.Play(IDs.AUDIO_ID__BUTTON, 0.0f, 0.0f);

            if (!IsMenu)
            {
                IsMenu = true;

                Watch.NewWatch(0.50f, (int tick, bool isFinished) =>
                {
                    if (isFinished)
                    {
                        IsMenu = false;

                        Manager.GetManager().InvokeMenu();
                    }
                });
            }
        }

        public void InvokeGame()
        {
            AudioUtils.Play(IDs.AUDIO_ID__BUTTON, 0.0f, 0.0f);

            if (!IsMenu)
            {
                IsMenu = true;

                Watch.NewWatch(0.50f, (int tick, bool isFinished) =>
                {
                    if (isFinished)
                    {
                        IsMenu = false;

                        Manager.GetManager().InvokeGame();
                    }
                });
            }
        }

        public bool IsMenu { get; set; }

        public override string ToString() => $"MenUI ()";
    }
}
