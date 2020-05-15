using Godot;

public class SimpleNoiseFilter : INoiseFilter
{

    // simplex noise implementation
    Noise noise = new Noise();
    NoiseSettingsLayer settings;

    public SimpleNoiseFilter(NoiseSettingsLayer settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.BaseRoughness;
        float amplitude = 1;
        for (int i = 0; i < settings.NumLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.Centre);
            noiseValue += (v + 1) * .5f * amplitude;
            frequency *= settings.Roughness;
            amplitude *= settings.Persistence;
        }
        noiseValue = Mathf.Max(0, noiseValue - settings.MinValue);
        return noiseValue * settings.Strength;
    }


}

