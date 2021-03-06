using Godot;
using System;

[Tool]
public class NoiseSettingsLayer : Resource
{

	public enum FilterType { Simple, Rigid }
	private float _strength = 1;
	private float _roughness = 2;
	private Vector3 _centre;
	private float minValue = 0;
	private int numLayers = 5;
	private float persistence = .5f;
	private float baseRoughness = 1;
	private bool _useFirstLayerAsMask;
	private FilterType _currentFilterType;
	private float _weightMultiplier = .8f;

	[Export]
	public float WeightMultiplier
	{
		get => _weightMultiplier; set
		{
			_weightMultiplier = value;
			OnPropertyChanged();
		}
	}

	[Export]
	public FilterType CurrentFilterType
	{
		get => _currentFilterType;
		set
		{
			_currentFilterType = value;
			OnPropertyChanged();
		}
	}
	[Export]
	public bool UseFirstLayerAsMask
	{
		get { return _useFirstLayerAsMask; }
		set
		{

			_useFirstLayerAsMask = value;
			OnPropertyChanged();

		}
	}

	bool _enabled;
	[Export]
	public bool Enabled
	{
		get { return _enabled; }
		set
		{

			_enabled = value;
			OnPropertyChanged();

		}
	}

	[Export]
	public float Roughness
	{
		get { return _roughness; }
		set
		{
			if (_roughness != value)
			{
				_roughness = value;
				OnPropertyChanged();
			}
		}
	}

	[Export]
	public float MinValue
	{
		get => minValue;
		set
		{
			minValue = value;
			OnPropertyChanged();
		}
	}

	[Export(PropertyHint.Range, "1,9")]
	// range 1 -8
	public int NumLayers
	{
		get => numLayers;
		set
		{
			numLayers = value;
			OnPropertyChanged();
		}
	}

	[Export]
	// amplitude per layer
	public float Persistence
	{
		get => persistence;
		set
		{
			persistence = value;
			OnPropertyChanged();
		}
	}

	[Export]
	public float BaseRoughness
	{
		get => baseRoughness;
		set
		{
			baseRoughness = value;
			OnPropertyChanged();
		}
	}
	[Export]
	public float Strength
	{
		get { return _strength; }
		set
		{
			_strength = value;
			OnPropertyChanged();
		}
	}


	[Export]
	public Vector3 Centre
	{
		get { return _centre; }
		set
		{
			_centre = value;
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
