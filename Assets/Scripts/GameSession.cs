using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession {

    private int score;
    private float gravity;
    private float initialGravity;

    private static GameSession current;

    public event System.EventHandler<ScoreEventArgs> ScoreChanged;
    public event System.EventHandler<GravityEventArgs> GravityChanged;
    public event System.EventHandler SpaceshipCrashed;

    public int Score
    {
        get
        {
            return score;
        }
    }

    private GameSession()
    {
        initialGravity = GameDifficulty.Settings.Gravity;
        GameDifficulty.SettingsChanged += GameDifficulty_SettingsChanged;
        gravity = initialGravity;
        score = 0;
    }

    private void GameDifficulty_SettingsChanged(object sender, System.EventArgs e)
    {
        initialGravity = GameDifficulty.Settings.Gravity;
    }

    public static GameSession Current
    {
        get
        {
            return current ?? (current = new GameSession());
        }
    }

    private void SetScore(int score)
    {
        this.score = score;
        if (ScoreChanged != null)
        {
            ScoreChanged(this, new ScoreEventArgs(score));
        }
    }

    private void SetGravity(float gravity)
    {
        this.gravity = gravity;
        if (GravityChanged != null)
        {
            GravityChanged(this, new GravityEventArgs(gravity));
        }
    }

    private void IncreaseScore(int increment)
    {
        SetScore(score + increment);
        SetGravity(initialGravity + ((float)score / 100));
    }

    public void ScoreStar()
    {
        IncreaseScore(1);
    }

    public void ScoreRareStar()
    {
        IncreaseScore(10);
    }

    public void CrashSpaceship()
    {
        if(SpaceshipCrashed != null)
        {
            SpaceshipCrashed(this, System.EventArgs.Empty);
        }
    }

    public void Reset()
    {
        SetScore(0);
        SetGravity(GameDifficulty.Settings.Gravity);
        Effects.Current.Forget();
    }

    public class ScoreEventArgs : System.EventArgs
    {
        public ScoreEventArgs(int score)
        {
            Score = score;
        }

        public int Score { get; private set; }
    }

    public class GravityEventArgs : System.EventArgs
    {
        public GravityEventArgs(float gravity)
        {
            Gravity = gravity;
        }

        public float Gravity { get; private set; }
    }
}
