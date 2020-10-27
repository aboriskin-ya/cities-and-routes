using DesktopApp.UserControllers;
using DesktopApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DesktopApp.Services.Helper
{
    internal class MapHelper
    {
        public static Point GetRelativeCurrentPosit1ion(Point OffsetValue, double Height, double Width, double ScaleValue, out Vector RelativeVector)
        {
            double CorrectiveKoef = 0;
            switch (ScaleValue)
            {
                case 2: CorrectiveKoef = 2; break;
                case 4: CorrectiveKoef = 1.33; break;
                case 8: CorrectiveKoef = 1.146; break;
                case 16: CorrectiveKoef = 1.07; break;
            }
            RelativeVector.X -= OffsetValue.X / Width / ScaleValue * CorrectiveKoef;
            RelativeVector.Y -= OffsetValue.Y / Height / ScaleValue * CorrectiveKoef;
            if (RelativeVector.X > 1.0) RelativeVector.X = 1.0;
            if (RelativeVector.Y > 1.0) RelativeVector.Y = 1.0;
            if (RelativeVector.X < 0) RelativeVector.X = 0;
            if (RelativeVector.Y < 0) RelativeVector.Y = 0;
            return Vector.Add(RelativeVector, new Point());
        }
        public static Offset GetOffset(Offset Offset, double ScaleValue, ZoomEnum ZoomSelector)
        {
            if (ScaleValue == 1) return new Offset(0, 0, 0, 0);
            if (ScaleValue > 16) return Offset;
            switch ((int)ZoomSelector)
            {
                case 0:
                    Offset.Top += Offset.Top / 2;
                    Offset.Bottom += Offset.Bottom / 2;
                    Offset.Right += Offset.Right / 2;
                    Offset.Left += Offset.Left / 2;
                    return Offset;
                case 1:
                    Offset.Top -= Offset.Top / 2;
                    Offset.Bottom -= Offset.Bottom / 2;
                    Offset.Right -= Offset.Right / 2;
                    Offset.Left -= Offset.Left / 2;
                    return Offset;
            }
            return Offset;
        }

        public static Point GetOffsetValue(double PPW, double PPH, ref Offset offset, Point CurrentOffsetValue)
        {

            var CurrentRelativeOffsetX = CurrentOffsetValue.X * PPW;
            var CurrentRelativeOffsetY = CurrentOffsetValue.Y * PPH;
            if (CurrentRelativeOffsetX > 0)
            {
                offset.Left = offset.Left - CurrentRelativeOffsetX;
                CurrentOffsetValue.X = offset.Left < 0 ? 0 : CurrentOffsetValue.X = offset.Left - CurrentRelativeOffsetX;
            }
            if(CurrentRelativeOffsetY > 0)
            {
                offset.Top = offset.Top - CurrentRelativeOffsetY;
                CurrentOffsetValue.Y = offset.Top < 0 ? 0 : CurrentOffsetValue.Y = offset.Top - CurrentRelativeOffsetY;
            }
            if (CurrentRelativeOffsetX < 0)
            {
                offset.Right = offset.Right + CurrentRelativeOffsetX;
                CurrentOffsetValue.X = offset.Right < 0 ? 0 : CurrentOffsetValue.X = offset.Right + CurrentRelativeOffsetX;
            }
            if (CurrentRelativeOffsetY < 0)
            {
                offset.Bottom = offset.Bottom + CurrentRelativeOffsetY;
                CurrentOffsetValue.Y = offset.Bottom < 0 ? 0 : CurrentOffsetValue.Y = offset.Bottom + CurrentRelativeOffsetY;
            }
            return new Point(CurrentRelativeOffsetX/PPW, CurrentRelativeOffsetY/PPH);
        }

    }
}
