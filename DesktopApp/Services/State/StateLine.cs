namespace DesktopApp.Services.State
{
    public enum StateLineStatus
    {
        Empty,
        SetCity,
        CreateCity,
        UpdateCity,
        SelectFirstCity,
        SelectSecondCity,
        CreateRoute,
        SaveChanges,
        ResolverSelectCities,
        ResolverDone
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
                    return "Please enter the name of a new city";
                case StateLineStatus.UpdateCity:
                    return "Please update the name of the city";
                case StateLineStatus.SelectFirstCity:
                    return "Please select the first city";
                case StateLineStatus.SelectSecondCity:
                    return "Please select the second city";
                case StateLineStatus.CreateRoute:
                    return "Please set a distance between the cities";
                case StateLineStatus.SaveChanges:
                    return "All changes were saved";
                case StateLineStatus.ResolverSelectCities: 
                    return "Select cities for resolving a goal";
                case StateLineStatus.ResolverDone: 
                    return "The goal was resolved";
            }
            return "";
        }
    }
}