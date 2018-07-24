using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public int PlanetSpacing;
    

    // Use this for initialization
    void Start () {

        int amount = 10;

        Enumerable.Range(-amount / 2, amount).ToList().ForEach(i =>
        {
            Enumerable.Range(-amount / 2, amount).ToList().ForEach(j =>
            {
                new StarredPlanet(new Vector3(i * PlanetSpacing, j * PlanetSpacing + ( i % 2 == 0 ? PlanetSpacing / 2 : 0)));
            });
        });
        
    }

    // Update is called once per frame
	void Update () {
        
    }

    static class PlanetSpritesFactory
    {
        const int planetSpritesCount = 5;
        static Sprite[] planetSprites = Enumerable.Range(0, planetSpritesCount)
            .Select(planetId => AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Sprites/Planet{0}_2.png", planetId)))
            .Select(texture => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero)).ToArray();

        public static Sprite Get()
        {
            return planetSprites[Random.Range(0, planetSpritesCount)];
        }
    }

    class StarredPlanet
    {
        static float starDistanceMultiplier = 1.5f;
        static Object starPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Star.prefab", typeof(GameObject));
        static Object planetPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Planet.prefab", typeof(GameObject));

        public StarredPlanet(Vector3 position)
        {
            
            
            var planet = CreatePlanet(position);
            var planetBounds = planet.GetComponent<SpriteRenderer>().bounds;
            var planetRadius = planetBounds.size.magnitude / (2 * Mathf.Sqrt(2));
            var planetCenter = planetBounds.center;
            var star = CreateStar(planetCenter, planetRadius);
        }

        private GameObject CreatePlanet(Vector3 position)
        {
            GameObject clone = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            var radius = Random.Range(0.1f, 0.5f);
            clone.transform.localScale = new Vector3(radius, radius, 1);
            clone.transform.position = position;
            var spriteRenderer = clone.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = PlanetSpritesFactory.Get();
            
            return clone;
        }

        GameObject CreateStar(Vector3 position, float planetRadius)
        {
            GameObject clone = Instantiate(starPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            var angle = Random.Range(0, 2 * Mathf.PI);
            Debug.Log(string.Format("Angle: {0}", angle));
            var starOffset = new Vector3(Mathf.Cos(angle) * planetRadius * starDistanceMultiplier, Mathf.Sin(angle) * planetRadius * starDistanceMultiplier);
            Debug.Log(string.Format("Offset: {0}", starOffset));
            clone.transform.position = position + starOffset;
            Debug.Log(string.Format("Position: {0}, starPosition: {1}", position, clone.transform.position));
            return clone;
        }
    }
}
