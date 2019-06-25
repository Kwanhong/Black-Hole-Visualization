using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static BlackHoleVisualization.Constants;

namespace BlackHoleVisualization
{
    public static class Constants
    {
        public const float C = 30; // 299792458f in real world;
        public const float G = 06; // 0.00000006673f in real world;
        public const float DT = 0.1f;

        public const uint WinSizeX = 640;
        public const uint WinSizeY = 360;

    }

    public class Data
    {
        public static RenderWindow window =
        new RenderWindow
        (
            new VideoMode(WinSizeX, WinSizeY),
            "BLACK HOLE VISUALIZATION",
            Styles.Titlebar,
            new ContextSettings(1, 1, 8)
        );
    }

    public class Utility
    {
        public static float Magnitude(Vector2f vector)
        {
            return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }
    }

    public class MainApp
    {
        static void Main(string[] args)
        {
            Game game = new Game();
        }

    }


}
