using Godot;
using System;

[Tool]
public class ColorSettings : Resource
{

	private Gradient _planetColour;


	ShaderMaterial _planetMaterial;

	ColorSettings()
	{
	}


	[Export]
	public Gradient PlanetColour
	{
		get
		{
			return _planetColour;
		}
		set
		{
			_planetColour = value;
			OnPropertyChanged();
		}
	}

	[Export]
	public ShaderMaterial PlanetMaterial
	{
		get => _planetMaterial;
		private set => _planetMaterial = value;
	}

	private void OnPropertyChanged()
	{
		EmitSignal(nameof(PropertyChanged));
	}

	[Signal]
	public delegate void PropertyChanged();


}

