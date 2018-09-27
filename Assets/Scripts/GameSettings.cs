using UnityEngine;
using UnityEditor;
using System.Linq;

public static class GameSettings
{
    private static bool adsEnabled = true;
    
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
}