using System;
using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [Serializable]
    public class Texture
    {
        [Header("Settings")]

        [SerializeField]
        private string id;

        [SerializeField]
        private Sprite sprite;

        public Texture(string id)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Texture' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = id.Trim();
                this.id = id.ToLower();
            }

            this.sprite = null;
        }

        public Texture(string id, Sprite sprite)
        {
            if (id == null)
            {
                throw new Exception($"Field 'Id' of class 'Texture' cannot be null.");
            }

            if (id != null)
            {
                this.id = id;
                this.id = id.Trim();
                this.id = id.ToLower();
            }

            this.sprite = sprite;
        }

        public string Id => id;

        public Sprite GetSprite()
        {
            return this.sprite;
        }

        public Sprite SetSprite(Sprite sprite)
        {
            return this.sprite = sprite;
        }

        public override string ToString() => $"Texture (Id: {Id})";
    }
}
