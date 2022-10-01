using UnityEngine;
using UnityEngine.UI;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Sign : MonoBehaviour
    {
        private const float RADIUS = 0.75f;
        private const float RADIUS_OFFSET_X = -0.00f;
        private const float RADIUS_OFFSET_Y = -0.25f;

        private const float DIALOGUE_TIME_PER_CHARACTER = 0.10f;
        private const float DIALOGUE_TIME_PER_CHARACTER_BASIS = 0.75f;

        private const float DIALOGUE_TYPEWRITE = 0.05f;
        private const float DIALOGUE_TYPEWRITE_PADDING = 16.00f;

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
        private int index;

        [SerializeField]
        private float typewriteTime;

        [SerializeField]
        private float typewriteTimeTotal;

        [Space(10)]

        [SerializeField]
        private bool isInteract;

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
                CircleCollider2D.radius = Sign.RADIUS;
                CircleCollider2D.offset = new Vector2(
                    Sign.RADIUS_OFFSET_X,
                    Sign.RADIUS_OFFSET_Y
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
                if (dialogueContainer != null)
                {
                    dialogueContainer.IsEnabled(false);
                }

                if (dialogueContainerBackground != null)
                {
                    dialogueContainerBackground.IsEnabled(false);
                }

                if (dialogueText != null)
                {
                    dialogueText.text = null;
                }

                dialogue = null;
                dialogueWithTypewrite = null;
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
                    dialogueContainer.IsEnabled(isInteract);
                }

                if (dialogueContainerBackground != null)
                {
                    float textSizeDelta = Sign.DIALOGUE_TYPEWRITE_PADDING;

                    foreach (char c in dialogueWithTypewrite)
                    {
                        if (dialogueText.font.GetCharacterInfo(c, out CharacterInfo characterInfo, dialogueText.fontSize))
                        {
                            textSizeDelta += characterInfo.advance;
                        }
                    }

                    dialogueContainerBackground.sizeDelta = new Vector2(textSizeDelta, dialogueContainerBackground.sizeDelta.y);
                    dialogueContainerBackground.IsEnabled(isInteract);
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
            OnTriggerEnter2DSign(collider2d);
        }

        private void OnTriggerEnter2DSign(Collider2D collider2d)
        {
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
                if (text != null)
                {
                    dialogue = text.ToUpper();
                    dialogueWithTypewrite = null;
                }

                displayTime = 0.0f;
                displayTimeTotal = (dialogue.Length * Sign.DIALOGUE_TIME_PER_CHARACTER) + Sign.DIALOGUE_TIME_PER_CHARACTER_BASIS;

                typewriteTime = 0.0f;
                typewriteTimeTotal = Sign.DIALOGUE_TYPEWRITE;

                index = 0;

                isInteract = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collider2d)
        {
            OnTriggerExit2DSign(collider2d);
        }

        private void OnTriggerExit2DSign(Collider2D collider2d)
        {
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
                isInteract = false;
            }
        }

        public CircleCollider2D CircleCollider2D { get; private set; }

        public override string ToString() => $"Sign ()";
    }
}
