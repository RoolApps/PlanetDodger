using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public int PlanetSpacing;
    public float Noise;
    public int RenderedPlanets;
    public GameObject Player;
    public GameObject Mesh;

    private Vector2 PlanetBoxSize;
    
    // Use this for initialization
    void Start () {

        PlanetBoxSize = new Vector2(PlanetSpacing, PlanetSpacing);
    }

    // Update is called once per frame
	void Update () {
        var playerPosition = Player.transform.position;
        var playerXBox = System.Convert.ToInt32(playerPosition.x / PlanetSpacing);
        var playerYBox = System.Convert.ToInt32(playerPosition.y / PlanetSpacing);
        
        Enumerable.Range(playerXBox - RenderedPlanets / 2, RenderedPlanets + 1).ToList().ForEach(x =>
        {
            Enumerable.Range(playerYBox - RenderedPlanets / 2, RenderedPlanets + 1).ToList().ForEach(y =>
            {
                if (Mathf.Abs(x - 1) <= 2 && Mathf.Abs(y - 1) <= 2)
                {
                    return;
                }
                var planetXCenter = (System.Convert.ToSingle(2 * x + 1) * PlanetSpacing) / 2;
                var planetYCenter = (System.Convert.ToSingle(2 * y + 1) * PlanetSpacing) / 2;
                var planets = Physics2D.OverlapBoxAll(new Vector2(planetXCenter, planetYCenter), PlanetBoxSize, 0);
                if (!planets.Any(planet => planet.name == "Planet(Clone)"))
                {
                    new StarredPlanet(new Vector3(x * PlanetSpacing + (y % 2 == 1 ? PlanetSpacing / 2 : 0) + Random.Range(0, Noise), y * PlanetSpacing + (x % 2 == 0 ? PlanetSpacing / 2 : 0) + Random.Range(0, Noise)));
                }
            });
        });
    }

    static class PlanetSpritesFactory
    {
        const int planetSpritesCount = 15;
        static Sprite[] planetSprites = Enumerable.Range(0, planetSpritesCount)
            .Select(planetId => AssetDatabase.LoadAssetAtPath<Texture2D>(string.Format("Assets/Sprites/Planet{0}.png", planetId)))
            .Select(texture => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero)).ToArray();

        public static Sprite Get()
        {
            return planetSprites[Random.Range(0, planetSpritesCount)];
        }
    }

    class StarredPlanet
    {
        static float magicLocalRadius = 5.4f;
        static float starDistanceMultiplier = 2f;
        static Object starPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Star.prefab", typeof(GameObject));
        static Object planetPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Planet.prefab", typeof(GameObject));
        static GameObject planetParticleSystemPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PlanetParticleSystem.prefab", typeof(GameObject)) as GameObject;

        public StarredPlanet(Vector3 position)
        {
            var planet = CreatePlanet(position);
            var planetBounds = planet.GetComponent<SpriteRenderer>().bounds;
            var planetRadius = planetBounds.size.magnitude / ( Mathf.Sqrt(2) * 2 );
            var planetCenter = planetBounds.center;
            CreateStar(planetCenter, planetRadius);
            CreatePlanetParticleSystem(planet, planetCenter, planetRadius);
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

        private GameObject CreatePlanetParticleSystem(GameObject planet, Vector3 position, float planetRadius)
        {
            GameObject clone = Instantiate(planetParticleSystemPrefab, planet.transform);
            clone.transform.localPosition = new Vector3(magicLocalRadius, magicLocalRadius);
            ParticleSystem system = clone.GetComponent<ParticleSystem>();

            var shape = system.shape;
            shape.scale = new Vector3(planetRadius, 1, 1);
            shape.position = new Vector3(0, 0, planetRadius);

            return clone;
        }

        private GameObject CreateStar(Vector3 position, float planetRadius)
        {
            GameObject clone = Instantiate(starPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            var angle = Random.Range(0, 2 * Mathf.PI);
            var starOffset = new Vector3(Mathf.Cos(angle) * planetRadius * starDistanceMultiplier, Mathf.Sin(angle) * planetRadius * starDistanceMultiplier);
            clone.transform.position = position + starOffset;
            return clone;
        }
    }
}
