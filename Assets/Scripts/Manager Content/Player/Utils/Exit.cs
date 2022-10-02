using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Exit : MonoBehaviour
    {
        private const float RADIUS = 0.75f;
        private const float RADIUS_OFFSET_X = -0.00f;
        private const float RADIUS_OFFSET_Y = -1.75f;

        [Header("Settings")]

        [SerializeField]
        private bool hasCollected;

        private void Awake()
        {
            AwakeCircleCollider2D();
        }

        private void AwakeCircleCollider2D()
        {
            CircleCollider2D = GetComponent<CircleCollider2D>();

            if (CircleCollider2D != null)
            {
                CircleCollider2D.radius = Exit.RADIUS;
                CircleCollider2D.offset = new Vector2(
                    Exit.RADIUS_OFFSET_X,
                    Exit.RADIUS_OFFSET_Y
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
                Rigidbody2D rigidbody2d = collider2d.gameObject.GetComponent<Rigidbody2D>();

                if (rigidbody2d != null)
                {
                    rigidbody2d.transform.position = new Vector3(transform.position.x, rigidbody2d.transform.position.y);

                    rigidbody2d.AddForce(Vector2.up * 64.0f, ForceMode2D.Impulse);
                }

                float x = collider2d.gameObject.transform.position.x;
                float y = collider2d.gameObject.transform.position.y;

                AudioUtils.Play(IDs.AUDIO_ID__TUBE_EXIT, x, y);

                Watch.NewWatch(0.5f, (int tick, bool isFinish) =>
                {
                    if (isFinish)
                    {
                        SceneUtils.NewScene();
                    }
                });

                hasCollected = true;
            }
        }

        public CircleCollider2D CircleCollider2D { get; private set; }

        public override string ToString() => $"Exit ()";
    }
}
