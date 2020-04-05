using System;

namespace TinyRaytracer
{
    struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float Length()
        {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        public Vector3 Normalize()
        {
            return this / Length();
        }

        public float Dot(Vector3 other)
        {
            return this.x * other.x + this.y * other.y + this.z * other.z;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
        }
        
        public static Vector3 operator *(Vector3 v, float value)
        {
            return new Vector3(v.x * value, v.y * value, v.z * value);
        }

        public static Vector3 operator /(Vector3 v, float value)
        {
            return v * (1.0f / value);
        }
    }
}
