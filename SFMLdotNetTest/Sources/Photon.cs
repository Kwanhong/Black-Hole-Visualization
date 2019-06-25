using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using static BlackHoleVisualization.Constants;
using static BlackHoleVisualization.Data;

namespace BlackHoleVisualization
{
    public class Photon
    {
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }

        public Photon(float x, float y) {
            this.Position = new Vector2f(x, y);
            this.Velocity = new Vector2f(-C, 0);
        }

        public void Update(){
            Vector2f DeltaVelocity = this.Velocity;
            DeltaVelocity *= DT;
            this.Position += DeltaVelocity;
        }

        public void Display(){
            CircleShape photon = new CircleShape(0.1f);
            photon.Origin = new Vector2f(photon.Radius, photon.Radius);
            photon.OutlineThickness = 1f;
            photon.Position = this.Position;
            photon.OutlineColor = Color.White;
            window.Draw(photon);

        }
    }
}