using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class NoiseFilter
{

    // simplex noise implementation
    Noise noise = new Noise();
    NoiseSettings settings;

    public NoiseFilter(NoiseSettings settings)
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
        //noise guives range [-1, 1] -> we need range [0,1]
        // float noiseValue = (noise.Evaluate(point * settings.Roughness + settings.Centre) + 1) * 0.5f;
        noiseValue = Mathf.Max(0, noiseValue - settings.MinValue);
        return noiseValue * settings.Strength;
    }

        
}

