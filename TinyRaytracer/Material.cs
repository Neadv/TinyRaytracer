
namespace TinyRaytracer
{
    struct Material
    {
        public Color DiffuseCollor;
        public Vector4 Albedo;
        public float SpecularExponent;
        public float RefractiveIndex;

        public Material(Color diffuse, Vector4 albedo, float specularExp, float refractiveIndex)
        {
            DiffuseCollor = diffuse;
            Albedo = albedo;
            SpecularExponent = specularExp;
            RefractiveIndex = refractiveIndex;
        }
    }
}
