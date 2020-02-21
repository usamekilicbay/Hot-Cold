using System.Collections.Generic;

public static class CurrentUserProfileKeeper
{
    public static Dictionary<string, object> currentUserProfileDictionary = new Dictionary<string, object>();

    public static Dictionary<string, object> UserProfileSetter() 
    {
        return currentUserProfileDictionary;
    }

   
        // General String Variables
        public static string Username;
        public static string Country;
        public static string Language;
        public static string SignUpDate;
        public static string LastSeen;
       
         // General Bool Variables
        public static bool SignInStatus;
        public static bool Intermateable;
    
        // Progression Int Variables
        public static int Level;
        public static int Cup;
        public static int Rank;
        public static int TotalPlayTime;
        public static int TotalMatches;
        public static int CompletedMatches;
        public static int AbandonedMatches;
        public static int Wins;
        public static int Losses;
        public static int WinningStreak;
        public static int HighScore;
        public static int SignInCount;


        // Consumable Int Variables
        public static int Energy;
        public static int Gold;
        public static int Diamond;
        public static int Joker;

}
