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

            Raytracer raytracer = new Raytracer(width, height, frameBuffer);

            Material ivory = new Material(new Color(120, 120, 90));
            Material red = new Material(new Color(100, 30, 30));
            
            raytracer.Spheres.Add(new Sphere(new Vector3(-4,-1,-12),2.5f, ivory));
            raytracer.Spheres.Add(new Sphere(new Vector3(-3, 1.5f, -10), 2f, red));
            raytracer.Spheres.Add(new Sphere(new Vector3(0, 2, -15), 3, red));
            raytracer.Spheres.Add(new Sphere(new Vector3(6, -5, -15), 4, ivory));
            
            raytracer.Render();
            
            var image = frameBuffer.GetBitmap();
            image.Save("Render.png");
        }
    }
}
