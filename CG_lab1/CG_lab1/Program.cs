using System;
using Engine;

namespace CG_lab1
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Engine.Engine.RunGame(new Game1());
        }
    }
}
