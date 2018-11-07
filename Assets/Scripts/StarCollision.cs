using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollision : MonoBehaviour {

    class CollisionSettings
    {
        public CollisionSettings(Action increaser, String explosionPrefabName, float duration)
        {
            Increaser = increaser;
            ExplosionPrefab = Resources.Load(String.Format("Prefabs/{0}", explosionPrefabName), typeof(GameObject));
            Duration = duration;
        }

        public Action Increaser { get; private set; }
        public UnityEngine.Object ExplosionPrefab { get; private set; }
        public float Duration { get; private set; }
    }

    private Dictionary<String, CollisionSettings> collisionSettings;

    // Use this for initialization
    void Start () {

        collisionSettings = new Dictionary<string, CollisionSettings>()
        {
            { "Star(Clone)", new CollisionSettings(GameSession.Current.ScoreStar, "StarExplosion", 1f) },
            { "RareStar(Clone)", new CollisionSettings(GameSession.Current.ScoreRareStar, "RareStarExplosion", 3f) }
        };
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionSettings settings = null;
        collisionSettings.TryGetValue(collision.name, out settings);
        if (settings != null)
        {
            settings.Increaser();
            var starColor = collision.gameObject.transform.Find("StarAnimation").GetComponent<SpriteRenderer>().color;
            var explosion = Instantiate(settings.ExplosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            var main = (explosion as GameObject).GetComponent<ParticleSystem>().main;
            main.startColor = new ParticleSystem.MinMaxGradient(starColor);
            Destroy(collision.gameObject);
            Destroy(explosion, settings.Duration);
        }
    }
}
