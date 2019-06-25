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
        public bool IsStopped { get; set; }
        public bool IsStuffed { get; set; }

        public Photon(float x, float y)
        {
            this.Position = new Vector2f(x, y);
            this.Velocity = new Vector2f(-C, 0);
            this.History = new List<Vector2f>();
        }

        public void Update()
        {
            Move();
            GenerateHistory();
        }

        private void Move()
        {
            if (this.IsStopped) return;

            Vector2f DeltaVelocity = this.Velocity;
            DeltaVelocity *= DT;
            this.Position += DeltaVelocity;
        }
        private void GenerateHistory()
        {
            if (!this.IsStopped)
                this.History.Add(this.Position);

            if (!this.IsStopped && this.History.Count > 100 ||
                (this.IsStopped && this.History.Count > 0 && !this.IsStuffed))
                History.RemoveAt(0);
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
                Color color = new Color(255, 255, 255, (byte)History.IndexOf(pos));
                Vertex vertex = new Vertex(pos, color);
                line.Append(vertex);
            }
            window.Draw(line);
        }
    }
}