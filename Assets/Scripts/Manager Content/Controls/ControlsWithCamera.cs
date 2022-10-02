using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(Controls))]
    public class ControlsWithCamera : MonoBehaviour
    {
        private const float CAMERA_VELOCITY = 16.0f;

        private const float CAMERA_OFFSET_X = 00.0f;
        private const float CAMERA_OFFSET_Y = 02.0f;

        [Header("Settings")]

        [SerializeField]
        private Camera main;

        private void Awake()
        {
            AwakeControls();
        }

        private void AwakeControls()
        {
            Controls = GetComponent<Controls>();
        }

        private void FixedUpdate()
        {
            FixedUpdateControls();
        }

        private void FixedUpdateControls()
        {
            if (main == null)
            {
                main = Camera.main;
            }

            if (main != null)
            {
                Vector3 target = transform.position;

                target.x = target.x + ControlsWithCamera.CAMERA_OFFSET_X;
                target.y = target.y + ControlsWithCamera.CAMERA_OFFSET_Y;

                if (Controls != null)
                {
                    Vector3 lookAt = Controls.GetDirection().ToVector();

                    target.x = target.x + lookAt.x;
                }

                target = Vector2.Lerp(main.transform.position, target, Time.fixedDeltaTime * ControlsWithCamera.CAMERA_VELOCITY);

                target.z = -10.0f;

                main.transform.position = target;
            }
        }

        public Controls Controls { get; private set; }

        public override string ToString() => $"ControlsWithCamera ()";
    }
}
