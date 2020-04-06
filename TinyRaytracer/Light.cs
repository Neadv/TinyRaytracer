
namespace TinyRaytracer
{
    struct Light
    {
        public Vector3 Position;
        public float Intensity;

        public Light(Vector3 position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }
    }
}
