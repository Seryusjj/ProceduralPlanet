using Godot;
using System;

[Tool]
public class ColorSettings : Resource
{

    private Color _planetColour;


    [Export]
    public Color PlanetColour
    {
        get
        {
            return _planetColour;
        }
        set
        {
            if (_planetColour != value)
            {
                _planetColour = value;
                EmitSignal(nameof(PropertyChanged));
            }
        }
    }


    [Signal]
    public delegate void PropertyChanged();


}

