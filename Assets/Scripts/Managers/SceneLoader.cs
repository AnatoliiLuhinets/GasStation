using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader
    {
        public void LoadScene(string sceneName)
        {
            LoadSceneWithLoadingScreen(sceneName).Forget();
        }
        
        private async UniTask LoadSceneWithLoadingScreen(string sceneName)
        {
            SceneManager.LoadSceneAsync(Constants.Consts.Scenes.LoadingScene, LoadSceneMode.Single);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            await UniTask.WaitWhile(() => !operation.isDone);
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

    }
}