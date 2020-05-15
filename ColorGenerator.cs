using Godot;
using System;

public class ColorGenerator
{
    ColorSettings Settings { get; set; }
    GradientTexture texture;
    public int Resolution { get; }
    int textureResolution = 50;

    public ColorGenerator(ColorSettings settings, int resolution)
    {
        Settings = settings;
        Resolution = resolution;
        texture = new GradientTexture();
        texture.Width = textureResolution;
    }

    public void UpdateElevation(MinMax elevationMinMax, Vector3 objCenter)
    {
        ShaderMaterial mat = Settings.PlanetMaterial;
        if (mat == null)
            throw new Exception("In color settings, the material is expected to be a ShaderMaterial");
        var minMax = new Vector2(elevationMinMax.Min, elevationMinMax.Max);
        texture.Gradient = Settings.PlanetColour;
        mat.SetShaderParam("elevation", minMax);
        mat.SetShaderParam("center", objCenter);
        mat.SetShaderParam("gradient", texture);


    }
}

