using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour {

    public TMPro.TextMeshProUGUI Score;
    public TMPro.TextMeshProUGUI Gravity;
    public WorldGenerator Generator;
    int score = 0;
    private Object starExplosionPrefab;

    // Use this for initialization
    void Start () {
        starExplosionPrefab = Resources.Load("Prefabs/StarExplosion", typeof(GameObject));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Star(Clone)")
        {
            score++;
            Score.text = string.Format("Score: {0}", score);
            var explosion = Instantiate(starExplosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(explosion, 3f);

            float gravityMultiplier = 1.0f + System.Convert.ToSingle(score) / 100;
            Generator.GravityMultiplier = gravityMultiplier;
            Gravity.text = string.Format("Gravity: {0}g", gravityMultiplier.ToString("n1"));
        }
    }
}
