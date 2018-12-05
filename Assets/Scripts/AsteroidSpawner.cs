using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public int Interval;
    public float Radius;

    private System.DateTime lastSpawn;
    private Object asteroidPrefab;


    private void Awake()
    {
        lastSpawn = System.DateTime.Now;
        asteroidPrefab = Resources.Load("Prefabs/Asteroid", typeof(GameObject));
    }

    private void Update()
    {
        var now = System.DateTime.Now;
        if (now - lastSpawn > new System.TimeSpan(0, 0, Interval))
        {
            lastSpawn = now;
            CreateAsteroid();
        }
    }

    private void CreateAsteroid()
    {
        GameObject clone = Instantiate(asteroidPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        var radius = Random.Range(0.5f, 0.8f);
        clone.transform.localScale = new Vector3(radius, radius, 1);

        var positionAngle = Random.Range(0f, 2f) * Mathf.PI;
        var positionOffset = new Vector3(Radius * Mathf.Cos(positionAngle), Radius * Mathf.Sin(positionAngle));
        clone.transform.position = transform.position + positionOffset;

        var direction = -positionOffset;
        //float AngleRad = Mathf.Atan2(direction.y, direction.x);
        //float AngleDeg = 180 / Mathf.PI * AngleRad;
        //clone.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        clone.transform.right = direction;

        var rigidbody = clone.GetComponent<Rigidbody2D>();
        rigidbody.mass *= radius;
        var speed = Random.Range(0.33f, 0.66f) * rigidbody.mass;
        rigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
        

        Destroy(clone, 10);
    }
}
