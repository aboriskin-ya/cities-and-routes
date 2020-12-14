namespace DesktopApp.Services.Console
{
    static class ConsoleOutput
    {
        public static string SuccessfulResult(string processDuration, string finalDistance)
        {
            return $"Process' duration: {processDuration}\nCalculated distance: {finalDistance}km\nPath: ";
        }
        public static string SuccessfulResult(string nameAlghorithm, string processDuration, string finalDistance)
        {
            return $"Algorithm: {nameAlghorithm}\n" + SuccessfulResult(processDuration, finalDistance);
        }
        public static string FailedResult()
        {
            return "A path is not found.";
        }
        public static string SolveButtonPressed()
        {
            return "\"Solve\" was pressed\n";
        }
        public static string CityName(string name)
        {
            return $"User selected a city: {name}\n";
        }
        public static string ResultBoundary()
        {
            return "--------RESULT--------\n";
        }
        public static string Boundary()
        {
            return "\n------------------------\n";
        }
        public static string Empty()
        {
            return "";
        }
        public static string Arrow()
        {
            return "->";
        }
    }
}
