using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class Ship
{
    private static Ship[] ships = new Ship[]
    {
        new Ship(500f, "Basic"),
        new Ship(750f, "Advanced"),
        new Ship(1000f, "Powerful")
    };

    public static Ship GetShip(string name)
    {
        return ships.Single(ship => ship.shipName == name);
    }

    private readonly float acceleration = 0f;
    private readonly Object prefab = null;
    private readonly string shipName = null;

    public Ship(float acceleration, string shipName)
    {
        this.acceleration = acceleration;
        this.shipName = shipName;
        this.prefab = LoadPrefab(shipName);
    }

    private Object LoadPrefab(string spriteName)
    {
        return Resources.Load(string.Format("Prefabs/{0}Ship", shipName), typeof(GameObject));
    }

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
    }

    public Object Prefab
    {
        get
        {
            return prefab;
        }
    }
}