
namespace TinyRaytracer
{
    struct Material
    {
        public Color DiffuseCollor;
        public Vector2 Albedo;
        public float SpecularExponent;

        public Material(Color diffuse, Vector2 albedo, float specularExp)
        {
            DiffuseCollor = diffuse;
            Albedo = albedo;
            SpecularExponent = specularExp; 
        }
    }
}
