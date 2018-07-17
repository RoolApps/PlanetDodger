using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int amount = 10;
        int planetSpritesCount = 5;

        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Planet.prefab", typeof(GameObject));
        var planetSprites = Enumerable.Range(0, planetSpritesCount)
            .Select(planetId => AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Sprites/Planet{0}.png", planetId)))
            .Select(texture => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero)).ToArray();

        Enumerable.Range(-amount / 2, amount).ToList().ForEach(i =>
        {
            Enumerable.Range(-amount / 2, amount).ToList().ForEach(j =>
            {
                GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
                var radius = Random.Range(0.1f, 0.5f);
                clone.transform.localScale = new Vector3(radius, radius, 1);
                clone.transform.position = new Vector3(i * 10 + Random.Range(-5f, 5f), j * 10 + Random.Range(-5f, 5f), 0);
                var spriteRenderer = clone.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = planetSprites[Random.Range(0, planetSpritesCount)];
            });
        });
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
