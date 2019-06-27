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

            Vector2f[] pos = new Vector2f[4]
            {
                new Vector2f(640, 300),
                new Vector2f(-300, +640),
                new Vector2f(-640, -300),
                new Vector2f(+300, -640)
            };
            Vector2f[] vel = new Vector2f[4] 
            { 
                new Vector2f(-C, 0), 
                new Vector2f(0, -C), 
                new Vector2f(C, 0), 
                new Vector2f(0, C) 
            };

            for (int j = 0; j < 4; j++)
            {
                for (var i = 0; i < count; i++)
                {
                    var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                    offset = blackHole.Position + SetMagnitude(offset, rnd.Next(10));

                    particles.Add(new Photon(pos[j] + offset, vel[j]));
                }
                    pos[j] = RotateVector(pos[j], 124.87f);
                    vel[j] = RotateVector(vel[j], 124.87f);

                for (var i = 0; i < count; i++)
                {
                    var offset = new Vector2f((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
                    offset = blackHole.Position + SetMagnitude(offset, rnd.Next(10));

                    particles.Add(new Photon(pos[j] + offset, vel[j]));
                }
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