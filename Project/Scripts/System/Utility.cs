using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static BlackHoleVisualization.Constants;

namespace BlackHoleVisualization
{
    public class Utility
    {
        public static float GetMagnitude(Vector2f vector)
        {
            return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector2f SetMagnitude(Vector2f vector, float mag)
        {
            vector = Normalize(vector);
            vector *= mag;
            return vector;
        }

        public static Vector2f Normalize(Vector2f vector)
        {
            var magnitude = GetMagnitude(vector);
            return vector /= magnitude;
        }

        public static Vector2f Limit(Vector2f vector, float min, float max)
        {
            if (GetMagnitude(vector) < min)
                vector = SetMagnitude(vector, min);
            else if (GetMagnitude(vector) > max)
                vector = SetMagnitude(vector, max);
            return vector;
        }

        public static Vector2f Limit(Vector2f vector, float max)
        {
            if (GetMagnitude(vector) > max)
                vector = SetMagnitude(vector, max);
            return vector;
        }

        public static float Limit(float var, float min, float max)
        {
            if (var < min) var = min;
            else if (var > max) var = max;
            return var;
        }

        public static float Limit(float var, float max)
        {
            if (var > max) var = max;
            return var;
        }

        public static float Distnace(Vector2f pos1, Vector2f pos2)
        {
            return MathF.Sqrt
            (
                MathF.Pow(pos1.X - pos2.X, 2) +
                MathF.Pow(pos1.Y - pos2.Y, 2)
            );
        }

        public static Vector2f RotateVector(Vector2f vector, float angle)
        {
            return new Vector2f
            (
                MathF.Cos(angle) * vector.X -
                MathF.Sin(angle) * vector.Y,
                MathF.Sin(angle) * vector.X +
                MathF.Cos(angle) * vector.Y
            );
        }

        public static float Map(float value, float start1, float stop1, float start2, float stop2)
        {
            return ((value - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}