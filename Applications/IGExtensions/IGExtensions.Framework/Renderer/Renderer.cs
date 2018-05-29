using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace IGExtensions.Framework.Tools
{
    public class Rasterizer
    {
        public Rasterizer()
        {
            ModelViewMatrix = Matrix3D.Identity;
            ProjectionMatrix = Matrix3D.Identity;
            BackFaceCull = true;

            Light = new Vector3D(0, 0, 10);
        }

        #region WriteableBitmap, ColorBuffer, PixelWidth and PixelHeight
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Writeable")]
        public WriteableBitmap WriteableBitmap
        {
            get { return writeableBitmap; }
            set
            {
                writeableBitmap = value;

#if SILVERLIGHT
                ColorBuffer = writeableBitmap != null ? writeableBitmap.Pixels : null;
#else
                throw new NotImplementedException();
#endif
                PixelWidth = writeableBitmap != null ? writeableBitmap.PixelWidth : 0;
                PixelHeight = writeableBitmap != null ? writeableBitmap.PixelHeight : 0;
                ClipRect = new Rect(0, 0, PixelWidth, PixelHeight);
            }
        }
        private WriteableBitmap writeableBitmap;

        public double[] DepthBuffer
        {
            get;
            set;
        }

        public int[] ColorBuffer
        {
            get;
            set;
        }

        public int PixelWidth
        {
            get;
            set;
        }

        public int PixelHeight
        {
            get;
            set;
        }

        #endregion

        #region Clipping
        public Rect ClipRect
        {
            get
            {
                return new Rect(clipLeft, clipTop, clipRight - clipLeft + 1, clipBottom - clipTop + 1);
            }
            set
            {
                clipLeft = (int)Math.Floor(value.Left);
                clipTop = (int)Math.Floor(value.Top);
                clipRight = (int)Math.Floor(value.Right) - 1;
                clipBottom = (int)Math.Floor(value.Bottom) - 1;
            }
        }
        private int clipLeft;
        private int clipTop;
        private int clipRight;
        private int clipBottom;
        #endregion

        public bool BackFaceCull
        {
            get;
            set;
        }

        public Matrix3D ModelViewMatrix
        {
            get { return modelViewMatrix; }
            set
            {
                modelViewMatrix = value;

                normalMatrix=ModelViewMatrix;
                normalMatrix.Invert();
                normalMatrix=normalMatrix.GetTranspose();
                
                modelViewProjectionMatrix = ModelViewMatrix.Multiply(ProjectionMatrix);
            }
        }
        private Matrix3D modelViewMatrix= Matrix3D.Identity;

        public Matrix3D ProjectionMatrix
        {
            get { return projectionMatrix; }
            set
            {
                projectionMatrix = value;

                modelViewProjectionMatrix = ModelViewMatrix.Multiply(ProjectionMatrix);
            }
        }
        private Matrix3D projectionMatrix= Matrix3D.Identity;

        private Matrix3D normalMatrix= Matrix3D.Identity;

        public Matrix3D ModelViewProjectionMatrix
        {
            get { return modelViewProjectionMatrix; }
        }
        private Matrix3D modelViewProjectionMatrix= Matrix3D.Identity;

        public Vector3D Light
        {
            get { return light; }
            set
            {
                light = value;
            }
        }
        private Vector3D light;

        public Color FrontColor
        {
            get;
            set;
        }

        public Color BackColor
        {
            get;
            set;
        }
#if false
        public void Triangle(Vector3D v0, Vector3D n0, Color c0, Vector3D v1, Vector3D n1, Color c1, Vector3D v2, Vector3D n2, Color c2)
        {
            Vector3D s0 = modelViewProjectionMatrix.Transform(v0);
            Vector3D s1 = modelViewProjectionMatrix.Transform(v1);
            Vector3D s2 = modelViewProjectionMatrix.Transform(v2);

            // could cull by screen space bounds

            bool backFacing = (s1.X - s0.X) * (s2.Y - s0.Y) - (s2.X - s0.X) * (s1.Y - s0.Y) <= 0.0;

            if (BackFaceCull && backFacing)
            {
                return;
            }

            Color color = backFacing ? BackColor : FrontColor;

            Vector3D n = Vector3D.CrossProduct(v1 - v0, v2 - v0);
            n.Normalize();

            c0 = Illuminate(v0, n, backFacing ? BackColor : c0);
            c1 = Illuminate(v1, n, backFacing ? BackColor : c1);
            c2 = Illuminate(v1, n, backFacing ? BackColor : c2);

            Rasterize(s0, c0, s1, c1, s2, c2);
        }
        public void Triangle(Vector3D v0, Vector3D n0, Vector3D v1, Vector3D n1, Vector3D v2, Vector3D n2)
        {
            Vector3D s0 = modelViewProjectionMatrix.Transform(v0);
            Vector3D s1 = modelViewProjectionMatrix.Transform(v1);
            Vector3D s2 = modelViewProjectionMatrix.Transform(v2);

            // could cull by screen space bounds

            bool backFacing=(s1.X - s0.X) * (s2.Y - s0.Y) - (s2.X - s0.X) * (s1.Y - s0.Y) <= 0.0;

            if (BackFaceCull && backFacing)
            {
                return;
            }

            Color color = backFacing ? BackColor : FrontColor;

            Vector3D n = Vector3D.CrossProduct(v1 - v0, v2 - v0);
            n.Normalize();

            Color c0 = Illuminate(v0, n0, color);
            Color c1 = Illuminate(v1, n1, color);
            Color c2 = Illuminate(v1, n2, color);

            Rasterize(s0, c0, s1, c1, s2, c2);
        }
        private Color Illuminate(Vector3D v, Vector3D n, Color color)
        {
            v = ModelViewMatrix.Transform(v) - Light;
            n = new Vector3D(normalMatrix.M11 * n.X + normalMatrix.M21 * n.Y + normalMatrix.M31 * n.Z + normalMatrix.OffsetX * 1.0, normalMatrix.M12 * n.X + normalMatrix.M22 * n.Y + normalMatrix.M32 * n.Z + normalMatrix.OffsetY * 1.0, normalMatrix.M13 * n.X + normalMatrix.M23 * n.Y + normalMatrix.M33 * n.Z + normalMatrix.OffsetZ * 1.0);

            double d = Vector3D.DotProduct(n, v) / v.Length;

            d =  Math.Min(d < 0? -d: d, 1.0);

            return Color.FromArgb(0xff, (byte)(color.R * d), (byte)(color.G * d), (byte)(color.B * d));
        }
#endif
        public void ClearBuffers()
        {
#if SILVERLIGHT
            System.Array.Clear(WriteableBitmap.Pixels, 0, PixelWidth * PixelHeight);
#else
            throw new NotImplementedException();
#endif
            if (DepthBuffer == null || DepthBuffer.Length != PixelWidth * PixelHeight)
            {
                DepthBuffer = new double[PixelWidth * PixelHeight];
            }
            else
            {
                System.Array.Clear(DepthBuffer, 0, DepthBuffer.Length);
            }
        }

        public void SwapBuffers()
        {
#if SILVERLIGHT
            WriteableBitmap.Invalidate();
#else
            WriteableBitmap.AddDirtyRect(new Int32Rect(0, 0, WriteableBitmap.PixelWidth, WriteableBitmap.PixelHeight));
#endif
        }
    }
}
