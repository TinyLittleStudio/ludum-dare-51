using UnityEngine;
using UnityEngine.UI;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class UserInterface : MonoBehaviour
    {
        private void Awake()
        {
            AwakeImage();
        }

        private void AwakeImage()
        {
            Image = GetComponent<Image>();
        }

        public Image Image { get; private set; }

        public override string ToString() => $"UserInterface ()";
    }
}
