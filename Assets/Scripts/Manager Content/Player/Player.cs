using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(Controls))]
    [RequireComponent(typeof(ControlsWithCamera))]
    public class Player : MonoBehaviour
    {
        private const float THRESHOLD = -6.0f;

        private const string ANIMATION_KEY__IS_WALKING = "isWalking";
        private const string ANIMATION_KEY__IS_JUMPING = "isJumping";

        [Header("Settings")]

        [SerializeField]
        private Animator animator;

        private void Awake()
        {
            AwakeControls();
            AwakeControlsWithCamera();
        }

        private void AwakeControls()
        {
            Controls = GetComponent<Controls>();
        }

        private void AwakeControlsWithCamera()
        {
            ControlsWithCamera = GetComponent<ControlsWithCamera>();
        }

        private void Update()
        {
            float x = transform.position.x;
            float y = transform.position.y;

            if (y < Player.THRESHOLD)
            {
                Die();
            }

            if (World.Current != null)
            {
                if (World.Current.GetParallax() != null)
                {
                    World.Current.GetParallax().Update(Camera.main.transform.position.x, Camera.main.transform.position.y);
                }
            }

            if (animator != null)
            {
                animator.SetBool(Player.ANIMATION_KEY__IS_WALKING, Controls.IsWalking());
                animator.SetBool(Player.ANIMATION_KEY__IS_JUMPING, Controls.IsJumping());
            }

            UpdateGameOver();
        }

        private void UpdateGameOver()
        {
            if (Manager.GetManager().Profile != null)
            {
                if (Manager.GetManager().Profile.IsGameOver())
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            if (gameObject.IsEnabled())
            {
                Watch.NewWatch(0.25f, (int tick, bool isFinished) =>
                {
                    if (isFinished)
                    {
                        SceneUtils.Reload();
                    }
                });

                gameObject.IsEnabled(false);
            }
        }

        public Controls Controls { get; private set; }

        public ControlsWithCamera ControlsWithCamera { get; private set; }

        public override string ToString() => $"Player ()";
    }
}
