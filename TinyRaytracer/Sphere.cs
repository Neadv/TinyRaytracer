using System;

namespace TinyRaytracer
{
    class Sphere
    {
        public Vector3 Center { get; }
        public float Radius { get; }
        public Material Material { get; }

        public Sphere(Vector3 center, float radius, Material material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public bool RayIntersect(Vector3 orig, Vector3 dir, out float dist)
        {
            Vector3 vpc = Center - orig;
            Vector3 pc = dir * dir.Dot(vpc) + orig;
            if ((pc - Center).Length > Radius || vpc.Dot(dir) < 0)
            {
                dist = -1;
                return false;
            }
            float d = (float)Math.Sqrt(Radius * Radius - Math.Pow((pc - Center).Length, 2));
            dist = (pc - orig).Length - d;
            if (dist < 0)
                dist = (pc - orig).Length + d;
            return true;
        }
    }
}
