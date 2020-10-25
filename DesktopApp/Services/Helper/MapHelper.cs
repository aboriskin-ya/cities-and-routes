using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopApp.Services.Helper
{
    internal class MapHelper
    {

        public static Point GetRelativeCurrentPosition(Point OffsetValue,double Height, double Width, double ScaleValue,out Vector RelativeVector)
        {
            double CorrectiveKoef = 0;
            switch (ScaleValue)
            {
                case 2:CorrectiveKoef = 2;break;
                case 4: CorrectiveKoef = 1.33; break;
                case 8: CorrectiveKoef = 1.146; break;
                case 16:CorrectiveKoef = 1.07;break;
            }
            RelativeVector.X -= OffsetValue.X / Width / ScaleValue * CorrectiveKoef;
            RelativeVector.Y -= OffsetValue.Y / Height / ScaleValue * CorrectiveKoef;
            if (RelativeVector.X > 1.0) RelativeVector.X = 1.0;
            if (RelativeVector.Y > 1.0) RelativeVector.Y = 1.0;
            if (RelativeVector.X < 0) RelativeVector.X = 0;
            if (RelativeVector.Y < 0) RelativeVector.Y = 0;
            return  Vector.Add(RelativeVector, new Point());
        }
    }
}
