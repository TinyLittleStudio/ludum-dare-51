using UnityEditor;
using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controls : MonoBehaviour
    {
        private const float GROUND_COLLISION_RADIUS = 0.2f;
        private const float GROUND_COLLISION_OFFSET_X = 0.0f;
        private const float GROUND_COLLISION_OFFSET_Y = 0.5f;

        private const float VELOCITY = 260.0f;
        private const float VELOCITY_DURATION = 0.05f;

        private const float JUMP_FORCE = 1600.0f;

        private const float FLIP_FORCE = 16.0f;

        [Header("Settings")]

        [SerializeField]
        private Vector2 velocity;

        [SerializeField]
        private Vector2 velocityNormalized;

        [Space(10)]

        [SerializeField]
        private bool isWalking;

        [SerializeField]
        private bool isJumping;

        [SerializeField]
        private bool isJumpingGrounded;

        [SerializeField]
        private bool isFlipped;

        [Space(10)]

        [SerializeField]
        private float dustTime;

        [SerializeField]
        private float dustTimeTotal;

        [SerializeField]
        private float dustWalkTime;

        [SerializeField]
        private float dustWalkTimeTotal;

        [Space(10)]

        [SerializeField]
        private Direction direction;

        private void Awake()
        {
            AwakeRigidbody2D();
        }

        private void AwakeRigidbody2D()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (isFlipped)
            {
                if (Rigidbody2D != null)
                {
                    Rigidbody2D.gameObject.transform.localScale = Vector2.Lerp(Rigidbody2D.gameObject.transform.localScale, new Vector2(-1.0f, 1.0f), Time.deltaTime * Controls.FLIP_FORCE);
                }
            }
            else
            {
                if (Rigidbody2D != null)
                {
                    Rigidbody2D.gameObject.transform.localScale = Vector2.Lerp(Rigidbody2D.gameObject.transform.localScale, new Vector2(+1.0f, 1.0f), Time.deltaTime * Controls.FLIP_FORCE);
                }
            }

            ControlsWalk(new Vector2(0.0f, 0.0f));

            if (!IsLocked)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    ControlsWalk(new Vector2(-1.0f, 0.0f));
                }

                if (Input.GetKey(KeyCode.D))
                {
                    ControlsWalk(new Vector2(+1.0f, 0.0f));
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ControlsJump();
                }
            }
        }

        private void OnEnable()
        {
            OnEnableEvents();
        }

        private void OnEnableEvents()
        {
            Event.Inject(IDs.EVENT_ID__WORLD_TURN_OFF, OnWorldTurnOff);
            Event.Inject(IDs.EVENT_ID__WORLD_TURN_ON, OnWorldTurnOn);
        }

        private void OnWorldTurnOff(Event e)
        {
            if (e == null)
            {
                return;
            }

            IsLocked = true;
        }

        private void OnWorldTurnOn(Event e)
        {
            if (e == null)
            {
                return;
            }

            IsLocked = false;
        }

        private void FixedUpdate()
        {
            FixedUpdateControls();
        }

        private void FixedUpdateControls()
        {
            if (Rigidbody2D != null)
            {
                isJumpingGrounded = false;

                Vector2 origin = Rigidbody2D.gameObject.transform.position;
                Vector2 offset = new Vector3(
                    Controls.GROUND_COLLISION_OFFSET_X,
                    Controls.GROUND_COLLISION_OFFSET_Y
                );

                Collider2D[] collider2ds = Physics2D.OverlapCircleAll(origin - offset, Controls.GROUND_COLLISION_RADIUS);

                if (collider2ds != null)
                {
                    foreach (Collider2D collider2d in collider2ds)
                    {
                        if (collider2d == null)
                        {
                            continue;
                        }

                        if (collider2d.gameObject == null)
                        {
                            continue;
                        }

                        if (collider2d.gameObject != Rigidbody2D.gameObject)
                        {
                            isJumpingGrounded = true;
                        }
                    }
                }
            }

            FixedUpdateControlsWalk();
            FixedUpdateControlsJump();
        }

        private void FixedUpdateControlsWalk()
        {
            if (Rigidbody2D != null)
            {
                velocity = velocityNormalized;

                velocity = velocity * Controls.VELOCITY;

                velocity = velocity * Time.fixedDeltaTime;

                velocity = Vector2.SmoothDamp(Rigidbody2D.velocity, velocity, ref velocity, Controls.VELOCITY_DURATION);

                velocity.y = Rigidbody2D.velocity.y;

                Rigidbody2D.velocity = velocity;
            }

            isWalking = Mathf.Abs(velocityNormalized.x) > 0.0f;

            isWalking = isWalking && !isJumping && isJumpingGrounded;

            if (isWalking)
            {
                float x = gameObject.transform.position.x;
                float y = gameObject.transform.position.y - 0.5f;

                dustTime += Time.fixedDeltaTime;

                if (dustTime > dustTimeTotal)
                {
                    ParticleUtils.Play(IDs.PARTICLE_ID__PLAYER_WALK, x, y);

                    dustTime = 0.0f;
                }

                dustWalkTime += Time.deltaTime;

                if (dustWalkTime > dustWalkTimeTotal)
                {
                    AudioUtils.Play(IDs.AUDIO_ID__PLAYER_WALK, x, y);

                    dustWalkTime = 0.0f;
                }
            }
        }

        private void FixedUpdateControlsJump()
        {
            if (isJumping)
            {
                if (Rigidbody2D != null)
                {
                    Vector2 velocityJumping = DirectionUtils.ToVector(Direction.NORTH);

                    velocityJumping = velocityJumping * 1.0f;

                    velocityJumping = velocityJumping * Controls.JUMP_FORCE;

                    Rigidbody2D.AddForce(velocityJumping, ForceMode2D.Force);

                    float x = Rigidbody2D.transform.position.x;
                    float y = Rigidbody2D.transform.position.y;

                    AudioUtils.Play(IDs.AUDIO_ID__PLAYER_JUMP, x, y);

                    ParticleUtils.Play(IDs.PARTICLE_ID__PLAYER_JUMP, x, y);

                    isJumping = false;
                    isJumpingGrounded = false;
                }
            }
        }

        public void ControlsWalk(Vector2 velocity)
        {
            this.velocity = velocity;
            this.velocityNormalized = velocity.normalized;

            if (velocityNormalized.x < 0)
            {
                direction = Direction.WEST;

                isFlipped = true;
            }

            if (velocityNormalized.x > 0)
            {
                direction = Direction.EAST;

                isFlipped = false;
            }
        }

        public void ControlsJump()
        {
            if (!isJumping && isJumpingGrounded)
            {
                isJumping = true;
            }
        }

        public Direction GetDirection()
        {
            return this.direction;
        }

        public Direction SetDirection(Direction direction)
        {
            return this.direction = direction;
        }

        public bool IsWalking()
        {
            return this.isWalking;
        }

        public bool IsJumping()
        {
            return this.isJumping;
        }

        public bool IsLocked { get; private set; }

        public Rigidbody2D Rigidbody2D { get; private set; }

        public override string ToString() => $"Controls ()";
    }
}
