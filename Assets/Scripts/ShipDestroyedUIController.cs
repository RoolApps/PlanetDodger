using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDestroyedUIController : MonoBehaviour {
    public GameObject MenuPanel;
    public TMPro.TextMeshProUGUI Submitting;
    public UnityEngine.UI.Button RestartButton;
    public UnityEngine.UI.Button MenuButton;
    public EZCameraShake.CameraShaker CameraShaker;
    // Use this for initialization
    void Start () {
        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed;
	}

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed;
        MenuPanel.SetActive(true);
        CameraShaker.ShakeOnce(20, 1, 0, 2);
        ScoreManager.SubmitScore(this, GameSession.Current.Score, DisplayMenu);
    }

    private void DisplayMenu()
    {
        Submitting.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
