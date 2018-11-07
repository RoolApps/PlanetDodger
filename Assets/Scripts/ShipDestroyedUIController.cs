using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroyedUIController : MonoBehaviour {
    public GameObject MenuPanel;
    public EZCameraShake.CameraShaker CameraShaker;
    public TMPro.TextMeshProUGUI Highscore;
    // Use this for initialization
    void Start () {
        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed;
    }

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed;
        Highscore.text = string.Format("Best: {0}", GameSettings.Current.Highscore);
        MenuPanel.SetActive(true);
        CameraShaker.ShakeOnce(20, 1, 0, 2);
        if (!Application.isEditor)
        {
            ScoreManager.SubmitScore(this, GameSession.Current.Score);
        }
    }
}
