namespace DesktopApp.Services.State
{
    public enum StateLineStatus
    {
        Empty,
        SetCity,
        CreateCity,
        SelectFirstCity,
        SelectSecondCity,
        CreateRoute,
        SaveChanges
    }  
    public static class StateLine
    {
        public static string Show(StateLineStatus status)
        {
            switch (status)
            {
                case StateLineStatus.SetCity:
                    return "Please click on the map to create a new city";
                case StateLineStatus.CreateCity:
                    return "Please enter the name of the new city";
                case StateLineStatus.SelectFirstCity:
                    return "Please select the first city";
                case StateLineStatus.SelectSecondCity:
                    return "Please select the second city";
                case StateLineStatus.CreateRoute:
                    return "Please set a distance between the cities";
                case StateLineStatus.SaveChanges:
                    return "All changes were saved";
            }
            return "";
        }
    }
}
