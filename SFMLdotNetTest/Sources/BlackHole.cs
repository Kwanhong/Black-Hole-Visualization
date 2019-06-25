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

        public BlackHole(float x = WinSizeX / 2f, float y = WinSizeY / 2f, float m = 6000)
        {
            this.Position = new Vector2f(x, y);
            this.Mass = m;
            this.Radious = (2 * G * this.Mass) / (C * C);
        }

        public void Attract(Photon photon)
        {
            Vector2f force = this.Position - photon.Position;
            float distance = GetMagnitude(force);

            if (distance < this.Radious)
            {
                photon.IsStopped = true;
                photon.Position = this.Position + SetMagnitude(-force, this.Radious);
            }

            float forceOfGravity = G * this.Mass / (distance * distance);
            force = SetMagnitude(force, forceOfGravity);
            photon.Velocity += force;
            photon.Velocity = Limit(photon.Velocity, C);
        }

        public void Display()
        {
            DisplayBlackHole();
            DisplayAccretionDisk();
            DisplayPhotonDisk();
        }

        private void DisplayBlackHole()
        {
            CircleShape blackHole = new CircleShape(this.Radious);
            blackHole.Origin = new Vector2f(blackHole.Radius, blackHole.Radius);
            blackHole.Position = this.Position;
            blackHole.FillColor = new Color(40, 40, 40);
            blackHole.OutlineThickness = 0f;
            window.Draw(blackHole);
        }

        private void DisplayAccretionDisk()
        {
            CircleShape accretionDisk = new CircleShape(this.Radious * 3 + 16, 64);
            accretionDisk.Origin = new Vector2f(accretionDisk.Radius, accretionDisk.Radius);
            accretionDisk.Position = this.Position;
            accretionDisk.FillColor = Color.Transparent;
            accretionDisk.OutlineThickness = 32;
            accretionDisk.OutlineColor = new Color(42, 42, 42);
            window.Draw(accretionDisk);
        }

        private void DisplayPhotonDisk()
        {
            CircleShape photonDisk = new CircleShape(this.Radious * 1.5f + 8, 32);
            photonDisk.Origin = new Vector2f(photonDisk.Radius, photonDisk.Radius);
            photonDisk.Position = this.Position;
            photonDisk.FillColor = Color.Transparent;
            photonDisk.OutlineThickness = 16;
            photonDisk.OutlineColor = new Color(42, 42, 42);
            window.Draw(photonDisk);
        }
    }
}