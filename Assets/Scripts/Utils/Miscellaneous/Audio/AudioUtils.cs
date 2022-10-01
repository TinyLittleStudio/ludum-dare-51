using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class AudioUtils
    {
        public static void Play(string id, float x, float y)
        {
            if (GameObjectUtils.Instantiate(id, out GameObject gameObject))
            {
                gameObject.transform.position = new Vector3(x, y, 0.0f);
                gameObject.IsEnabled(true);

                if (gameObject.HasComponent<AudioSource>(out AudioSource audioSource))
                {
                    audioSource.pitch = Random.Range(0.8f, 1.2f);
                    audioSource.Play();
                }
            }
        }
    }
}
