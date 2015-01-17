using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;
using GameLogic.Components;
using GameLogic.Systems;
using GameWindow.Rendering;

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
        private static readonly ManualResetEventSlim _gameLoopStart = new ManualResetEventSlim(false);

        /// <summary>
        /// The renderer
        /// </summary>
        private static IRenderer _renderer;

        /// <summary>
        /// The processing systems
        /// </summary>
        private static IProcessEntities _systems;

        /// <summary>
        /// The entites
        /// </summary>
        private static IProcessableEntities _entites;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initializes the game world
            _entites = CreateGame();

            // cancellation tokens for game shutdown
            var cts = new CancellationTokenSource();
            _gameLoopCancellationToken = cts.Token;

            // initializes the game loop task
            var gameLoop = Task.Factory.StartNew(GameLoop, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            // create the buffer manager
            var bufferManager = new BufferManager(bufferCount:2);

            // create the renderer
            var renderer = new Renderer(bufferManager);
            _renderer = renderer;

            // prepares the window rendering
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // create the form and hook into the events
            var form = new MainForm();
            form.BufferFactoryReady += (sender, args) =>
                                       {
                                           Trace.TraceInformation("Initializing buffer manager.");
                                           bufferManager.Initialize(args.Factory);
                                       };
            form.BlitReady += (sender, args) =>
                              {
                                  Trace.TraceInformation("Initializing renderer.");
                                  renderer.Initialize(args.Blit);
                              };
            form.Shown += (sender, args) =>
                          {
                              Trace.TraceInformation("Unlocking game loop.");
                              _gameLoopStart.Set();
                          };
            form.Closing += (sender, args) =>
                            {
                                Trace.TraceInformation("Canceling game loop.");
                                cts.Cancel();
                                // ReSharper disable once MethodSupportsCancellation
                                gameLoop.Wait();
                            };

            // create the system manager
            var systems = new SystemManager();
            _systems = systems;

            systems.AddSystem(new InputSystem(form));
            systems.AddSystem(new VelocitySystem());
            systems.AddSystem(new PositionSystem());
            systems.AddSystem(new PreRenderSystem(bufferManager));
            systems.AddSystem(new RenderSystem(bufferManager));

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

            // fetch the renderer and the systems
            var renderer = _renderer;
            var systems = _systems;
            var entities = _entites;

            // prepare throughput measurement
            var counter = 0;
            var sw = Stopwatch.StartNew();

            // loop until the game should be left
            while (!ct.IsCancellationRequested)
            {
                Thread.Sleep(1000/*ms per second*/ / 10 /*frames per second*/);

                // process the entities
                entities.ProcessWith(systems);

                // render a frame
                renderer.Render();

                // count frames
                ++counter;
            }

            // calculate throughput
            sw.Stop();
            var throughput = counter/sw.Elapsed.TotalSeconds;
            Trace.TraceInformation("Game Loop throughput: {0} loops per second", throughput);
        }

        /// <summary>
        /// Creates the game.
        /// </summary>
        private static IProcessableEntities CreateGame()
        {
            var em = new EntityManager();

            var player = em.CreateEntity();
            player.AddComponent(new PositionComponent { X = 0F, Y = 0F });
            player.AddComponent(new ColorComponent { Color = Color.DarkGreen });
            player.AddComponent(new AABBComponent { Width = 5F, Height = 5F });
            player.AddComponent(new InputComponent());
            player.AddComponent(new VelocityComponent());

            var star = em.CreateEntity();
            star.AddComponent(new PositionComponent { X = 7F, Y = 7F });
            star.AddComponent(new ColorComponent { Color = Color.White });
            star.AddComponent(new AABBComponent { Width = 0.5F, Height = 0.5F });
            star.AddComponent(new ParallaxComponent { Distance = 1 });

            var star2 = em.CreateEntity();
            star2.AddComponent(new PositionComponent { X = 7F, Y = 7F });
            star2.AddComponent(new ColorComponent { Color = Color.White });
            star2.AddComponent(new AABBComponent { Width = 0.5F, Height = 0.5F });
            star2.AddComponent(new ParallaxComponent { Distance = 10 });

            return em;
        }
    }
}
