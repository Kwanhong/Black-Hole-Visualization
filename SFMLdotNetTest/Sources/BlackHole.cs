using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using static BlackHoleVisualization.Constants;
using static BlackHoleVisualization.Data;
using static BlackHoleVisualization.Utility;

namespace BlackHoleVisualization
{
    public class BlackHole
    {
        public Vector2f Position { get; set; }
        public float Mass { get; set; }
        public float Radious { get; set; }

        public BlackHole(float x = WinSizeX / 2f, float y = WinSizeY / 2f, float m = 3000)
        {
            this.Position = new Vector2f(x, y);
            this.Mass = m;
            this.Radious = (2 * G * this.Mass) / (C * C);
        }

        public void Attract(Photon photon){
            Vector2f force = this.Position - photon.Position;
            float distance = GetMagnitude(force);
            float forceOfGravity = G * this.Mass / (distance * distance);
            force = SetMagnitude(force, forceOfGravity);
            photon.Velocity += force;
            photon.Velocity = Limit(photon.Velocity, C);
        }

        public void DisPlay(){
            CircleShape blackHole = new CircleShape(this.Radious);
            blackHole.Origin = new Vector2f(blackHole.Radius, blackHole.Radius);
            blackHole.Position = this.Position;
            blackHole.FillColor = new Color(20, 20, 20);
            blackHole.OutlineThickness = 0f;
            window.Draw(blackHole);

            CircleShape accretionDisk = new CircleShape(this.Radious * 3 + 16, 64);
            accretionDisk.Origin = new Vector2f(accretionDisk.Radius, accretionDisk.Radius);
            accretionDisk.Position = this.Position;
            accretionDisk.FillColor = Color.Transparent;
            accretionDisk.OutlineThickness = 32;
            accretionDisk.OutlineColor = new Color(75, 75, 75);
            window.Draw(accretionDisk);

            CircleShape photon = new CircleShape(this.Radious * 1.5f + 8, 32);
            photon.Origin = new Vector2f(photon.Radius, photon.Radius);
            photon.Position = this.Position;
            photon.FillColor = Color.Transparent;
            photon.OutlineThickness = 16;
            photon.OutlineColor = new Color(100, 100, 100);
            window.Draw(photon);
        }
    }
}