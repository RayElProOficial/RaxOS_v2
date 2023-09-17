using Cosmos.System.Graphics;
using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaxOS
{
    internal class Boot
    {
        static Canvas canvas;
        [ManifestResourceStream(ResourceName = "RaxOS.Images.boot.bmp")]
        static byte[] byte2;
        static Bitmap bootlogo = new Bitmap(byte2);
        public static void BootSys()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1920, 1080, ColorDepth.ColorDepth32));
            canvas.Clear(Color.Black);
            canvas.DrawImage(bootlogo, 660, 240);
            canvas.Display();
            DelayCode(6000);
            MainDesktop.StartDesk();
        }
        static void DelayCode(int milliseconds)
        {
            Cosmos.HAL.PIT pit = new Cosmos.HAL.PIT();
            pit.Wait((uint)milliseconds);
            pit = new Cosmos.HAL.PIT();
        }
    }
}
