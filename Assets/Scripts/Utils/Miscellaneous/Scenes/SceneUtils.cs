using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class SceneUtils
    {
        public static void NewScene()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene != null)
            {
                SceneManager.LoadScene(scene.buildIndex + 1, LoadSceneMode.Single);
            }
        }

        public static void NewScene(string sceneId)
        {
            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }

        public static void NewScene(string sceneId, UnityAction<Scene, Scene> onScene)
        {
            if (onScene != null)
            {
                onScene += (Scene a, Scene b) =>
                {
                    SceneManager.activeSceneChanged -= onScene;
                };

                SceneManager.activeSceneChanged += onScene;
            }

            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }

        public static void Reload()
        {
            SceneUtils.Reload(null);
        }

        public static void Reload(UnityAction<Scene, Scene> onScene)
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene != null)
            {
                NewScene(scene.name, onScene);
            }
        }

        public static void Inject(UnityAction<Scene, LoadSceneMode> sceneLoaded)
        {
            if (sceneLoaded != null)
            {
                SceneManager.sceneLoaded += sceneLoaded;
            }
        }

        public static void Deject(UnityAction<Scene, LoadSceneMode> sceneLoaded)
        {
            if (sceneLoaded != null)
            {
                SceneManager.sceneLoaded -= sceneLoaded;
            }
        }

    }
}
