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
        public enum States { Idle, HightLighted, Pressed }
        public Vector2f Position { get; set; }
        public float Mass { get; set; }
        public float Radious { get; set; }
        public States MouseState
        {
            get
            {
                return mouseState;
            }
            set
            {
                mouseState = value;
                switch (mouseState)
                {
                    case States.Idle:
                        brightness = 25;
                        break;
                    case States.HightLighted:
                        brightness = 27;
                        break;
                    case States.Pressed:
                        brightness = 20;
                        break;
                }
            }
        }
        private States mouseState = States.Idle;
        private byte brightness = 40;

        public BlackHole(float x = WinSizeX / 2f, float y = WinSizeY / 2f, float m = 6000)
        {
            this.Position = new Vector2f(x, y);
            this.Mass = m;
            this.Radious = (2 * G * this.Mass) / (C * C);
            this.MouseState = States.Idle;
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
            blackHole.FillColor = new Color(brightness, brightness, brightness);
            blackHole.OutlineThickness = 0f;
            window.Draw(blackHole);
            FadingOutline(ref blackHole, 10, 1.1f);
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
            FadingOutline(ref accretionDisk);
            FadingInline(ref accretionDisk);
        }

        private void DisplayPhotonDisk()
        {
            CircleShape photonDisk = new CircleShape(this.Radious * 1.5f + 16, 32);
            photonDisk.Origin = new Vector2f(photonDisk.Radius, photonDisk.Radius);
            photonDisk.Position = this.Position;
            photonDisk.FillColor = Color.Transparent;
            photonDisk.OutlineThickness = 32;
            photonDisk.OutlineColor = new Color(42, 42, 42);
            window.Draw(photonDisk);
            FadingOutline(ref photonDisk);
            FadingInline(ref photonDisk);
        }

        private void FadingOutline(ref CircleShape shape, int fadeLevel = 5, float scaleFactor = 1.25f){
            CircleShape temp = new CircleShape(shape);
            for (int i = 0; i < fadeLevel; i++) {
                shape.Radius += i * scaleFactor;
                shape.Origin =  new Vector2f(shape.Radius, shape.Radius);
                shape.FillColor *= new Color(255, 255, 255, 200);
                shape.OutlineColor *= new Color(255, 255, 255, 200);
                window.Draw(shape);
            }
            shape = temp;
        }
        private void FadingInline(ref CircleShape shape, int fadeLevel = 5, float scaleFactor = 1.25f){
            CircleShape temp = new CircleShape(shape);
            for (int i = 0; i < fadeLevel; i++) {
                shape.Radius -= i * scaleFactor;
                shape.Origin =  new Vector2f(shape.Radius, shape.Radius);
                shape.FillColor *= new Color(255, 255, 255, 200);
                shape.OutlineColor *= new Color(255, 255, 255, 200);
                window.Draw(shape);
            }
            shape = temp;
        }

    }
}