using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    AsyncOperation operation;
    public UnityEngine.UI.Slider Progress;

	public void StartGame()
    {
        operation = SceneManager.LoadSceneAsync("Scenes/GameplayScene");
    }

    private void LateUpdate()
    {
        if(operation != null)
        {
            Progress.value = operation.progress;
        }
    }
}
