using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTextChanger : MonoBehaviour {
    public TMPro.TextMeshProUGUI Gravity;
    // Use this for initialization
    void Awake () {
        GameSession.Current.SpaceshipCrashed += Current_SpaceshipCrashed;
        GameSession.Current.GravityChanged += Current_GravityChanged;
	}

    private void Current_SpaceshipCrashed(object sender, System.EventArgs e)
    {
        GameSession.Current.SpaceshipCrashed -= Current_SpaceshipCrashed;
        GameSession.Current.GravityChanged -= Current_GravityChanged;
    }

    private void Current_GravityChanged(object sender, GameSession.GravityEventArgs e)
    {
        Gravity.text = string.Format("Gravity: {0}g", e.Gravity.ToString("n1"));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
