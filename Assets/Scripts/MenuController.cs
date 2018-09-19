using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Linq;

public class MenuController : MonoBehaviour {

    AsyncOperation operation;
    public UnityEngine.UI.Slider Progress;
    public TMPro.TextMeshProUGUI Scores;

    private GameDifficulty.Difficulty GetDifficulty(string difficultyName)
    {
        switch (difficultyName)
        {
            case "Easy":
                return GameDifficulty.Difficulty.Easy;
            case "Medium":
                return GameDifficulty.Difficulty.Medium;
            case "Hard":
                return GameDifficulty.Difficulty.Hard;
            default:
                return GameDifficulty.Difficulty.Unknown;
        }
    }

	public void StartGame(string difficultyName)
    {
        GameDifficulty.Difficulty difficulty = GetDifficulty(difficultyName);
        GameDifficulty.SetDifficulty(difficulty);
        operation = SceneManager.LoadSceneAsync("Scenes/GameplayScene");
    }

    public void LoadScores(string difficultyName)
    {
        Scores.text = "LOADING...";
        ScoreManager.GetScores(this, difficultyName, ScoresLoaded);
    }

    private void ScoresLoaded(ScoreManager.Scores scores)
    {
        Scores.text = String.Join(Environment.NewLine, scores.Items.Select(score => String.Format("{0}: {1}", score.name, score.score)).ToArray());
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
