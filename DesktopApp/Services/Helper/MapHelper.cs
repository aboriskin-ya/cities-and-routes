using DesktopApp.Resources;
using DesktopApp.ViewModels;
using System;
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
            return Vector.Add(RelativeVector, new Point());
        }
        public static Offset GetOffset(Offset Offset, double ScaleValue, double Height, double Width, Point TransformPosition, ZoomEnum ZoomSelector)
        {
            if (ScaleValue > 16) return Offset;
            if (ScaleValue == 1) return new Offset(0, 0, 0, 0);
            switch ((int)ZoomSelector)
            {
                case 0:
                    Offset.Top += Height * TransformPosition.Y / 2;
                    Offset.Bottom += Height * (1 - TransformPosition.Y) / 2;
                    Offset.Right += Width * (1 - TransformPosition.X) / 2;
                    Offset.Left += Width * TransformPosition.X / 2;
                    return Offset;
                case 1:
                    Offset.Top -= Height * TransformPosition.Y / 2;
                    Offset.Bottom -= Height * (1 - TransformPosition.Y) / 2;
                    Offset.Right -= Width * (1 - TransformPosition.X) / 2;
                    Offset.Left -= Width * TransformPosition.X / 2;
                    return Offset;
            }
            return Offset;
        }

        public static Point GetOffsetValue(double PPW, double PPH, Offset offset, Point CurrentOffsetValue)
        {

            var CurrentRelativeOffsetX = CurrentOffsetValue.X * PPW;
            var CurrentRelativeOffsetY = CurrentOffsetValue.Y * PPH;
            if (CurrentRelativeOffsetX > 0)
            {
                if (Math.Max(Math.Abs(CurrentRelativeOffsetX), offset.Left) > offset.Left)
                    CurrentRelativeOffsetX = offset.Left;
            }
            if (CurrentRelativeOffsetY > 0)
            {
                if (Math.Max(Math.Abs(CurrentRelativeOffsetY), offset.Top) > offset.Top)
                    CurrentRelativeOffsetY = offset.Top;
            }
            if (CurrentRelativeOffsetX < 0)
            {
                if (Math.Max(Math.Abs(CurrentRelativeOffsetX), offset.Right) > offset.Right)
                    CurrentRelativeOffsetX = -offset.Right;
            }
            if (CurrentRelativeOffsetY < 0)
            {
                if (Math.Max(Math.Abs(CurrentRelativeOffsetY), offset.Bottom) > offset.Bottom)
                    CurrentRelativeOffsetY = -offset.Bottom;
            }
            return new Point(CurrentRelativeOffsetX / PPW, CurrentRelativeOffsetY / PPH);
        }
        public static Offset GetOffsetAfterDrag(double PPW, double PPH, Offset CurrentOffset, Point CurrentOffsetValue)
        {

            var CurrentRelativeOffsetX = CurrentOffsetValue.X * PPW;
            var CurrentRelativeOffsetY = CurrentOffsetValue.Y * PPH;
            if (CurrentRelativeOffsetX > 0)
            {
                CurrentOffset.Right += CurrentRelativeOffsetX;
                CurrentOffset.Left -= CurrentRelativeOffsetX;
            }
            if (CurrentRelativeOffsetY > 0)
            {
                CurrentOffset.Top -= CurrentRelativeOffsetY;
                CurrentOffset.Bottom += CurrentRelativeOffsetY;
            }
            if (CurrentRelativeOffsetX < 0)
            {
                CurrentOffset.Right += CurrentRelativeOffsetX;
                CurrentOffset.Left -= CurrentRelativeOffsetX;
            }
            if (CurrentRelativeOffsetY < 0)
            {
                CurrentOffset.Top -= CurrentRelativeOffsetY;
                CurrentOffset.Bottom += CurrentRelativeOffsetY;
            }
            return CurrentOffset;
        }
    }
}
