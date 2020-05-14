using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettingsLayer settings) 
    {
        switch (settings.CurrentFilterType) 
        {
            case NoiseSettingsLayer.FilterType.Rigid:
                return new RigidNoiseFilter(settings);
            case NoiseSettingsLayer.FilterType.Simple:
                return new SimpleNoiseFilter(settings);
        }
        return null;
    }
}

