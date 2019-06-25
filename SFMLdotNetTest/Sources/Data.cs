using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static BlackHoleVisualization.Constants;

namespace BlackHoleVisualization
{
    public class Data
    {
        public static RenderWindow window =
        new RenderWindow
        (
            new VideoMode(WinSizeX, WinSizeY),
            WinTitleText,
            WinLayoutStyle,
            new ContextSettings(1, 1, WinAntialiasingLevel)
        );
    }
}