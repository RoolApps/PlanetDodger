using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTextChanger : MonoBehaviour {
    public TMPro.TextMeshProUGUI Score;

    private const string scoreTemplate = "Score: {0}";

    // Use this for initialization
    void Awake () {
        GameSession.Current.ScoreChanged += Current_ScoreChanged;
        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed; ;
	}

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.ScoreChanged -= Current_ScoreChanged;
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed; ;
    }

    private void Current_ScoreChanged(object sender, GameSession.ScoreEventArgs e)
    {
        Score.text = string.Format(scoreTemplate, e.Score);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
