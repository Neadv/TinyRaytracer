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

        public static Color operator *(Color color, float value)
        {
            return new Color((byte)(color.r * value), (byte)(color.g * value), (byte)(color.b * value));
        }

        public static Color operator +(Color c1, Color c2)
        {
            Color color;
            color.r = (byte)Math.Min(255, c1.r + c2.r);
            color.g = (byte)Math.Min(255, c1.g + c2.g);
            color.b = (byte)Math.Min(255, c1.b + c2.b);
            color.a = 255;
            return color;
        }
    }
}
