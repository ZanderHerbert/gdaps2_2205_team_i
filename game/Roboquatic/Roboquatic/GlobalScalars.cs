using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Roboquatic
{
    public static class GlobalScalars
    {
        public static float x { get; set; }
        public static float y { get; set; }

        public static Rectangle scaleRect(Rectangle rect)
        {
            Rectangle scaled = rect;
            scaled.X = (int)(rect.X * x);
            scaled.Y = (int)(rect.Y * y);
            scaled.Width = (int)(rect.Width * x);
            scaled.Height = (int)(rect.Height * y);
            return scaled;
        }

        public static Rectangle scaleSize(Rectangle rect)
        {
            Rectangle scaled = rect;
            scaled.Width = (int)(rect.Width * x);
            scaled.Height = (int)(rect.Height * y);
            return scaled;
        }
    }
}
