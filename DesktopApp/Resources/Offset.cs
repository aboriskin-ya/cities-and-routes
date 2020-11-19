namespace DesktopApp.UserControls
{
    public struct Offset
    {
        public Offset(double Top, double Right, double Bottom, double Left)
        {
            this.Top = Top;
            this.Bottom = Bottom;
            this.Left = Left;
            this.Right = Right;
        }
        public double Top { get; set; }
        public double Bottom { get; set; }
        public double Right { get; set; }
        public double Left { get; set; }
    }
}
