using System.Collections;
using UnityEngine;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    [RequireComponent(typeof(AudioSource))]
    public class Audio : MonoBehaviour
    {
        private void Awake()
        {
            AwakeAudioSource();
        }

        private void AwakeAudioSource()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            OnEnableAudioSource();
        }

        private void OnEnableAudioSource()
        {
            if (AudioSource != null)
            {
                AudioSource.Play();
            }

            StartCoroutine(WaitForAudioAndDestroy());
        }

        private void OnDisable()
        {
            OnDisableAudioSource();
        }

        private void OnDisableAudioSource()
        {
            if (AudioSource != null)
            {
                AudioSource.Stop();
            }
        }

        private IEnumerator WaitForAudio()
        {
            yield return new WaitUntil(() =>
            {
                if (AudioSource != null)
                {
                    return !AudioSource.isPlaying;
                }

                return true;
            });
        }

        private IEnumerator WaitForAudioAndDestroy()
        {
            yield return WaitForAudio();

            if (gameObject != null)
            {
                GameObject.Destroy(gameObject);
            }
        }

        public AudioSource AudioSource { get; private set; }

        public override string ToString() => $"Audio ()";
    }
}
