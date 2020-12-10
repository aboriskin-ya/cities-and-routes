namespace Service.DTO
{
    public class SettingsUpdateDTO
    {
        public string FoundPathColor { get; set; }
        public double FoundPathSize { get; set; }
        public bool DisplayCitiesNames { get; set; }
        public bool DisplayingImage { get; set; }
        public bool DisplayingGraph { get; set; }
        public double VertexSize { get; set; }
        public string VertexColor { get; set; }
        public double EdgeSize { get; set; }
        public string EdgeColor { get; set; }
    }
}