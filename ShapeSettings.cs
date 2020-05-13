using Godot;
using System.Collections.Generic;
using System.Linq;

[Tool]
public class ShapeSettings : Resource
{



    public List<NoiseSettingsLayer> _layers = new List<NoiseSettingsLayer>();
    [Export]
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
