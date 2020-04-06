using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Vector3 pc = Center - orig;
            Vector3 pcA = dir * dir.Dot(pc);
            if ((pcA - Center).Length() > Radius)
            {
                dist = -1;
                return false;
            }
            float c = (float)Math.Sqrt(Radius * Radius - Math.Pow((Center - pcA).Length(), 2));
            dist = pcA.Length() - c;
            if (pc.Length() < Radius)
                dist = pcA.Length() + c;
            if (pc.Dot(dir) < 0)
                dist = c - pcA.Length();
            return true;
        }
    }
}
