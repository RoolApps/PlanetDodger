using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    AsyncOperation operation;
    public UnityEngine.UI.Slider LandscapeProgress;
    public UnityEngine.UI.Slider PortraitProgress;

	public void StartGame()
    {
        operation = SceneManager.LoadSceneAsync("Scenes/GameplayScene");
        
    }

    private void LateUpdate()
    {
        if(operation != null)
        {
            LandscapeProgress.value = operation.progress;
            PortraitProgress.value = operation.progress;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
