using System;

namespace TinyRaytracer
{
    struct Color
    {
        public static Color White = new Color(255, 255, 255);
        public static Color Black = new Color(0, 0, 0);
        public static Color Red = new Color(255, 0, 0);
        public static Color Green = new Color(0, 255, 0);
        public static Color Blue = new Color(0, 0, 255);

        public byte r;
        public byte g;
        public byte b;
        public byte a;

        public Color(byte r, byte g, byte b, byte a = 255)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(float r, float g, float b, float a = 1.0f)
        {
            this.r = (byte)(Math.Max(0.0f, Math.Min(1.0f, r)) * 255);
            this.g = (byte)(Math.Max(0.0f, Math.Min(1.0f, g)) * 255);
            this.b = (byte)(Math.Max(0.0f, Math.Min(1.0f, b)) * 255);
            this.a = (byte)(Math.Max(0.0f, Math.Min(1.0f, a)) * 255);
        }
    }
}
