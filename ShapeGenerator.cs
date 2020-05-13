using Godot;
using System.Collections.Generic;
using System.Linq;


public class ShapeGenerator
{
	public ShapeSettings Settings { get; set; }
	public NoiseFilter[] noiseFilters;
	public List<NoiseSettingsLayer> settingsLayers;

	public ShapeGenerator(ShapeSettings settings)
	{
		Settings = settings;
		settingsLayers = settings.Layers.Where(x => x != null).ToList();
		noiseFilters = new NoiseFilter[settingsLayers.Count];
		for (int i = 0; i < noiseFilters.Length; i++)
		{
			noiseFilters[i] = new NoiseFilter(settingsLayers[i]);
		}
	}

	public Vector3 CalculatePopintOnPlanet(Vector3 pointOnUnitSphere)
	{
		float firstLayerValue = 0;
		float elevation = 0;
		if (noiseFilters.Length > 0) 
		{
			firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
			if (settingsLayers[0].Enabled) 
			{
				elevation = firstLayerValue;
			}
		}
		for (int i = 1; i < noiseFilters.Length; i++)
		{
			if (settingsLayers[i].Enabled)
			{
				float mask = settingsLayers[i].UseFirstLayerAsMask ? firstLayerValue : 1;
				elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
			}
		}
		
		return pointOnUnitSphere * Settings.PlanetRadius * (1 + elevation);
	}

}

 
