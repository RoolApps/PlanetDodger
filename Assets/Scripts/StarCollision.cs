using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour {

    public UnityEngine.UI.Text Score;
    int score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Star(Clone)")
        {
            score++;
            Score.text = score.ToString();
            Destroy(collision.gameObject);
        }
    }
}
