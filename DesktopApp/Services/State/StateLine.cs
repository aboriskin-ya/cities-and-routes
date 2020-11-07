namespace DesktopApp.Services.State
{
    public enum StateLineStatus
    {
        Empty,
        AddCity,
        SetCity,
        CreateCity,
        AddRouteAndDistance
    }  
    public static class StateLine
    {
        public static string Show(StateLineStatus status)
        {
            switch (status)
            {
                case StateLineStatus.Empty:
                    return "";
                case StateLineStatus.AddCity:
                    return "Please click on the \"Add a city\" to create a new city";
                case StateLineStatus.SetCity:
                    return "Please click on the map to create a new city";
                case StateLineStatus.CreateCity:
                    return "Please enter the name of the new city";
                case StateLineStatus.AddRouteAndDistance:
                    return "Please set the route and distance between cities";
            }
            return "";
        }
    }
}
