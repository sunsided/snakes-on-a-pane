using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic.Components;
using GameLogic.Entities;

namespace GameWindow
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateGame();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
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
        }
    }
}
