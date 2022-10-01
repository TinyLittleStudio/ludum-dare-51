using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class Obstacle : MonoBehaviour
    {
        private GameObject[] ages;

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
            if (e == null)
            {
                return;
            }

            if (World.Current != null)
            {
                int age = World.Current.GetAge();

                if (age > ages.Length)
                {
                    GameObject.Destroy(gameObject.transform.root.gameObject);
                }
            }
        }

        public override string ToString() => $"Obstacle ()";
    }
}
