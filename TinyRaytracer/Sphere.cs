using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyRaytracer
{
    class Sphere
    {
        private Vector3 _center;
        private float _radius;

        public Sphere(Vector3 center, float radius)
        {
            _center = center;
            _radius = radius;
        }

        public bool RayIntersect(Vector3 orig, Vector3 dir, ref float dist)
        {
            Vector3 pc = _center - orig;
            Vector3 pcA = dir * dir.Dot(pc);
            if ((pcA - _center).Length() > _radius)
                return false;
            float c = (float)Math.Sqrt(_radius * _radius - Math.Pow((_center - pcA).Length(), 2));
            dist = pcA.Length() - c;
            if (pc.Length() < _radius)
                dist = pcA.Length() + c;
            if (pc.Dot(dir) > 0)
                dist = c - pcA.Length();
            return true;
        }
    }
}
