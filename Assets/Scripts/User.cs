﻿#region General

public class UserGeneral
{
    // String variables
    public string Username;
    public string SignUpDate;
    public string LastSignInDate;
    public string Country;
    public string Language;

    // Bool variables
    public bool SignInStatus;

    public UserGeneral
        (
         // String
         string _username,
         string _signUpDate,
         string _lastSignInDate,
         string _country,
         string _language,

         // Bool
         bool _signInStatus
        )
    {
        // String
        Username = _username;
        SignUpDate = _signUpDate;
        LastSignInDate = _lastSignInDate;
        Country = _country;
        Language = _language;

        // Bool
        SignInStatus = _signInStatus;
    }
}

#endregion

#region Progression

public class UserProgression
{
    // Int variables
    public int Level;
    public int Cup;
    public int HighScore;
    public int SignInCount;
    public int TotalPlayTime;
    public int TotalMatchCount;
    public int CompletedGameCount;
    public int AbandonedGameCount;
    public int WinCount;
    public int LoseCount;
    public int Energy;
    public int Gold;
    public int Diamond;
    public int Joker;


    public UserProgression
        (
        // Int
        int _level,
        int _cup,
        int _highScore,
        int _signInCount,
        int _totalPlayTime,
        int _totalMatchCount,
        int _completedGameCount,
        int _abandonedGameCount,
        int _winCount,
        int _loseCount,
        int _energy,
        int _gold,
        int _diamond,
        int _joker
        )
    {
        // Int
        Level = _level;
        Cup = _cup;
        HighScore = _highScore;
        SignInCount = _signInCount;
        TotalPlayTime = _totalPlayTime;
        TotalMatchCount = _totalMatchCount;
        CompletedGameCount = _completedGameCount;
        AbandonedGameCount = _abandonedGameCount;
        WinCount = _winCount;
        LoseCount = _loseCount;
        Energy = _energy;
        Gold = _gold;
        Diamond = _diamond;
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