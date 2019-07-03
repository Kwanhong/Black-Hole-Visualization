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
        List<Photon> photons;

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
            window.MouseMoved += OnMouseMoved;
            window.MouseButtonPressed += OnMouseButtonPressed;
            window.MouseButtonReleased += OnMouseButtonReleased;

            blackHole = new BlackHole();
            photons = new List<Photon>();

            GeneratePhotons();
        }

        private void GeneratePhotons()
        {
            photons.Clear();

            PhotonBall photonBall = new PhotonBall(photons, blackHole.Position);

            for (int i = 0; i < 8; i++)
            {
                photonBall.Generate();
                photonBall.Rotate(124.87f);
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
            foreach (var photon in photons)
                photon.Update();
        }

        public void Rneder()
        {
            blackHole.Display();

            foreach (var photon in photons)
            {
                blackHole.Attract(photon);
                photon.Update();
                photon.Display();
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
                else blackHole.MouseState = BlackHole.States.Idle;
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
            else blackHole.MouseState = BlackHole.States.Idle;
        }

        #endregion

    }
}