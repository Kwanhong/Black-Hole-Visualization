using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static BlackHoleVisualization.Constants;
using static BlackHoleVisualization.Utility;
using static BlackHoleVisualization.Data;

namespace BlackHoleVisualization
{
    public class PhotonBall
    {
        List<Photon> photonList;

        Vector2f position;
        Vector2f velocity;
        Vector2f offset;

        int radious;
        int count;

        public PhotonBall(List<Photon> photonList, Vector2f offset)
        {
            this.photonList = photonList;
            this.offset = offset;

            this.position = new Vector2f(640, 300);
            this.velocity = new Vector2f(-C, 0);

            this.radious = 10;
            this.count = 500;
        }

        public void Generate()
        {
            for (var i = 0; i < count; i++)
            {
                var angle = Map(i, 0, count, 0, radious * 0.5f * MathF.PI);
                var spread = new Vector2f
                (
                    MathF.Cos(angle),
                    MathF.Sin(angle)
                );
                spread = SetMagnitude(spread, Map(i, 0, count, 0, radious));

                photonList.Add(new Photon(this.position + this.offset + spread, this.velocity));
            }
        }

        public void Rotate(float angle)
        {
            this.position = RotateVector(this.position, angle);
            this.velocity = RotateVector(this.velocity, angle);
        }
    }
}