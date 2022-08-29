namespace LogicLayer.UtilityMethods
{
    /// <summary>
    /// Helpful methods for back-end utilities.
    /// </summary>
    public static class BackendUtilityMethods
    {
        /// <summary>
        /// Calculates a random alphanumeric string.
        /// </summary>
        /// <param name="length">Desired length of returned string</param>
        /// <returns>A random alphanumeric string.</returns>
        public static string GetRandomAlphanumericString(int length)
        {
            Random random = new Random(); // Randomness generator
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Available characters for returned string

            return new string(Enumerable.Repeat(chars, length) // Create random string
                .Select(s => s[random.Next(s.Length)]).ToArray());

        }
        
    }
}
