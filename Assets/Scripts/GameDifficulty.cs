using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

public static class GameDifficulty
{
    public static event System.EventHandler SettingsChanged;

    public enum Difficulty
    {
        Unknown,
        Easy,
        Medium,
        Hard
    }

    public class GameSettings
    {
        public GameSettings(Difficulty difficulty, int planetSpacing, float gravity, float noise, int renderedPlanets)
        {
            Difficulty = difficulty;
            PlanetSpacing = planetSpacing;
            Gravity = gravity;
            Noise = noise;
            RenderedPlanets = renderedPlanets;
        }

        public Difficulty Difficulty { get; private set; }

        public string DifficultyString
        {
            get
            {
                return Difficulty.ToString().Split('.').Last().ToLower();
            }
        }

        public int PlanetSpacing { get; private set; }

        public float Noise { get; private set; }

        public int RenderedPlanets { get; private set; }

        public float Gravity { get; private set; }
    }

    private static GameSettings[] settings = new GameSettings[]
    {
        new GameSettings(Difficulty.Easy, 15, 1f, 1.5f, 4),
        new GameSettings(Difficulty.Medium, 11, 2f, 1.5f, 5),
        new GameSettings(Difficulty.Hard, 8, 3f, 1.5f, 6)
    };

    private static GameSettings activeSettings = settings.First();

    public static void SetDifficulty(Difficulty difficulty)
    {
        activeSettings = settings.Single(setting => setting.Difficulty == difficulty);
        if(SettingsChanged != null)
        {
            SettingsChanged(null, System.EventArgs.Empty);
        }
    }

    public static GameSettings Settings
    {
        get
        {
            return activeSettings;
        }
    }
}
