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
            "BLACK HOLE VISUALIZATION",
            Styles.Resize,
            new ContextSettings(1, 1, 8)
        );
    }
}