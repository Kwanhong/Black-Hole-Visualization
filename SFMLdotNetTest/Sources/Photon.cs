using SFML.System;
using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using static BlackHoleVisualization.Constants;
using static BlackHoleVisualization.Data;

namespace BlackHoleVisualization
{
    public class Photon
    {
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
        public List<Vector2f> History { get; set; }

        public Photon(float x, float y)
        {
            this.Position = new Vector2f(x, y);
            this.Velocity = new Vector2f(-C, 0);
            this.History = new List<Vector2f>();
        }

        public void Update()
        {
            this.History.Add(this.Position);

            Vector2f DeltaVelocity = this.Velocity;
            DeltaVelocity *= DT;
            this.Position += DeltaVelocity;

            if (this.History.Count > 100)
            {
                History.RemoveAt(0);
            }
        }

        public void Display()
        {
            CircleShape photon = new CircleShape(0.1f);
            photon.Origin = new Vector2f(photon.Radius, photon.Radius);
            photon.OutlineThickness = 1f;
            photon.OutlineColor = Color.White;
            photon.Position = this.Position;
            window.Draw(photon);

            VertexArray line = new VertexArray(PrimitiveType.Lines);
            foreach (var pos in History)
            {
                line.Append(new Vertex(pos, Color.White));
            }
            window.Draw(line);
        }
    }
}