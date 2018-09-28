using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
using System.Reflection;

public static class GameDifficulty
{
    public enum Difficulty
    {
        Unknown,
        Easy,
        Medium,
        Hard
    }

    public class GameSettings
    {
        private readonly Difficulty difficulty;
        private readonly int planetSpacing;
        private readonly float gravity;

        public GameSettings(Difficulty difficulty, int planetSpacing, float gravity)
        {
            this.difficulty = difficulty;
            this.planetSpacing = planetSpacing;
            this.gravity = gravity;
        }

        public Difficulty Difficulty
        {
            get
            {
                return this.difficulty;
            }
        }

        public string DifficultyString
        {
            get
            {
                return this.difficulty.ToString().Split('.').Last().ToLower();
            }
        }

        public int PlanetSpacing
        {
            get
            {
                return this.planetSpacing;
            }
        }

        public float Gravity
        {
            get
            {
                return this.gravity;
            }
        }
    }

    private static GameSettings[] settings = new GameSettings[]
    {
        new GameSettings(Difficulty.Easy, 15, 1f),
        new GameSettings(Difficulty.Medium, 11, 2f),
        new GameSettings(Difficulty.Hard, 8, 3f)
    };

    private static GameSettings activeSettings = settings.First();

    public static void SetDifficulty(Difficulty difficulty)
    {
        activeSettings = settings.Single(setting => setting.Difficulty == difficulty);
    }

    public static GameSettings Settings
    {
        get
        {
            return activeSettings;
        }
    }
}
