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
    public class Game
    {
        BlackHole blackHole;
        List<Photon> particles;

        public Game()
        {
            Initialize();
            Run();
        }

        public void Initialize()
        {
            window.SetFramerateLimit(20);
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;
            window.MouseMoved += OnMouseMoved;
            window.MouseButtonPressed += OnMouseButtonPressed;
            window.MouseButtonReleased += OnMouseButtonReleased;

            blackHole = new BlackHole();
            particles = new List<Photon>();

            GeneratePhotons();
        }

        private void GeneratePhotons()
        {

            particles.Clear();

            Random rnd = new Random();
            var count = 50;

            var X = 640;
            var Y = 300;
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(X, Y);

                var vel = new Vector2f(-C, 0);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(MathF.Cos(124.87f) * X - MathF.Sin(124.87f) * Y, MathF.Sin(124.87f) * X + MathF.Cos(124.87f) * Y);

                var vel = new Vector2f(-1, -1);
                vel = SetMagnitude(vel, C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }


            X = -300;
            Y = +640;
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(X, Y);
                var vel = new Vector2f(0, -C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(MathF.Cos(124.87f) * X - MathF.Sin(124.87f) * Y, MathF.Sin(124.87f) * X + MathF.Cos(124.87f) * Y);

                var vel = new Vector2f(1, -1);
                vel = SetMagnitude(vel, C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }


            X = -640;
            Y = -300;
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(X, Y);
                var vel = new Vector2f(C, 0);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(MathF.Cos(124.87f) * X - MathF.Sin(124.87f) * Y, MathF.Sin(124.87f) * X + MathF.Cos(124.87f) * Y);

                var vel = new Vector2f(1, 1);
                vel = SetMagnitude(vel, C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }


            X = +300;
            Y = -640;
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(X, Y);
                var vel = new Vector2f(0, C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }
            for (var i = 0; i < count; i++)
            {
                var pos = blackHole.Position + new Vector2f(MathF.Cos(124.87f) * X - MathF.Sin(124.87f) * Y, MathF.Sin(124.87f) * X + MathF.Cos(124.87f) * Y);

                var vel = new Vector2f(-1, 1);
                vel = SetMagnitude(vel, C);
                var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                offset = SetMagnitude(offset, rnd.Next(10));
                particles.Add(new Photon(pos + offset, vel));
            }


        }

        public void Run()
        {
            Event mainEvent = new Event();
            while (window.IsOpen)
            {
                HandleEvent(mainEvent);
                Update();
                Rneder();
            }
        }

        public void HandleEvent(Event e)
        {
            window.DispatchEvents();
        }

        public void Update()
        {
            foreach (var particle in particles)
                particle.Update();
        }

        public void Rneder()
        {
            blackHole.Display();

            foreach (var particle in particles)
            {
                blackHole.Attract(particle);
                particle.Update();
                particle.Display();
            }

            window.Display();
            window.Clear(new Color(46, 46, 46));
        }

        #region Events

        private void OnClose(object sender, EventArgs e)
        {
            window.Close();
        }
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }

        private void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {

            Vector2f mousePos = new Vector2f(e.X, e.Y);
            if (blackHole.MouseState != BlackHole.States.Pressed)
            {
                if (Distnace(mousePos, blackHole.Position) <= blackHole.Radious)
                    blackHole.MouseState = BlackHole.States.HightLighted;
                else
                    blackHole.MouseState = BlackHole.States.Idle;
            }
        }
        private void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            Vector2f mousePos = new Vector2f(e.X, e.Y);
            if (Distnace(mousePos, blackHole.Position) <= blackHole.Radious)
                blackHole.MouseState = BlackHole.States.Pressed;
        }
        private void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            Vector2f mousePos = new Vector2f(e.X, e.Y);
            if (Distnace(mousePos, blackHole.Position) <= blackHole.Radious)
            {
                blackHole.MouseState = BlackHole.States.HightLighted;
                GeneratePhotons();
            }
            else
                blackHole.MouseState = BlackHole.States.Idle;
        }

        #endregion

    }
}