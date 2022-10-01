using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(Controls))]
    public class ControlsWithCamera : MonoBehaviour
    {
        private const float CAMERA_VELOCITY = 16.0f;
        private const float CAMERA_DISTANCE = 4.0f;

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
                Vector3 target;

                target = Vector2.Lerp(main.transform.position, transform.position + new Vector3(0.0f, 2.0f, 0.0f), Time.fixedDeltaTime * ControlsWithCamera.CAMERA_VELOCITY);

                target.z = -10.0f;

                // target = Vector3.ClampMagnitude(target, ControlsWithCamera.CAMERA_DISTANCE);

                // target.x = target.x + (Controls.GetDirection().ToVector().x * -1.0f);

                main.transform.position = target;
            }
        }

        public Controls Controls { get; private set; }

        public override string ToString() => $"ControlsWithCamera ()";
    }
}
