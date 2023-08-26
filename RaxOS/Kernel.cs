using Cosmos.System.Graphics;
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

        private readonly Bitmap bitmap = new Bitmap(10, 10,
                new byte[] { 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255,
                    23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 23, 59, 88, 255, 153, 57, 12, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 153, 57, 12, 255, 23, 59, 88, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72, 72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0,
                    255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 72, 72,
                    72, 255, 72, 72, 72, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255,
                    10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255,
                    243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148, 255, 10, 66, 148,
                    255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255,
                    0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, 0, 255, 243, 255, }, ColorDepth.ColorDepth32);

        protected override void BeforeRun()
        {
            // If all works correctly you should not really see this :-)
            Console.WriteLine("Cosmos booted successfully. Let's go in Graphical Mode");

            /*
            You don't have to specify the Mode, but here we do to show that you can.
            To not specify the Mode and pick the best one, use:
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            */
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1920, 1080, ColorDepth.ColorDepth32));

            // This will clear the canvas with the specified color.
            canvas.Clear(Color.Blue);
        }

        protected override void Run()
        {
            try
            {
                // A red Point
                canvas.DrawPoint(Color.Red, 69, 69);

                // A GreenYellow horizontal line
                canvas.DrawLine(Color.GreenYellow, 250, 100, 400, 100);

                // An IndianRed vertical line
                canvas.DrawLine(Color.IndianRed, 350, 150, 350, 250);

                // A MintCream diagonal line
                canvas.DrawLine(Color.MintCream, 250, 150, 400, 250);

                // A PaleVioletRed rectangle
                canvas.DrawRectangle(Color.PaleVioletRed, 350, 350, 80, 60);

                // A LimeGreen rectangle
                canvas.DrawRectangle(Color.LimeGreen, 450, 450, 80, 60);

                // A bitmap
                canvas.DrawImage(bitmap, 100, 150);

                canvas.Display(); // Required for something to be displayed when using a double buffered driver

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
