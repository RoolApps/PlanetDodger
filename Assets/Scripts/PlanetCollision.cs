using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Advertisements;

public class PlanetCollision : MonoBehaviour {

    public GameObject Explosion;
    public ParticleSystem Smoke;
    public GameObject MenuPanel;
    public JoystickController Controller;
    public TMPro.TextMeshProUGUI Submitting;
    public UnityEngine.UI.Button RestartButton;
    public UnityEngine.UI.Button MenuButton;
    public EZCameraShake.CameraShaker CameraShaker;

    private bool AlreadyCollided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Planet(Clone)" && !AlreadyCollided)
        {
            AlreadyCollided = true;
            Controller.ScriptEnabled = false;
            Explosion.SetActive(true);
            MenuPanel.SetActive(true);
            CameraShaker.ShakeOnce(20, 1, 0, 2);
            
            var score = GetComponent<StarCollision>().score;
            ScoreManager.SubmitScore(this, score, DisplayMenu);

            //if(Smoke.isStopped)
            //{
            //    Smoke.Play();
            //}
            if(GameSettings.AdsEnabled)
            {
                ShowAd();
            }
        }
    }

    private void ShowAd()
    {
        if (Advertisement.IsReady("video"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("video", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    private void DisplayMenu()
    {
        Submitting.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
    }
}
