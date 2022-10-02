using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class GameUI : MonoBehaviour
    {
        private const int THRESHOLD = 5;

        [Header("Settings")]

        [SerializeField]
        private Transform container;

        [SerializeField]
        private Transform containerTransition;

        [Space(10)]

        [SerializeField]
        private UserInterface userInterfaceBattery;

        [SerializeField]
        private UserInterface userInterfaceBatteryMask;

        [SerializeField]
        private UserInterface userInterfaceBatteryMaskInfill;

        [Space(10)]

        [SerializeField]
        private UserInterface userInterfaceBatteryCharges;

        [Space(10)]

        [SerializeField]
        private UserInterface userInterfaceTurnOffAndOn;

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
            StartGame();
        }

        private void StartGame()
        {
            AudioUtils.Play(IDs.AUDIO_ID__START_APPLICATION_GAME, 0.0f, 0.0f);
        }

        private void OnEnable()
        {
            OnEnableEvents();
        }

        private void OnEnableEvents()
        {
            Event.Inject(IDs.EVENT_ID__PROFILE_BATTERY_CHARGES, OnProfileBatteryCharges);

            Event.Inject(IDs.EVENT_ID__WORLD_TURN_OFF, OnWorldTurnOff);
            Event.Inject(IDs.EVENT_ID__WORLD_TURN_ON, OnWorldTurnOn);
        }

        private void OnDisable()
        {
            OnDisableEvents();
        }

        private void OnDisableEvents()
        {
            Event.Deject(IDs.EVENT_ID__PROFILE_BATTERY_CHARGES, OnProfileBatteryCharges);

            Event.Deject(IDs.EVENT_ID__WORLD_TURN_OFF, OnWorldTurnOff);
            Event.Deject(IDs.EVENT_ID__WORLD_TURN_ON, OnWorldTurnOn);
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (userInterfaceBatteryMaskInfill == null)
            {
                return;
            }

            if (userInterfaceBatteryMaskInfill.Image == null)
            {
                return;
            }

            if (World.Current != null)
            {
                float time = World.Current.Time;
                float timeTotal = World.Current.TimeTotal;

                userInterfaceBatteryMaskInfill.Image.fillAmount = 1.0f - (time / timeTotal);
            }
        }

        private void OnProfileBatteryCharges(Event e)
        {
            if (e == null)
            {
                return;
            }

            int total;

            total = e.GetValue<int>(IDs.EVENT_ARGUMENT_ID__BATTERY_CHARGES, 0);

            total = Mathf.Clamp(total, 0, GameUI.THRESHOLD);

            if (userInterfaceBatteryCharges != null)
            {
                int count = userInterfaceBatteryCharges.transform.childCount;

                for (int i = 0; i < Mathf.Abs(total - count); i++)
                {
                    if (total < count)
                    {
                        Transform transform = userInterfaceBatteryCharges.transform.GetChild((userInterfaceBatteryCharges.transform.childCount - 1) - i);

                        if (transform != null)
                        {
                            if (transform.gameObject != null)
                            {
                                GameObject.Destroy(transform.gameObject);
                            }
                        }
                    }

                    if (total > count)
                    {
                        if (GameObjectUtils.Instantiate<UserInterface>(IDs.USER_INTERFACE_ID__BATTERY_CHARGE, userInterfaceBatteryCharges.transform, out UserInterface userInterface))
                        {
                            userInterface.IsEnabled(true);
                        }
                    }
                }
            }
        }

        private void OnWorldTurnOff(Event e)
        {
            if (Manager.GetManager().Profile == null)
            {
                return;
            }

            if (Manager.GetManager().Profile.IsGameOver())
            {
                return;
            }

            if (userInterfaceTurnOffAndOn != null)
            {
                userInterfaceTurnOffAndOn.IsEnabled(true);
            }
        }

        private void OnWorldTurnOn(Event e)
        {
            if (Manager.GetManager().Profile == null)
            {
                return;
            }

            if (Manager.GetManager().Profile.IsGameOver())
            {
                return;
            }

            if (userInterfaceTurnOffAndOn != null)
            {
                userInterfaceTurnOffAndOn.IsEnabled(false);
            }
        }

        public void InvokeMenu()
        {
            Manager.GetManager().InvokeMenu();
        }

        public void InvokeGame()
        {
            Manager.GetManager().InvokeGame();
        }

        public override string ToString() => $"GameUI ()";
    }
}
