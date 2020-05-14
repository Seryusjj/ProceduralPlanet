using Godot;
using System;

[Tool]
public class ColorSettings : Resource
{

	private Gradient _planetColour;


	Material _planetMaterial;

	ColorSettings()
	{
	   // _planetMaterial.VertexColorIsSrgb = true;
	   // _planetMaterial.VertexColorUseAsAlbedo = true;
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
	public Material PlanetMaterial
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

