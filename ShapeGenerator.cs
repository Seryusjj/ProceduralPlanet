using Godot;



public class ShapeGenerator
{
    public ShapeSettings Settings { get; set; }
    public NoiseFilter noiseFilter;

    public ShapeGenerator(ShapeSettings settings)
    {
        Settings = settings;
        noiseFilter = new NoiseFilter(Settings.NoiseSettings);
    }

    public Vector3 CalculatePopintOnPlanet(Vector3 pointOnUnitSphere)
    {
        float elevation = noiseFilter.Evaluate(pointOnUnitSphere);
        return pointOnUnitSphere * Settings.PlanetRadius * (1 + elevation);
    }

}

