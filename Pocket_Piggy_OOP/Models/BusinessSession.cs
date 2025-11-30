namespace PocketPiggy.Models
{
    /// <summary>
    /// Static class to store the current logged-in business user's session information
    /// </summary>
    public static class BusinessSession
    {
        public static int CurrentBusinessId { get; set; }
        public static string CurrentBusinessName { get; set; }
        
        public static void Clear()
        {
            CurrentBusinessId = 0;
            CurrentBusinessName = null;
        }
        
        public static bool IsLoggedIn => CurrentBusinessId > 0;
    }
}

