using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    AsyncOperation operation;
    public UnityEngine.UI.Slider Progress;

	public void StartGame(string difficultyName)
    {
        GameDifficulty.Difficulty difficulty = GameDifficulty.Difficulty.Unknown;
        if(difficultyName == "Easy")
        {
            difficulty = GameDifficulty.Difficulty.Easy;
        }
        else if (difficultyName == "Medium")
        {
            difficulty = GameDifficulty.Difficulty.Medium;
        }
        else if (difficultyName == "Hard")
        {
            difficulty = GameDifficulty.Difficulty.Hard;
        }
        GameDifficulty.SetDifficulty(difficulty);
        operation = SceneManager.LoadSceneAsync("Scenes/GameplayScene");
    }

    private void LateUpdate()
    {
        if(operation != null)
        {
            Progress.value = operation.progress;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
