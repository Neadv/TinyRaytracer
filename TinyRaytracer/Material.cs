
namespace TinyRaytracer
{
    struct Material
    {
        public Color DiffuseCollor;

        public Material(Color diffuse)
        {
            DiffuseCollor = diffuse;
        }
    }
}
