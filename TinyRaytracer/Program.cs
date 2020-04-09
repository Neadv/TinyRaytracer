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

            Material ivory = new Material(new Color(120, 120, 90), new Vector4(0.6f, 0.3f, 0.1f, 0), 50, 1);
            Material red = new Material(new Color(100, 30, 30), new Vector4(0.9f, 0.1f, 0, 0), 10, 1);
            Material mirror = new Material(new Color(255, 255, 255), new Vector4(0, 10, 0.8f, 0), 1425, 1);
            Material glass = new Material(new Color(140, 170, 200), new Vector4(0, 0.5f, 0.1f, 0.8f), 125, 1.5f);

            raytracer.Spheres.Add(new Sphere(new Vector3(-3, 0, -16), 2, ivory));
            raytracer.Spheres.Add(new Sphere(new Vector3(-1, -1.5f, -12), 2, glass));
            raytracer.Spheres.Add(new Sphere(new Vector3(1.5f, -0.5f, -18), 3, red));
            raytracer.Spheres.Add(new Sphere(new Vector3(7, 5, -18), 4, mirror));

            raytracer.Lights.Add(new Light(new Vector3(-20, 20, 20), 1.5f));
            raytracer.Lights.Add(new Light(new Vector3(30, 50, -25), 1.8f));
            raytracer.Lights.Add(new Light(new Vector3(30, 20, 30), 1.7f));

            raytracer.Render();

            var image = frameBuffer.GetBitmap();
            image.Save("Render.png");
        }
    }
}
