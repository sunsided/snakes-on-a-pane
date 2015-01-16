using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic.Components;
using GameLogic.Entities;

namespace GameWindow
{
    static class Program
    {
        /// <summary>
        /// The cancellation token used to leave the game loop
        /// </summary>
        private static CancellationToken _gameLoopCancellationToken;

        /// <summary>
        /// The game loop start event
        /// </summary>
        private static ManualResetEventSlim _gameLoopStart = new ManualResetEventSlim(false);

        /// <summary>
        /// Occurs when a frame should be rendered.
        /// </summary>
        private static event EventHandler RenderFrame;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initializes the game world
            CreateGame();

            // cancellation tokens for game shutdown
            var cts = new CancellationTokenSource();
            _gameLoopCancellationToken = cts.Token;

            // initializes the game loop task
            var gameLoop = Task.Factory.StartNew(GameLoop, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            // prepares the window rendering
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create the form and hook into the events
            var form = new MainForm();
            form.Shown += (sender, args) =>
                          {
                              _gameLoopStart.Set();
                          };
            form.Closing += (sender, args) =>
                            {
                                cts.Cancel();
                                // ReSharper disable once MethodSupportsCancellation
                                gameLoop.Wait();
                            };
            RenderFrame += (sender, args) =>
                           {
                               form.RenderFrame();
                           };

            // fire in the hole!
            Application.Run(form);
        }

        /// <summary>
        /// The game loop
        /// </summary>
        private static void GameLoop()
        {
            var ct = _gameLoopCancellationToken;
            
            // wait for the game loop to start or
            // for the game to exit
            _gameLoopStart.Wait(ct);
            ct.ThrowIfCancellationRequested();

            // prepare throughput measurement
            var counter = 0;
            var sw = Stopwatch.StartNew();

            // loop until the game should be left
            while (!ct.IsCancellationRequested)
            {
                Thread.Sleep(1000/*ms per second*/ / 10 /*frames per second*/);

                // render a frame
                RenderFrame(null, EventArgs.Empty);

                // count frames
                ++counter;
            }

            // calculate throughput
            sw.Stop();
            var throughput = (double) counter/sw.Elapsed.TotalSeconds;
        }

        /// <summary>
        /// Creates the game.
        /// </summary>
        private static void CreateGame()
        {
            var player = new Entity();
            player.AddComponent(new PositionComponent { X = 0F, Y = 0F });
            player.AddComponent(new ColorComponent { Color = Color.DarkGreen });
            player.AddComponent(new AABBComponent { Width = 5F, Height = 5F });

            var star = new Entity();
            star.AddComponent(new PositionComponent { X = 7F, Y = 7F });
            star.AddComponent(new ColorComponent { Color = Color.White });
            star.AddComponent(new AABBComponent { Width = 0.5F, Height = 0.5F });
            star.AddComponent(new ParallaxComponent { Distance = 1 });

            var star2 = new Entity();
            star2.AddComponent(new PositionComponent { X = 7F, Y = 7F });
            star2.AddComponent(new ColorComponent { Color = Color.White });
            star2.AddComponent(new AABBComponent { Width = 0.5F, Height = 0.5F });
            star2.AddComponent(new ParallaxComponent { Distance = 10 });
        }
    }
}
