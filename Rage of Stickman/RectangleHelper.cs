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
            Console.WriteLine("centerA :"+centerA);
            Console.WriteLine("centerB :"+centerB);


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
            Console.WriteLine("centerA :" + centerA);
            Console.WriteLine("centerB :" + centerB);
            float distY = centerA - centerB;
            float minDistanceY = (aRectangle.Height + bRectangle.Height) / 2;
            if (Math.Abs(distY) >= minDistanceY)
                return 0f;
            return distY > 0 ? minDistanceY - distY : -minDistanceY - distY;
        }

        public static Vector2 GetIntersectionDepth(this Rectangle rectA, Rectangle rectB)
        {
            // Calculate half sizes.
            float halfWidthA = rectA.Width / 2.0f;
            float halfHeightA = rectA.Height / 2.0f;
            float halfWidthB = rectB.Width / 2.0f;
            float halfHeightB = rectB.Height / 2.0f;

            // Calculate centers.
            Vector2 centerA = new Vector2(rectA.Left + halfWidthA, rectA.Top + halfHeightA);
            Vector2 centerB = new Vector2(rectB.Left + halfWidthB, rectB.Top + halfHeightB);

            // Calculate current and minimum-non-intersecting distances between centers.
            float distanceX = centerA.X - centerB.X;
            float distanceY = centerA.Y - centerB.Y;
            float minDistanceX = halfWidthA + halfWidthB;
            float minDistanceY = halfHeightA + halfHeightB;

            // If we are not intersecting at all, return (0, 0).
            if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
                return Vector2.Zero;

            // Calculate and return intersection depths.
            float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
            float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
            return new Vector2(depthX, depthY);
        }

    }  
}
