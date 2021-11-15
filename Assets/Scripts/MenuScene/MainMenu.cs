using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;

    public RectTransform loadingOverlay;

    AsyncOperation sceneLoadingOperation;

    // Start is called before the first frame update
    void Start()
    {
        loadingOverlay.gameObject.SetActive(false);

        sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        sceneLoadingOperation.allowSceneActivation = false;
    }

    public void LoadScene()
    {
        loadingOverlay.gameObject.SetActive(true);

        sceneLoadingOperation.allowSceneActivation = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
