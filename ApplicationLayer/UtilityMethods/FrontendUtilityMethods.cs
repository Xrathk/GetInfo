namespace ApplicationLayer.UtilityMethods
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

        /// <summary>
        /// Limit news card text based on type of text
        /// </summary>
        /// <param name="type">Type of text (1 for title, 2 for description)</param>
        /// <param name="text">Text</param>
        /// <returns>Limited text (less than a number of characters)</returns>
        public static string LimitText(int type, string text)
        {
            if (type == 1)
            {
                string newText = "";
                if (text.Length > 97)
                {
                    newText = text.Substring(0, 98) + "...";
                    return newText;
                }
                else
                {
                    return text;
                }

            }
            else // type = 2
            {
                string newText = "";
                if (text.Length > 240)
                {
                    newText = text.Substring(0, 241) + "...";
                    return newText;
                }
                else
                {
                    return text;
                }
            }
        }
    }
}
