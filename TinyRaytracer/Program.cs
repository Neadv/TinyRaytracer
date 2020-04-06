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
            
            raytracer.Spheres.Add(new Sphere(new Vector3(-4, 0.5f,-10),2.5f, ivory));
            raytracer.Spheres.Add(new Sphere(new Vector3(-3, -1.5f, -8), 2f, red));
            raytracer.Spheres.Add(new Sphere(new Vector3(0, 0, -13), 3, red));
            raytracer.Spheres.Add(new Sphere(new Vector3(6, 5, -13), 4, ivory));

            raytracer.Lights.Add(new Light(new Vector3(-10, 20, 20), 1.5f));
            
            raytracer.Render();
            
            var image = frameBuffer.GetBitmap();
            image.Save("Render.png");
        }
    }
}
