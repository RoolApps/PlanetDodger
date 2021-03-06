﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    private int PlanetSpacing;
    private float Noise;
    private int RenderedPlanets;
    
    private Vector2 PlanetBoxSize;

    // Use this for initialization
    void Start () {

        var settings = GameDifficulty.Settings;
        PlanetSpacing = settings.PlanetSpacing;
        Noise = settings.Noise;
        RenderedPlanets = settings.RenderedPlanets;
        
        PlanetBoxSize = new Vector2(PlanetSpacing, PlanetSpacing);
    }

    // Update is called once per frame
    void Update () {
        var playerPosition = transform.position;
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
                    new StarredPlanet(
                        new Vector3(
                            x * PlanetSpacing + (y % 2 == 1 ? PlanetSpacing / 2 : 0) + Random.Range(0, Noise),
                            y * PlanetSpacing + (x % 2 == 0 ? PlanetSpacing / 2 : 0) + Random.Range(0, Noise),
                            10),
                        gameObject,
                        PlanetSpacing);
                }
            });
        });
    }

    static class PlanetSpritesFactory
    {
        const int planetSpritesCount = 15;
        static Sprite[] planetSprites = Enumerable.Range(0, planetSpritesCount)
            .Select(planetId => Resources.Load<Texture2D>(string.Format("Sprites/Planet{0}", planetId)))
            .Select(texture => Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f))).ToArray();

        public static Sprite Get()
        {
            return planetSprites[Random.Range(0, planetSpritesCount)];
        }
    }

    class StarredPlanet
    {
        const float starDistanceMultiplier = 2f;
        const int starsCount = 5;
        static Object rareStarPrefab = Resources.Load("Prefabs/RareStar", typeof(GameObject));
        static Object starPrefab = Resources.Load("Prefabs/Star", typeof(GameObject));
        static Object planetPrefab = Resources.Load("Prefabs/Planet", typeof(GameObject));
        static GameObject shipParticleSystemPrefab = Resources.Load("Prefabs/ShipParticleSystem", typeof(GameObject)) as GameObject;

        public StarredPlanet(Vector3 position, GameObject player, float starsSpacing)
        {
            var planet = CreatePlanet(position);
            var planetBounds = planet.GetComponent<SpriteRenderer>().bounds;
            var planetRadius = planetBounds.size.magnitude / ( Mathf.Sqrt(2) * 2 );
            var planetCenter = planetBounds.center;
            CreateStar(rareStarPrefab, planetCenter, Random.Range(0, 2 * Mathf.PI), planetRadius);
            Enumerable.Range(0, starsCount).ToList().ForEach(i =>
            {
                var multiplier = Random.Range(0.2f, 0.5f);
                CreateStar(starPrefab, planetCenter, 2f * Mathf.PI * i / starsCount, starsSpacing * multiplier);
            });
            CreateShipParticleSystem(planet, player);
        }

        private GameObject CreatePlanet(Vector3 position)
        {
            GameObject clone = Instantiate(planetPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            var radius = Random.Range(0.3f, 1.5f);
            clone.transform.localScale = new Vector3(radius, radius, 1);
            clone.transform.position = position;
            var spriteRenderer = clone.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = PlanetSpritesFactory.Get();
            clone.GetComponent<Planet>().Radius = radius;
            return clone;
        }

        private GameObject CreateShipParticleSystem(GameObject planet, GameObject player)
        {
            GameObject clone = Instantiate(shipParticleSystemPrefab, player.transform);
            planet.GetComponent<Planet>().ShipParticleSystem = clone.GetComponent<ParticleSystem>();
            ParticleAttractor particleAttractor = clone.GetComponent<ParticleAttractor>();
            particleAttractor.Target = planet.transform;
            
            return clone;
        }

        private GameObject CreateStar(Object prefab, Vector3 position, float angle, float planetRadius)
        {
            GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            var starOffset = new Vector3(Mathf.Cos(angle) * planetRadius * starDistanceMultiplier, Mathf.Sin(angle) * planetRadius * starDistanceMultiplier);
            clone.transform.position = position + starOffset;
            return clone;
        }
    }
}
