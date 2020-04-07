using System;

namespace TinyRaytracer
{
    struct Vector2
    {
        public float x;
        public float y;

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y);
            }
        }

        public float this[int index]
        {
            get
            {
                if (index == 0) return x;
                if (index == 1) return y;
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0) x = value;
                if (index == 1) y = value;
                throw new IndexOutOfRangeException();
            }
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Normalize() => this / Length;

        public float Dot(Vector2 other) => this.x * other.x + this.y * other.y;

        public static Vector2 operator +(Vector2 v1, Vector2 v2) => new Vector2(v1.x + v2.x, v1.y + v2.y);

        public static Vector2 operator -(Vector2 v1, Vector2 v2) => new Vector2(v1.x - v2.x, v1.y - v2.y);

        public static Vector2 operator *(Vector2 v, float value) => new Vector2(v.x * value, v.y * value);

        public static Vector2 operator /(Vector2 v, float value) => v * (1.0f / value);
    }

    struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        public float this[int index]
        {
            get
            {
                if (index == 0) return x;
                if (index == 1) return y;
                if (index == 2) return z;
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0) x = value;
                if (index == 1) y = value;
                if (index == 2) z = value;
                throw new IndexOutOfRangeException();
            }
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Normalize() => this / Length;

        public float Dot(Vector3 other) => this.x * other.x + this.y * other.y + this.z * other.z;

        public static Vector3 operator +(Vector3 v1, Vector3 v2) => new Vector3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);

        public static Vector3 operator -(Vector3 v1, Vector3 v2) => new Vector3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);

        public static Vector3 operator *(Vector3 v, float value) => new Vector3(v.x * value, v.y * value, v.z * value);

        public static Vector3 operator /(Vector3 v, float value) => v * (1.0f / value);
    }

    struct Vector4
    {
        private float v1;
        private float v2;
        private float v3;
        private float v4;

        public float this[int index]
        {
            get
            {
                if (index == 0) return v1;
                if (index == 1) return v2;
                if (index == 2) return v3;
                if (index == 3) return v4;
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index == 0) v1 = value;
                if (index == 1) v2 = value;
                if (index == 2) v3 = value;
                if (index == 3) v4 = value;
                throw new IndexOutOfRangeException();
            }
        }

        public Vector4(float v1, float v2, float v3, float v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }
    }
}
