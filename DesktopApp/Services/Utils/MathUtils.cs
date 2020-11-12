namespace DesktopApp.Services.Utils
{
    public static class MathUtils
    {
        public static uint Factorial(uint num)
        {
            uint factorial = 1;
            for (uint i = 1; i <= num; i++)
            {
                factorial *= i;
            }
            return factorial;
        }

        public static uint BinomialCoefficient(uint n, uint k)
        {
            return Factorial(n) / (Factorial(k) * Factorial(n - k));
        }
    }
}
