namespace Area92.Helpers
{
    public static class EndYearExtensions
    {
        public static bool IsEndedSeries(this int releaseYear, int endYear)
        {
            var currentYear = DateTime.Now.Year;

            if (endYear < 0)
            {
                return false;
            }
            if (currentYear <= endYear)
            {
                return false;
            }
            return true;
        }
    }
}
