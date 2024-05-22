using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneWithLoadingScreen(sceneName));
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        IEnumerator LoadSceneWithLoadingScreen(string sceneName)
        {
            SceneManager.LoadSceneAsync(Constants.Consts.Scenes.LoadingScene, LoadSceneMode.Single);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!operation.isDone)
            {
                yield return null;
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }
    }
}