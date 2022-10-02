using UnityEngine;

using static UnityEngine.ParticleSystem;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class ParticleUtils
    {
        public static void Play(string particleId, float x, float y)
        {
            if (GameObjectUtils.Instantiate($"Prefabs/Particle/{particleId}", out ParticleSystem particleSystem))
            {
                MainModule mainModule = particleSystem.main;
                mainModule.stopAction = ParticleSystemStopAction.Destroy;

                particleSystem.gameObject.transform.position = new Vector3(x, y, 0.0f);
                particleSystem.IsEnabled(true);
            }
        }
    }
}
