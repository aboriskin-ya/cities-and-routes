using Service.DefaultModels;
using System;
namespace Service.DTO
{
    public class SettingsGetDTO
    {
        public Guid Id { get; set; }
        public string FoundPathColor { get; set; } = SettingsDefault.FoundPathColor;
        public double FoundPathSize { get; set; } = SettingsDefault.FoundPathSize;
        public bool DisplayCitiesNames { get; set; } = SettingsDefault.DisplayCitiesNames;
        public bool DisplayingImage { get; set; } = SettingsDefault.DisplayingImage;
        public bool DisplayingGraph { get; set; } = SettingsDefault.DisplayingGraph;
        public double VertexSize { get; set; } = SettingsDefault.VertexSize;
        public string VertexColor { get; set; } = SettingsDefault.VertexColor;
        public double EdgeSize { get; set; } = SettingsDefault.EdgeSize;
        public string EdgeColor { get; set; } = SettingsDefault.EdgeColor;
        public Guid MapId { get; set; }
    }
}
