﻿namespace BeautySalon.Components
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    /// <summary>
    /// Defines the <see cref="DrawHelper" />
    /// </summary>
    internal static class DrawHelper
    {
        /// <summary>
        /// The CreateRoundRect
        /// </summary>
        /// <param name="x">The x<see cref="float"/></param>
        /// <param name="y">The y<see cref="float"/></param>
        /// <param name="width">The width<see cref="float"/></param>
        /// <param name="height">The height<see cref="float"/></param>
        /// <param name="radius">The radius<see cref="float"/></param>
        /// <returns>The <see cref="GraphicsPath"/></returns>
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();

            if (radius == 0)
            {
                gp.AddRectangle(new RectangleF(x, y, width, height));
                return gp;
            }

            float radiusLim = radius * 2;
            float halfWidth = width / 2;
            float halfHeight = height / 2;
            if (width >= height)
            {
                if (halfHeight < radiusLim || radius < 0)
                {
                    radiusLim = height;
                }
            }
            else
            {
                if (halfWidth < radiusLim || radius < 0)
                {
                    radiusLim = width;
                }
            }


            gp.AddArc(x, y, radiusLim, radiusLim, 180, 90);
            gp.AddArc(x + width - radiusLim, y, radiusLim, radiusLim, 270, 90);
            gp.AddArc(x + width - radiusLim, y + height - radiusLim, radiusLim, radiusLim, 0, 90);
            gp.AddArc(x, y + height - radiusLim, radiusLim, radiusLim, 90, 90);
            gp.CloseFigure();
            return gp;
        }

        /// <summary>
        /// The CreateRoundRect
        /// </summary>
        /// <param name="rect">The rect<see cref="Rectangle"/></param>
        /// <param name="radius">The radius<see cref="float"/></param>
        /// <returns>The <see cref="GraphicsPath"/></returns>
        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        /// <summary>
        /// The CreateRoundRect
        /// </summary>
        /// <param name="rect">The rect<see cref="RectangleF"/></param>
        /// <param name="radius">The radius<see cref="float"/></param>
        /// <returns>The <see cref="GraphicsPath"/></returns>
        public static GraphicsPath CreateRoundRect(RectangleF rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        /// <summary>
        /// The BlendColor
        /// </summary>
        /// <param name="backgroundColor">The backgroundColor<see cref="Color"/></param>
        /// <param name="frontColor">The frontColor<see cref="Color"/></param>
        /// <param name="blend">The blend<see cref="double"/></param>
        /// <returns>The <see cref="Color"/></returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// The BlendColor
        /// </summary>
        /// <param name="backgroundColor">The backgroundColor<see cref="Color"/></param>
        /// <param name="frontColor">The frontColor<see cref="Color"/></param>
        /// <returns>The <see cref="Color"/></returns>
        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }

        public static void DrawSquareShadow(Graphics g, Rectangle bounds, float radius)
        {
            DrawSquareShadow(g, bounds.X, bounds.Y, bounds.Width, bounds.Height, radius);
        }

        public static void DrawSquareShadow(Graphics g, RectangleF bounds, float radius)
        {
            DrawSquareShadow(g, bounds.X, bounds.Y, bounds.Width, bounds.Height, radius);
        }

        public static void DrawSquareShadow(Graphics g, float x, float y, float width, float height, float radius)
        {
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(12, 0, 0, 0)))
            {
                GraphicsPath path;
                path = DrawHelper.CreateRoundRect(new RectangleF(x - 3.5f, y - 1.5f, width + 6, height + 6), radius);
                g.FillPath(shadowBrush, path);
                path = DrawHelper.CreateRoundRect(new RectangleF(x - 2.5f, y - 1.5f, width + 4, height + 4), radius);
                g.FillPath(shadowBrush, path);
                path = DrawHelper.CreateRoundRect(new RectangleF(x - 1.5f, y - 0.5f, width + 2, height + 2), radius);
                g.FillPath(shadowBrush, path);
                path = DrawHelper.CreateRoundRect(new RectangleF(x - 0.5f, y + 1.5f, width + 0, height + 0), radius);
                g.FillPath(shadowBrush, path);
                path = DrawHelper.CreateRoundRect(new RectangleF(x - 0.5f, y + 2.5f, width + 0, height + 0), radius);
                g.FillPath(shadowBrush, path);
                path.Dispose();
            }
        }

        public static void DrawRoundShadow(Graphics g, Rectangle bounds)
        {
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(12, 0, 0, 0)))
            {
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 2, bounds.Y - 1, bounds.Width + 4, bounds.Height + 6));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 1, bounds.Y - 1, bounds.Width + 2, bounds.Height + 4));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y - 0, bounds.Width + 0, bounds.Height + 2));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 2, bounds.Width + 0, bounds.Height + 0));
                g.FillEllipse(shadowBrush, new Rectangle(bounds.X - 0, bounds.Y + 1, bounds.Width + 0, bounds.Height + 0));
            }
        }
    }
}
