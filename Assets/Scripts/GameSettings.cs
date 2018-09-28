using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public static class GameSettings
{
    private static bool adsEnabled = true;
    private static Dictionary<ShipType, Ship> ships = new Dictionary<ShipType, Ship>()
    {
        { ShipType.Basic, new Ship(500f, "Basic") },
        { ShipType.Advanced, new Ship(750f, "Advanced") },
        { ShipType.Powerful, new Ship(1000f, "Powerful") }
    };
    private static Ship ship = ships.First().Value;
    
    public static void DisableAds()
    {
        adsEnabled = false;
    }

    public static bool AdsEnabled
    {
        get
        {
            return adsEnabled;
        }
    }

    public static void SetShip(ShipType shipType)
    {
        ship = ships[shipType];
    }

    public static Ship Ship
    {
        get
        {
            return ship;
        }
    }
       

    public enum ShipType
    {
        Basic,
        Advanced,
        Powerful
    }
}