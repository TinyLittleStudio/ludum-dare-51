using System.Collections.Generic;
using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public class Textures
    {
        private static readonly Textures TEXTURES = new Textures();

        private List<Texture> textures;

        private Textures()
        {
            textures = new List<Texture>();

            Sprite[] sprites = Resources.LoadAll<Sprite>(TexturesUtils.DIRECTORY);

            if (sprites != null)
            {
                foreach (Sprite sprite in sprites)
                {
                    textures.Add(new Texture(sprite.name, sprite));
                }
            }
        }

        public bool HasTexture(string id)
        {
            return HasTexture(id, out _);
        }

        public bool HasTexture(string id, out Texture texture)
        {
            return (texture = GetTexture(id)) != null;
        }

        public Texture GetTexture(string id)
        {
            if (textures != null)
            {
                foreach (Texture texture in textures)
                {
                    if (StringUtils.EqualsIgnoreCase(texture.Id, id))
                    {
                        return texture;
                    }
                }
            }

            return null;
        }

        public Texture SetTexture(string id, Sprite sprite)
        {
            Texture texture = GetTexture(id);

            if (texture != null)
            {
                texture.SetSprite(sprite);
            }

            return texture;
        }

        public override string ToString() => $"Textures ()";

        public static Textures GetTextures()
        {
            return Textures.TEXTURES;
        }
    }
}
