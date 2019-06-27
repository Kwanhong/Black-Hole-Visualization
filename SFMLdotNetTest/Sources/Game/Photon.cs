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
        public bool IsDisappearing
        {
            get
            {
                return this.History.Count > 100 ||
                (
                    !this.IsStuffed &&
                    this.IsStopped &&
                    this.History.Count > 0
                );
            }
        }

        public bool IsDisappeared
        {
            get
            {
                return
                !this.IsStuffed &&
                this.IsStopped &&
                this.History.Count <= 0;
            }
        }

        #region Instructor
        public Photon(float x, float y)
        {
            this.Velocity = new Vector2f(-C, 0);
            this.Position = new Vector2f(x, y);
            this.History = new List<Vector2f>();
        }

        public Photon(Vector2f pos)
        {
            this.Position = pos;
            this.Velocity = new Vector2f(-C, 0);
            this.History = new List<Vector2f>();
        }

        public Photon(Vector2f pos, Vector2f vel)
        {
            this.Velocity = vel;
            this.Position = pos;
            this.History = new List<Vector2f>();
        }

        public Photon(float x, float y, Vector2f vel)
        {
            this.Velocity = vel;
            this.Position = new Vector2f(x, y);
            this.History = new List<Vector2f>();
        }
        #endregion

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

            if (this.IsDisappearing)
                History.RemoveAt(0);
        }

        public void Display()
        {
            if (this.IsDisappeared) return;

            DisplayPhoton();
            DisplayHistory();
        }

        private void DisplayPhoton()
        {
            CircleShape photon = new CircleShape(0.1f);
            photon.Origin = new Vector2f(photon.Radius, photon.Radius);
            photon.OutlineThickness = (History.Count * 0.005f);
            photon.OutlineColor = new Color(255, 255, 255, (byte)(History.Count * 2.55f));
            photon.FillColor = new Color(255, 255, 255, (byte)(History.Count * 2.55f));
            photon.Position = this.Position;
            window.Draw(photon);

        }

        private void DisplayHistory()
        {
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