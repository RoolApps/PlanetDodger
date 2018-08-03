using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StarCollision : MonoBehaviour {

    public UnityEngine.UI.Text Score;
    int score = 0;
    private Object starExplosionPrefab;

    // Use this for initialization
    void Start () {
        starExplosionPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/StarExplosion.prefab", typeof(GameObject));
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
            var explosion = Instantiate(starExplosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(explosion, 3f);
            
        }
    }
}
