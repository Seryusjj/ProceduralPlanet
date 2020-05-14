using Godot;
using System.Collections.Generic;
using System.Linq;

[Tool]
public class ShapeSettings : Resource
{
	public List<NoiseSettingsLayer> _layers = new List<NoiseSettingsLayer>();
	[Export(PropertyHint.Range, "1,9")]
	public List<NoiseSettingsLayer> Layers
	{
		get => _layers; set
		{
			_layers = value;
			foreach (var s in _layers.Where(x => x != null))
			{
				if (!s.IsConnected(nameof(NoiseSettingsLayer.PropertyChanged), this, nameof(OnPropertyChanged)))
				{
					s.Connect(nameof(NoiseSettingsLayer.PropertyChanged), this, nameof(OnPropertyChanged));
					GD.Print("Connect");
				}
			}
		}
	}

	private float _planetRadius = 1;
	[Export(PropertyHint.Range, "1,20000")]
	public float PlanetRadius
	{
		get
		{
			return _planetRadius;
		}
		set
		{
			_planetRadius = value;
			OnPropertyChanged();
		}
	}


	private int _resolution = 14;
	[Export(PropertyHint.Range, "2,256")]
	public int Resolution
	{
		get
		{
			return _resolution;
		}
		set
		{
			_resolution = value;
			OnPropertyChanged();
		}
	}

	public void OnPropertyChanged()
	{
		EmitSignal(nameof(PropertyChanged));
	}

	[Signal]
	public delegate void PropertyChanged();
}
