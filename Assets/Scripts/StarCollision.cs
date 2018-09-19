using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour {

    public TMPro.TextMeshProUGUI Score;
    public TMPro.TextMeshProUGUI Gravity;
    public WorldGenerator Generator;
    public int score = 0;
    private Object starExplosionPrefab;

    // Use this for initialization
    void Start () {
        starExplosionPrefab = Resources.Load("Prefabs/StarExplosion", typeof(GameObject));
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void UpdateText()
    {
        Score.text = string.Format("Score: {0}", score);
        Gravity.text = string.Format("Gravity: {0}g", Generator.GravityMultiplier.ToString("n1"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Star(Clone)")
        {
            score++;
            var explosion = Instantiate(starExplosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(explosion, 3f);

            Generator.GravityMultiplier += 0.01f;

            UpdateText();
        }
    }
}
