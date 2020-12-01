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
        FindShortestPath,
        TravelSalesman_PushButton,
        TravelSalesman_SelectCities,
        TravelSalesman_ResolvingGoal,
        TravelSalesman_Done
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
                case StateLineStatus.FindShortestPath:
                    return "Please select two cities to calculate the shortest path";
            }
            return "";
        }
        public static string GetStatus(StateLineStatus states)
        {
            switch (states)
            {
                case StateLineStatus.TravelSalesman_PushButton: return "Press button 'Select cities' to begin procedure choicing cities";
                case StateLineStatus.TravelSalesman_SelectCities: return "Select cities for resolving goal";
                case StateLineStatus.TravelSalesman_ResolvingGoal: return "Resolving travelsalesman for selected routes";
                case StateLineStatus.TravelSalesman_Done: return "Goal was resolved. Check console above here";
            }
            return "";
        }
    }
}