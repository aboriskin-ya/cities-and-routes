namespace Service.DTO
{
    public class SettingsUpdateDTO
    {
        public bool DisplayingImage { get; set; }
        public bool DisplayingGraph { get; set; }
        public double VertexSize { get; set; }
        public string VertexColor { get; set; }
        public double EdgeSize { get; set; }
        public string EdgeColor { get; set; }
    }
}