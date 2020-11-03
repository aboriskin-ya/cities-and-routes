namespace DesktopApp.Services.State
{
    public enum StateLineStatus
    {
        AddMap = 1,
        SetMap,
        AddBondAndDistance
    }  
    public static class StateLine
    {
        public static string Show(StateLineStatus status)
        {
            switch (status)
            {
                case StateLineStatus.AddMap:
                    return "Please click on the \"Add a city\" to create a new city";
                case StateLineStatus.SetMap:
                    return "Please click on the map to create a new city";
                case StateLineStatus.AddBondAndDistance:
                    return "Please set the bond and distance between cities";
            }
            return "";
        }
    }
}
