using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Advertisements;

public class PlanetCollision : MonoBehaviour {

    public GameObject Explosion;
    
    private bool AlreadyCollided = false;
    private static DateTime lastAdTime = DateTime.MinValue;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Planet(Clone)" && !AlreadyCollided)
        {
            AlreadyCollided = true;
            Explosion.SetActive(true);
            var score = GameSession.Current.Score;
            if (GameSettings.Current.Highscore < score)
            {
                GameSettings.Current.Highscore = score;
            }
            if (score >= 100 && !GameSettings.Current.AdvancedShipUnlocked)
            {
                GameSettings.Current.AdvancedShipUnlocked = true;
            }
            GameSession.Current.CrashSpaceship();
            
            if (!GameSettings.Current.AdsDisabled)
            {
                var now = DateTime.Now;
                if (now - lastAdTime > new TimeSpan(0, 3, 0))
                {
                    lastAdTime = now;
                    ShowAd();
                }
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
}
