using UnityEngine;
using UnityEngine.UI;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TerminalDialogue : MonoBehaviour
    {
        private const float RADIUS = 1.75f;
        private const float RADIUS_OFFSET_X = -0.00f;
        private const float RADIUS_OFFSET_Y = -0.00f;

        private const float DIALOGUE_TIME_PER_CHARACTER = 0.10f;
        private const float DIALOGUE_TIME_PER_CHARACTER_BASIS = 2.75f;

        private const float DIALOGUE_TYPEWRITE = 0.05f;
        private const float DIALOGUE_TYPEWRITE_PADDING = 16.00f;

        [Header("Settings")]

        [SerializeField]
        private string text;

        [Space(10)]

        [SerializeField]
        private float displayTime;

        [SerializeField]
        private float displayTimeTotal;

        [Space(10)]

        [SerializeField]
        private string dialogue;

        [SerializeField]
        private string dialogueWithTypewrite;

        [SerializeField]
        private float typewriteTime;

        [SerializeField]
        private float typewriteTimeTotal;

        [SerializeField]
        private int index;

        [Space(10)]

        [SerializeField]
        private bool isTriggered;

        [SerializeField]
        private bool isTriggeredOnlyOnce;

        [Space(10)]

        [SerializeField]
        private bool hasInteracted;

        [Space(10)]

        [SerializeField]
        private RectTransform dialogueContainer;

        [SerializeField]
        private RectTransform dialogueContainerBackground;

        [SerializeField]
        private Text dialogueText;

        private void Awake()
        {
            AwakeCircleCollider2D();
        }

        private void AwakeCircleCollider2D()
        {
            CircleCollider2D = GetComponent<CircleCollider2D>();

            if (CircleCollider2D != null)
            {
                CircleCollider2D.radius = TerminalDialogue.RADIUS;
                CircleCollider2D.offset = new Vector2(
                    TerminalDialogue.RADIUS_OFFSET_X,
                    TerminalDialogue.RADIUS_OFFSET_Y
                );

                CircleCollider2D.isTrigger = true;
            }
        }

        private void Update()
        {
            UpdateDialogue();
        }

        private void UpdateDialogue()
        {
            if (dialogueContainer != null)
            {
                dialogueContainer.transform.localScale = transform.localScale * 0.0625f;
            }

            UpdateDialogueDisplay();
            UpdateDialogueDisplayTypewrite();
        }

        private void UpdateDialogueDisplay()
        {
            if (dialogue == null)
            {
                return;
            }

            displayTime += Time.deltaTime;

            if (displayTime >= displayTimeTotal)
            {
                dialogue = null;
                dialogueWithTypewrite = null;

                if (dialogueText != null)
                {
                    dialogueText.text = null;
                }

                if (dialogueContainer != null)
                {
                    dialogueContainer.IsEnabled(false);
                }

                if (dialogueContainerBackground != null)
                {
                    dialogueContainerBackground.IsEnabled(false);
                }

                HasInteracted(false);
            }
        }

        private void UpdateDialogueDisplayTypewrite()
        {
            if (dialogue == null)
            {
                return;
            }

            typewriteTime += Time.deltaTime;

            if (typewriteTime >= typewriteTimeTotal)
            {
                if (dialogue != null)
                {
                    if (dialogue.Length > index)
                    {
                        dialogueWithTypewrite = dialogueWithTypewrite + dialogue[index];
                    }
                }

                index++;

                if (dialogueContainer != null)
                {
                    dialogueContainer.IsEnabled(HasInteracted());
                }

                if (dialogueContainerBackground != null)
                {
                    float textSizeDelta = TerminalDialogue.DIALOGUE_TYPEWRITE_PADDING;

                    foreach (char c in dialogueWithTypewrite)
                    {
                        if (dialogueText.font.GetCharacterInfo(c, out CharacterInfo characterInfo, dialogueText.fontSize))
                        {
                            textSizeDelta += characterInfo.advance;
                        }
                    }

                    dialogueContainerBackground.sizeDelta = new Vector2(textSizeDelta, dialogueContainerBackground.sizeDelta.y);
                    dialogueContainerBackground.IsEnabled(HasInteracted());
                }

                if (dialogueText != null)
                {
                    dialogueText.text = dialogueWithTypewrite;
                }

                typewriteTime -= typewriteTimeTotal;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            OnTriggerEnter2DTerminal(collider2d);
        }

        private void OnTriggerEnter2DTerminal(Collider2D collider2d)
        {
            if (hasInteracted)
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
                if (!isTriggered || !isTriggeredOnlyOnce)
                {
                    TerminalDialogue[] terminalDialogues = Object.FindObjectsOfType<TerminalDialogue>();

                    if (terminalDialogues != null)
                    {
                        foreach (TerminalDialogue terminalDialogue in terminalDialogues)
                        {
                            terminalDialogue.HasInteracted(false);
                        }
                    }

                    float x = collider2d.gameObject.transform.position.x;
                    float y = collider2d.gameObject.transform.position.y;

                    AudioUtils.Play(IDs.AUDIO_ID__TALK, x, y);

                    HasInteracted(true);
                }

                isTriggered = true;
            }
        }

        public bool HasInteracted()
        {
            return this.hasInteracted;
        }

        public bool HasInteracted(bool hasInteracted)
        {
            dialogue = null;
            dialogueWithTypewrite = null;

            typewriteTime = 0.0f;
            typewriteTimeTotal = TerminalDialogue.DIALOGUE_TYPEWRITE;

            displayTime = 0.0f;
            displayTimeTotal = 0.0f;

            index = 0;

            if (dialogueText != null)
            {
                dialogueText.text = null;
            }

            if (dialogueContainer != null)
            {
                dialogueContainer.IsEnabled(false);
            }

            if (dialogueContainerBackground != null)
            {
                dialogueContainerBackground.IsEnabled(false);
            }

            if (hasInteracted)
            {
                if (text != null)
                {
                    dialogue = text;
                    dialogueWithTypewrite = null;

                    displayTime = 0.0f;
                    displayTimeTotal = (dialogue.Length * TerminalDialogue.DIALOGUE_TIME_PER_CHARACTER) + TerminalDialogue.DIALOGUE_TIME_PER_CHARACTER_BASIS;
                }
            }

            return this.hasInteracted = hasInteracted;
        }

        public CircleCollider2D CircleCollider2D { get; private set; }

        public override string ToString() => $"TerminalDialogue ()";
    }
}
