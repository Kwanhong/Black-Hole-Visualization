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
        public const float C = 30;
        public const float G = 04;
        public const float DT = 0.1f;

        public const uint WinSizeX = 1920;
        public const uint WinSizeY = 1080;

        public const Styles WinLayoutStyle = Styles.None;
        public const string WinTitleText = "BLACK HOLE VISUALIZATION";
        public const uint WinAntialiasingLevel = 8;

    }
}