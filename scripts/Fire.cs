using Godot;
using System;

public class Fire
{
    private Color color;
    private Vector2 position;
    private Vector2 size;
    private int intensity;

    public Fire(Vector2 position, Vector2 size, int intensity=0)
    {
        this.position = position;
        this.size = size;
        this.SetIntensity(intensity);
    }

    public void SetIntensity(int val)
    {
        this.intensity = val;
        this.color = ColorPallete.GetColorInteger(this.intensity);
    }

    public int GetIntensity()
    {
        return this.intensity;
    }

    public Color GetColor()
    {
        return this.color;
    }

    public Vector2 GetPosition()
    {
        return this.position;
    }

    public Vector2 GetSize()
    {
        return this.size;
    }
}
