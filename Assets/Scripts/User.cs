public class User
{
    // String variables
    string username { get; set; }
    string signUpDate { get; set; }
    string lastSignInDate { get; set; }
    string country { get; set; }
    string language { get; set; }

    // Int variables
    int level { get; set; }
    int cup { get; set; }
    int highScore { get; set; }
    int signInCount { get; set; }
    int totalPlayTime { get; set; }
    int totalMatchCount { get; set; }
    int completedGameCount { get; set; }
    int abandonedGameCount { get; set; }
    int winCount { get; set; }
    int loseCount { get; set; }
    int energy { get; set; }
    int gold { get; set; }
    int diamond { get; set; }
    int joker { get; set; }

    // Bool variables
    bool signInStatus { get; set; }


    public User
        (
        // String
        string username,
        string signUpDate,
        string lastSignInDate,
        string country,
        string language,
        // Int
        int level,
        int cup,
        int highScore,
        int signInCount,
        int totalPlayTime,
        int totalMatchCount,
        int completedGameCount,
        int abandonedGameCount,
        int winCount,
        int loseCount,
        int energy,
        int gold,
        int diamond,
        int joker,
        // Bool
        bool signInStatus
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
    }
}
