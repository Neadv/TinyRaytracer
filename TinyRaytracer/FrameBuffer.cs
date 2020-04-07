using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace TinyRaytracer
{
    class FrameBuffer
    {
        public int Width { get; set; }
        public int Height { get; set; }

        private Color[,] _image;

        public FrameBuffer(int width, int height)
        {
            Width = width;
            Height = height;

            _image = new Color[width, height];
        }

        public Color GetPixel(int x, int y)
        {
            return _image[x, y];
        }

        public void SetPixel(int x, int y, Color color)
        {
            _image[x, y] = color;
        }

        public void Clear()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _image[x, y] = Color.Black;
                }
            }
        }
        public void Clear(Color color)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _image[x, y] = color;
                }
            }
        }

        public Image GetBitmap()
        {
            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            BitmapData data = null;

            try
            {
                data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                int pixelSize = 4;

                int bytes = data.Stride * Height;
                byte[] rgbValues = new byte[bytes];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        int i = (x + y * Width)*pixelSize;
                        rgbValues[i + 0] = _image[x, y].b;
                        rgbValues[i + 1] = _image[x, y].g;
                        rgbValues[i + 2] = _image[x, y].r;
                        rgbValues[i + 3] = _image[x, y].a;
                    }
                }

                IntPtr ptr = data.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            }
            finally
            {
                if (data != null) bitmap.UnlockBits(data);
            }

            return bitmap;
        }
    }
}
