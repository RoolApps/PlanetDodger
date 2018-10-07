using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour {

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
            GameSession.Current.IncreaseScore();
            var explosion = Instantiate(starExplosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(explosion, 3f);
        }
    }
}
