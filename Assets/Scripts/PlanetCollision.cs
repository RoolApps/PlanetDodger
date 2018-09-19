using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        }
    }

    private void DisplayMenu()
    {
        Submitting.gameObject.SetActive(false);
        RestartButton.gameObject.SetActive(true);
        MenuButton.gameObject.SetActive(true);
    }
}
