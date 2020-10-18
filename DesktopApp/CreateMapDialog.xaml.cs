using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopApp
{
    public enum AllowExtensions { jpg, png };
    public partial class CreateMapDialog : Window
    {
        public CreateMapDialog()
        {
            InitializeComponent();
        }
        //private void MapImage_Drop(object sender, DragEventArgs e)
        //{
        //    if(e.Data.GetDataPresent(DataFormats.FileDrop))
        //    {
        //        string[] dropPath = e.Data.GetData(DataFormats.FileDrop, true) as string[];
        //        if( Enum.IsDefined(typeof(AllowExtensions), Path.GetExtension(dropPath[0]).Trim('.')) )
        //        {
        //            //mapName.Text = Path.GetFileNameWithoutExtension(dropPath[0]);
        //            newMap.Source = new BitmapImage(new Uri(Path.GetFullPath(dropPath[0])));
        //            newMap.Width = scroll.Width;
        //            newMap.Stretch = Stretch.None;
        //            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        //        }

        //    }
        //}
    }
    
}
