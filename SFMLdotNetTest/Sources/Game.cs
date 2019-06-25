using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static BlackHoleVisualization.Constants;
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
            window.SetFramerateLimit(60);
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;

            blackHole = new BlackHole();
            particles = new List<Photon>();

            //var start = blackHole.Position.Y - 303.84f;
            //var end = blackHole.Position.Y - 299.304;
            var start = blackHole.Position.Y - WinSizeY / 2;
            var end = blackHole.Position.Y + WinSizeY / 2;
            for (var y = start; y < end; y += 1f)
                particles.Add(new Photon(blackHole.Position.X + 640, y));
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
            window.Clear(new Color(45, 45, 45));
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

        #endregion

    }
}