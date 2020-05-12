using Godot;


[Tool]
public class ShapeSettings : Resource
{
	private NoiseSettings _noiseSettings;
	[Export]
	public NoiseSettings NoiseSettings
	{
		get
		{
			return _noiseSettings;
		}
		set
		{
			_noiseSettings = value;
			if (_noiseSettings != null)
				_noiseSettings.Connect(nameof(NoiseSettings.PropertyChanged), this, nameof(OnPropertyChanged));
		}
	}

	private float _planetRadius = 1;
	[Export]
	public float PlanetRadius
	{
		get
		{
			return _planetRadius;
		}
		set
		{
			if (value != _planetRadius && value > 0)
			{
				_planetRadius = value;
				OnPropertyChanged();
			}
			else if (value <= 0)
			{
				_planetRadius = 0;
			}

		}
	}


	private int _resolution = 14;
	[Export]
	public int Resolution
	{
		get
		{
			return _resolution;
		}
		set
		{
			if (value != _resolution && value >= 2 && value <= 200)
			{
				_resolution = value;
				OnPropertyChanged();
			}
			else if (value < 2)
			{
				_resolution = 2;
			}
			else if (value > 200)
			{
				_resolution = 200;
			}
		}
	}

	public void OnPropertyChanged()
	{
		EmitSignal(nameof(PropertyChanged));
	}

	[Signal]
	public delegate void PropertyChanged();
}
