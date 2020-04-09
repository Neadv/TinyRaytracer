using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyRaytracer
{
    class Raytracer
    {
        public List<Sphere> Spheres { get; } = new List<Sphere>();
        public List<Light> Lights { get; } = new List<Light>();

        private int _width;
        private int _height;

        private Vector3 _camPos = new Vector3(0, 0, 0);
        private float _fov = (float)Math.PI / 3.0f;
        private Color _lightColor = Color.White;
        private int _maxDepth = 4;
        private float _aspect_ratio;

        private FrameBuffer _frame;

        public Raytracer(int width, int height, FrameBuffer frameBuffer)
        {
            _width = width;
            _height = height;
            _frame = frameBuffer;
            _aspect_ratio = (float)_width / _height; 
        }

        public void Render()
        {
            Parallel.For(0, _height, RenderParallel);
        }

        private void RenderParallel(int j)
        {
            for (int i = 0; i < _width; i++)
            {
                float x = (2 * (i + 0.5f) / _width - 1) * (float)Math.Tan(_fov / 2) * _aspect_ratio;
                float y = -(2 * (j + 0.5f) / _height - 1) * (float)Math.Tan(_fov / 2);
                Vector3 dir = (new Vector3(x, y, -1)).Normalize();
                _frame.SetPixel(i, j, CastRay(_camPos, dir));
            }
        }

        private Color CastRay(Vector3 orig, Vector3 dir, int depth = 0)
        {
            Vector3 point, normal;
            Material material;

            if (depth > _maxDepth || !SceneIntersect(orig, dir, out point, out normal, out material))
                return new Color(50, 210, 230);

            Color reflectColor = new Color();
            if (material.Albedo[2] != 0)
            {
                Vector3 reflectDir = Reflect(dir, normal);
                Vector3 reflectOrig = reflectDir.Dot(normal) < 0 ? point - normal * 0.001f : point + normal * 0.001f;
                reflectColor = CastRay(reflectOrig, reflectDir, depth + 1);
            }

            Color refractColor = new Color();
            if (material.RefractiveIndex != 1 && material.Albedo[3] != 0)
            {
                Vector3 refractDir = Refract(dir, normal, material.RefractiveIndex).Normalize();
                Vector3 refractOrig = refractDir.Dot(normal) < 0 ? point - normal * 0.001f : point + normal * 0.001f;
                refractColor = CastRay(refractOrig, refractDir, depth + 1);
            }

            float diffuseIntensity = 0;
            float specularIntensity = 0;
            foreach (var l in Lights)
            {
                var lightDir = (l.Position - point).Normalize();

                //Shadow
                var lightDist = (l.Position - point).Length;

                var shadowOrig = lightDir.Dot(normal) < 0 ? point - normal * 0.001f : point + normal * 0.001f;

                if (SceneIntersect(shadowOrig, lightDir, out Vector3 shadowPoint, out Vector3 shadowNormal, out Material tmpMat)
                   && (shadowPoint - shadowOrig).Length < lightDist)
                    continue;

                //----------

                diffuseIntensity += l.Intensity * Math.Max(0f, normal.Dot(lightDir));
                specularIntensity += (float)Math.Pow(Math.Max(0f, Reflect(lightDir, normal).Dot(dir)), material.SpecularExponent) * l.Intensity;
            }
            Color diffuse = material.DiffuseCollor * (diffuseIntensity * material.Albedo[0]);
            Color specular = _lightColor * (specularIntensity * material.Albedo[1]);
            Color reflect = reflectColor * material.Albedo[2];
            Color refract = refractColor * material.Albedo[3];

            return diffuse + specular + reflect + refract;
        }

        private bool SceneIntersect(Vector3 orig, Vector3 dir, out Vector3 hit, out Vector3 normal, out Material material)
        {
            float sphereDist = float.MaxValue;
            hit = new Vector3();
            normal = new Vector3();
            material = new Material();
            foreach (var s in Spheres)
            {
                float dist;
                if (s.RayIntersect(orig, dir, out dist) && dist < sphereDist)
                {
                    sphereDist = dist;
                    material = s.Material;
                    hit = orig + dir * dist;
                    normal = (hit - s.Center).Normalize();
                }
            }

            float checkerboardDist = float.MaxValue;
            if(Math.Abs(dir.y) > 0.001)
            {
                float d = -(orig.y + 4) / dir.y;
                Vector3 pt = orig + dir*d;
                if(d > 0 && Math.Abs(pt.x) < 10 && pt.z < -10 && pt.z > -30 && d < sphereDist)
                {
                    checkerboardDist = d;
                    hit = pt;
                    normal = new Vector3(0, 1, 0);
                    int tmp = ((int)(.5 * hit.x + 1000) + (int)(0.5 * hit.z)) & 1;
                    if (tmp == 1)
                        material.DiffuseCollor = new Color(255, 255, 255);
                    else
                        material.DiffuseCollor = new Color(255, 200, 70);
                    material.DiffuseCollor = material.DiffuseCollor * 0.3f;
                    material.Albedo = new Vector4(1,0,0,0);
                }
            }

            return Math.Min(sphereDist, checkerboardDist) < 1000;

        }

        private Vector3 Reflect(Vector3 I, Vector3 N)
        {
            return I - N * 2 * I.Dot(N);
        }
        private Vector3 Refract(Vector3 I, Vector3 N, float eta_t, float eta_i = 1.0f)
        {
            float cosi = -(float)Math.Max(-1.0f, Math.Min(1.0f, I.Dot(N)));
            if (cosi < 0) return Refract(I, -N, eta_i, eta_t); // if the ray comes from the inside the object, swap the air and the media
            float eta = eta_i / eta_t;
            float k = 1 - eta * eta * (1 - cosi * cosi);
            return k < 0 ? new Vector3(1, 0, 0) : I * eta + N * (eta * cosi - (float)Math.Sqrt(k));
        }

    }
}
