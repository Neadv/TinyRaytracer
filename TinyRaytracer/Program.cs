using System;

namespace TinyRaytracer
{
    class Program
    {
        static void Main()
        {
            int width = 1024;
            int height = 728;

            FrameBuffer frameBuffer = new FrameBuffer(width, height);

            Sphere sphere = new Sphere(new Vector3(-3, 0, -16), 4);
            float fov = (float)Math.PI/2.0f;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float xDir = (float)((2 * (x + 0.5) / (float)width - 1) * Math.Tan(fov / 2.0f) * width / (float)height);
                    float yDir = -(float)((2 * (y + 0.5) / (float)height - 1) * Math.Tan(fov / 2.0f));
                    Vector3 dir = new Vector3(xDir, yDir, -1).Normalize();
                    frameBuffer.SetPixel(x, y, CastRay(new Vector3(), dir, sphere));
                }
            }
            
            var image = frameBuffer.GetBitmap();
            image.Save("Render.png");
        }
        static Color CastRay(Vector3 orig, Vector3 dir, Sphere sphere)
        {
            float sphereDist = 0;
            if (!sphere.RayIntersect(orig, dir, ref sphereDist))
                return new Color(0.2f, 0.7f, 0.8f);
            return new Color(0.4f, 0.4f, 0.3f);
        }
    }
}
