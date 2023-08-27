using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;

namespace RaxOS
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;

        

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Let's go in Graphical Mode");
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1920, 1080, ColorDepth.ColorDepth32));
            canvas.Clear(Color.Blue);
        }

        protected override void Run()
        {
            try
            {
                Boot.BootSys();
                Console.ReadKey();
                Sys.Power.Shutdown();
            }
            catch (Exception e)
            {
                mDebugger.Send("Exception occurred: " + e.Message);
                Sys.Power.Shutdown();
            }
        }
    }
}
