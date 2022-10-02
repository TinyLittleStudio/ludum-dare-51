using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class Obstacle : MonoBehaviour
    {
        [Header("Settings")]

        [SerializeField]
        private int age;

        [SerializeField]
        private ObstacleVariant[] obstacleVariants;

        private void Awake()
        {
            AwakeEvents();
        }

        private void AwakeEvents()
        {
            age = age - 1;

            OnWorld(
                new Event(IDs.EVENT_ID__WORLD)
            );
        }

        private void OnEnable()
        {
            OnEnableEvents();
        }

        private void OnEnableEvents()
        {
            Event.Inject(IDs.EVENT_ID__WORLD, OnWorld);
        }

        private void OnDisable()
        {
            OnDisableEvents();
        }

        private void OnDisableEvents()
        {
            Event.Deject(IDs.EVENT_ID__WORLD, OnWorld);
        }

        private void OnWorld(Event e)
        {
            OnWorldAge(e);
        }

        private void OnWorldAge(Event e)
        {
            if (e == null)
            {
                return;
            }

            SetAge(age + 1);
        }

        public int GetAge()
        {
            return this.age;
        }

        public int SetAge(int age)
        {
            if (obstacleVariants != null)
            {
                ObstacleVariant next = null;
                ObstacleVariant prev = null;

                foreach (ObstacleVariant obstacleVariant in obstacleVariants)
                {
                    if (obstacleVariant != null)
                    {
                        obstacleVariant.IsEnabled(obstacleVariant.IsAge(age));

                        if (next == null && obstacleVariant.IsAge(age))
                        {
                            next = obstacleVariant;
                        }

                        if (prev == null && obstacleVariant.IsAge(this.age))
                        {
                            prev = obstacleVariant;
                        }
                    }
                }

                if (next != null && prev != null)
                {
                    next.transform.position = prev.transform.position;
                    next.transform.rotation = prev.transform.rotation;
                }

                if (next == null)
                {
                    GameObject.Destroy(transform.root.gameObject);
                }
            }

            return this.age = age;
        }

        public override string ToString() => $"Obstacle ()";
    }
}
