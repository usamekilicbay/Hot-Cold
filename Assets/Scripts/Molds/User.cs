#region General

public class UserGeneral
{
    // String variables
    public string Username;
    public string SignUpDate;
    public string LastSeen;
    public string Country;
    public string Language;
    
    // Bool Variables
    public bool SignInStatus;
    public bool Intermateable;

    public UserGeneral
        (
         // String
         string _username,
         string _signUpDate,
         string _lastSeen,
         string _country,
         string _language,
         
         // Bool
         bool _signInStatus,
         bool _intermateable
        )
    {
        // String
        Username = _username;
        SignUpDate = _signUpDate;
        LastSeen = _lastSeen;
        Country = _country;
        Language = _language;
        
        // Bool
        SignInStatus = _signInStatus;
        Intermateable = _intermateable;
    }
}

#endregion

#region Progression

public class UserProgression
{
    // Int variables
    public int Level;
    public int Cup;
    public int Rank;
    public int TotalPlayTime;
    public int TotalMatches;
    public int CompletedMatches;
    public int AbandonedMatches;
    public int Wins;
    public int Losses;
    public int WinningStreak;
    public int HighScore;
    public int SignInCount;

    public UserProgression
        (
        // Int
        int _level,
        int _cup,
        int _rank,
        int _totalPlayTime,
        int _totalMatches,
        int _completedMatches,
        int _abandonedMatches,
        int _wins,
        int _losses,
        int _winningStreak,
        int _highScore,
        int _signInCount
        )
    {
        // Int
        Level = _level;
        Cup = _cup;
        Rank = _rank;
        TotalPlayTime = _totalPlayTime;
        TotalMatches = _totalMatches;
        CompletedMatches = _completedMatches;
        AbandonedMatches = _abandonedMatches;
        Wins = _wins;
        Losses = _losses;
        WinningStreak = _winningStreak;
        HighScore = _highScore;
        SignInCount = _signInCount;
    }
}

#endregion

#region Consumable

public class UserConsumable
{
    public int Energy;
    public int Papcoin;
    public int Gem;
    public int Joker;

    public UserConsumable
        (
        int _energy,
        int papcoin,
        int _gem,
        int _joker
        )
    {
        Energy = _energy;
        Papcoin = papcoin;
        Gem = _gem;
        Joker = _joker;
    }
}

#endregion

#region Old User Class
/*class User
{
    // String variables
    public string username;
    public string signUpDate;
    public string lastSignInDate;
    public string country;
    public string language;

    // Int variables
    public int level;
    public int cup;
    public int highScore;
    public int signInCount;
    public int totalPlayTime;
    public int totalMatchCount;
    public int completedGameCount;
    public int abandonedGameCount;
    public int winCount;
    public int loseCount;
    public int energy;
    public int gold;
    public int diamond;
    public int joker;

    // Bool variables
    public bool signInStatus;


    /*public User
        (
        // String
        string username = "",
        string signUpDate = "",
        string lastSignInDate = "",
        string country = "",
        string language = "",
        // Int
        int level = 0,
        int cup = 0,
        int highScore = 0,
        int signInCount = 0,
        int totalPlayTime = 0,
        int totalMatchCount = 0,
        int completedGameCount = 0,
        int abandonedGameCount = 0,
        int winCount = 0,
        int loseCount = 0,
        int energy = 0,
        int gold = 0,
        int diamond = 0,
        int joker = 0,
        // Bool
        bool signInStatus = false
        )
    {
        // String
        this.username = username;
        this.signUpDate = signUpDate;
        this.lastSignInDate = lastSignInDate;
        this.country = country;
        this.language = language;

        // Int
        this.level = level;
        this.cup = cup;
        this.highScore = highScore;
        this.signInCount = signInCount;
        this.totalPlayTime = totalPlayTime;
        this.totalMatchCount = totalMatchCount;
        this.completedGameCount = completedGameCount;
        this.abandonedGameCount = abandonedGameCount;
        this.winCount = winCount;
        this.loseCount = loseCount;
        this.energy = energy;
        this.gold = gold;
        this.diamond = diamond;
        this.joker = joker;

        // Bool
        this.signInStatus = signInStatus;
    }*/

/* public User
	 (
	 // String
	 string username = "",
	 string signUpDate = "",
	 string lastSignInDate = "",
	 string country = "",
	 string language = "",

	 bool signInStatus = false
	 )
 {
	 // String
	 this.username = username;
	 this.signUpDate = signUpDate;
	 this.lastSignInDate = lastSignInDate;
	 this.country = country;
	 this.language = language;

	 // Bool
	 this.signInStatus = signInStatus;
 }*/

/* public User
	 (
	 // Int
	 int level = 0,
	 int cup = 0,
	 int highScore = 0,
	 int signInCount = 0,
	 int totalPlayTime = 0,
	 int totalMatchCount = 0,
	 int completedGameCount = 0,
	 int abandonedGameCount = 0,
	 int winCount = 0,
	 int loseCount = 0,
	 int energy = 0,
	 int gold = 0,
	 int diamond = 0,
	 int joker = 0
	 ) 
 {
	 // Int
	 this.level = level;
	 this.cup = cup;
	 this.highScore = highScore;
	 this.signInCount = signInCount;
	 this.totalPlayTime = totalPlayTime;
	 this.totalMatchCount = totalMatchCount;
	 this.completedGameCount = completedGameCount;
	 this.abandonedGameCount = abandonedGameCount;
	 this.winCount = winCount;
	 this.loseCount = loseCount;
	 this.energy = energy;
	 this.gold = gold;
	 this.diamond = diamond;
	 this.joker = joker;
 }
}*/

#endregion