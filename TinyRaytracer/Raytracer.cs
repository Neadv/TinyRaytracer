using System;
using System.Collections.Generic;

namespace TinyRaytracer
{
    class Raytracer
    {
        public List<Sphere> Spheres { get; } = new List<Sphere>();
        public List<Light> Lights { get; } = new List<Light>();

        private int _width;
        private int _height;

        private Vector3 _camPos = new Vector3();
        private float _fov = (float)Math.PI / 2.0f;
        private Color _lightColor = Color.White;

        private FrameBuffer _frame;

        public Raytracer(int width, int height, FrameBuffer frameBuffer)
        {
            _width = width;
            _height = height;
            _frame = frameBuffer;
        }

        public void Render()
        {
            float aspect_ratio = (float)_width / _height;
            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    float x = (2 * (i + 0.5f) / _width - 1) * (float)Math.Tan(_fov / 2) * aspect_ratio;
                    float y = -(2 * (j + 0.5f) / _height - 1) * (float)Math.Tan(_fov / 2);
                    Vector3 dir = (new Vector3(x, y, -1)).Normalize();
                    _frame.SetPixel(i, j, CastRay(dir));
                }
            }
        }

        private Color CastRay(Vector3 dir)
        {
            Vector3 point, normal;
            Material material;

            if (!SceneIntersect(dir, out point, out normal, out material))
                return new Color(50, 210, 230);


            float diffuseIntensity = 0;
            float specularIntensity = 0;
            foreach (var l in Lights)
            {
                var lightDir = (l.Position - point).Normalize();

                diffuseIntensity += l.Intensity * Math.Max(0f, normal.Dot(lightDir));
                specularIntensity += (float)Math.Pow(Math.Max(0f, Reflect(lightDir, normal).Dot(dir)), material.SpecularExponent) * l.Intensity;
            }
            return material.DiffuseCollor * (diffuseIntensity * material.Albedo[0]) + _lightColor*(specularIntensity*material.Albedo[1]);
        }

        private bool SceneIntersect(Vector3 dir, out Vector3 hit, out Vector3 normal, out Material material)
        {
            float sphereDist = float.MaxValue;
            hit = new Vector3();
            normal = new Vector3();
            material = new Material();
            foreach (var s in Spheres)
            {
                float dist;
                if(s.RayIntersect(_camPos, dir, out dist) && dist < sphereDist)
                {
                    sphereDist = dist;
                    material = s.Material;
                    hit = _camPos + dir * dist;
                    normal = (hit - s.Center).Normalize();
                }
            }

            return sphereDist < 1000;
        }

        private Vector3 Reflect(Vector3 I, Vector3 N)
        {
            return I -  N *2 * I.Dot(N);
        }
    }
}
