namespace LogicLayer.UtilityMethods
{
    /// <summary>
    /// Helpful methods for front-end display.
    /// </summary>
    public static class FrontendUtilityMethods
    {
        /// <summary>
        /// Fetch random background pic for default layout, from site resources.
        /// </summary>
        /// <returns>The picture relative path</returns>
        public static string FetchDefaultBackground()
        {
            // Pick file at random based on hour
            var hour = DateTime.Now.Hour;
            var allFiles = Directory.GetFiles("Resources/Pictures/DefaultBackground");
            var randomFile = allFiles[hour % allFiles.Length];

            // Get correct filename, return to frontend
            randomFile = randomFile.Replace('\\', '/');
            return randomFile.Substring(randomFile.IndexOf('/'));

        }
    }
}
