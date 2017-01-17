using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rage_of_Stickman
{
    public static class RectangleHelper
    {
        public static float GetHorizontalIntersectionDepth(this Rectangle aRectangle, Rectangle bRectangle)
        {
            float centerA = aRectangle.Left + aRectangle.Width / 2;
            float centerB = bRectangle.Left + bRectangle.Width / 2;

            float distX = centerA - centerB;
            float minDistanceX = (aRectangle.Width + bRectangle.Width) / 2;

            if (Math.Abs(distX) >= minDistanceX)
                return 0f;
            return distX > 0 ? minDistanceX - distX : -minDistanceX - distX;
        }

        public static float GetVerticalIntersectionDepth(this Rectangle aRectangle, Rectangle bRectangle)
        {
            float centerA = aRectangle.Top + aRectangle.Height / 2;
            float centerB = bRectangle.Top + bRectangle.Height / 2;

            float distY = centerA - centerB;
            float minDistanceY = (aRectangle.Height + bRectangle.Height) / 2;

            if (Math.Abs(distY) >= minDistanceY)
                return 0f;
            return distY > 0 ? minDistanceY - distY : -minDistanceY - distY;
        }

    }  
}
