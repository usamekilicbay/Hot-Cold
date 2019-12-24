public class User
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