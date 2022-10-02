using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(TerminalDialogue))]
    public class Terminal : MonoBehaviour
    {
        private const float FLIP_FORCE = 16.0f;

        [Header("Settings")]

        [SerializeField]
        private SpriteRenderer terminalSpriteRenderer;

        [Space(10)]

        [SerializeField]
        private Sprite terminal;

        [SerializeField]
        private Sprite terminalOffline;

        private void Awake()
        {
            AwakeTerminalDialogue();
        }

        private void AwakeTerminalDialogue()
        {
            TerminalDialogue = GetComponent<TerminalDialogue>();
        }

        private void Update()
        {
            UpdateTerminalDialogue();
            UpdateTerminalDialogueTransform();
        }

        private void UpdateTerminalDialogue()
        {
            if (TerminalDialogue != null)
            {
                if (TerminalDialogue.HasInteracted())
                {
                    if (terminalSpriteRenderer != null)
                    {
                        terminalSpriteRenderer.sprite = terminal;
                    }
                }
                else
                {
                    if (terminalSpriteRenderer != null)
                    {
                        terminalSpriteRenderer.sprite = terminalOffline;
                    }
                }
            }
        }

        private void UpdateTerminalDialogueTransform()
        {
            if (World.Current == null)
            {
                return;
            }

            if (World.Current.Player == null)
            {
                return;
            }

            if (World.Current.Player.transform.position.x < transform.position.x)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(+1.0f, +1.0f), Time.deltaTime * Terminal.FLIP_FORCE);
            }

            if (World.Current.Player.transform.position.x > transform.position.x)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1.0f, +1.0f), Time.deltaTime * Terminal.FLIP_FORCE);
            }
        }

        public TerminalDialogue TerminalDialogue { get; private set; }

        public override string ToString() => $"Terminal ()";
    }
}
