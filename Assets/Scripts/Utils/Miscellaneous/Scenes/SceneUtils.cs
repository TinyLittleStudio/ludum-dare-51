using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TinyLittleStudio.LudumDare51.PROJECT_NAME
{
    public static class SceneUtils
    {
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
    }
}
