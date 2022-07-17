using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentIndex;
    private AsyncOperation _asyncOperation;
    private bool preloading;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (currentIndex == 0)
        {
            currentIndex = 1;
        }
        preloading = false;
        PreLoadNextScene();
    }

    private void PreLoadNextScene()
    {
        if (_asyncOperation == null)
            return;
        if(currentIndex == 1 && !preloading)
        {
            Debug.Log("Started Scene Preloading");
            // Start scene preloading.
            currentIndex = 2;
            StartCoroutine(LoadSceneAsyncProcess("Gameplay" + currentIndex.ToString()));
        } else if(currentIndex == 2 && !preloading)
        {
            Debug.Log("Started Scene Preloading");
            // Start scene preloading.
            currentIndex = 1;
            StartCoroutine(LoadSceneAsyncProcess("Gameplay" + currentIndex.ToString()));
        }
        preloading = true;
    }
    public void NextScene()
    {
        if(_asyncOperation != null)
        {
            Debug.Log("Allowed Scene Activation");
            _asyncOperation.allowSceneActivation = true;
        }
    }
    public void ReturnToMenu()
    {
        
    }
    private IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        // Begin to load the Scene you have specified.
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Don't let the Scene activate until you allow it to.
        _asyncOperation.allowSceneActivation = false;

        while (!_asyncOperation.isDone)
        {
            Debug.Log($"[scene]:{sceneName} [load progress]: {_asyncOperation.progress}");

            yield return null;
        }
    }
}
