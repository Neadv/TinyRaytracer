using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRaytracer
{
    class Program
    {
        static void Main()
        {
            FrameBuffer frameBuffer = new FrameBuffer(1024, 768);

            for (int y = 0; y < 768; y++)
            {
                for (int x = 0; x < 1024; x++)
                {
                    frameBuffer.SetPixel(x, y, new Color(y / 768.0f, x / 1024.0f, 0));
                }
            }
            
            var image = frameBuffer.GetBitmap();
            image.Save("Render.png");
        }
    }
}
