using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Collectable : MonoBehaviour
    {
        private const float RADIUS = 0.50f;
        private const float RADIUS_OFFSET_X = -0.00f;
        private const float RADIUS_OFFSET_Y = -0.00f;

        [Header("Settings")]

        [SerializeField]
        private bool hasCollected;

        private void Awake()
        {
            AwakeCircleCollider2D();

            hasCollected = true;
        }

        private void AwakeCircleCollider2D()
        {
            CircleCollider2D = GetComponent<CircleCollider2D>();

            if (CircleCollider2D != null)
            {
                CircleCollider2D.radius = Collectable.RADIUS;
                CircleCollider2D.offset = new Vector2(
                    Collectable.RADIUS_OFFSET_X,
                    Collectable.RADIUS_OFFSET_Y
                );

                CircleCollider2D.isTrigger = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            OnTriggerEnter2DCollect(collider2d);
        }

        private void OnTriggerEnter2DCollect(Collider2D collider2d)
        {
            if (hasCollected)
            {
                return;
            }

            if (collider2d == null)
            {
                return;
            }

            if (collider2d.gameObject == null)
            {
                return;
            }

            if (collider2d.gameObject.CompareTag(IDs.TAG_ID__PLAYER))
            {
                collider2d.gameObject.IsEnabled(false);

                float x = gameObject.transform.position.x;
                float y = gameObject.transform.position.y;

                AudioUtils.Play(IDs.AUDIO_ID__COLLECTABLE, x, y);

                ParticleUtils.Play(IDs.PARTICLE_ID__COLLECTABLE, x, y);

                if (Manager.GetManager().Profile != null)
                {
                    Manager.GetManager().Profile.Charge(1);
                }

                GameObject.Destroy(gameObject);

                hasCollected = false;
            }
        }

        public CircleCollider2D CircleCollider2D { get; private set; }

        public override string ToString() => $"Collectable ()";
    }
}
