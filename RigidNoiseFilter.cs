using Godot;

public class RigidNoiseFilter : INoiseFilter
{

    // simplex noise implementation
    Noise noise = new Noise();
    NoiseSettingsLayer settings;

    public RigidNoiseFilter(NoiseSettingsLayer settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.BaseRoughness;
        float amplitude = 1;
        float weight = 1;
        for (int i = 0; i < settings.NumLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.Centre));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp(v * settings.WeightMultiplier, 0, 1);
            noiseValue += v * amplitude;
            frequency *= settings.Roughness;
            amplitude *= settings.Persistence;
        }
        noiseValue = Mathf.Max(0, noiseValue - settings.MinValue);
        return noiseValue * settings.Strength;
    }


}

