using UnityEngine;
using UnityEditor;

public class Ship
{
    private readonly float acceleration = 0f;
    private readonly Sprite sprite = null;

    public Ship(float acceleration, string spriteName)
    {
        this.acceleration = acceleration;
        this.sprite = LoadSprite(spriteName);
    }

    private Sprite LoadSprite(string spriteName)
    {
        var texture = Resources.Load<Texture2D>(string.Format("Sprites/Ship{0}", spriteName));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public float Acceleration
    {
        get
        {
            return acceleration;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }
}