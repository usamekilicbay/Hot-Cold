public class Room
{
    // String
    public string roomID; 	
    public string roomName; 	
    public string roomPassword;	
    public string creatorID; 	
    public string creatorUsername; 	
    public string playerLimit;	
    public string scoreLimit;

    // Bool
    public bool electiveness;

    public Room
        (
          // String
          string _roomID,
          string _roomName,
          string _creatorID,
          string _password,
          string _creatorUsername,
          string _playerLimit,
          string _scoreLimit,

          // Bool
          bool _electiveness
        )
    {
        // String
        roomID = _roomID;
        creatorID = _creatorID;
        roomPassword = _password;
        creatorUsername = _creatorUsername;
        playerLimit = _playerLimit;
        scoreLimit = _scoreLimit;

        // Bool
        electiveness = _electiveness;
    }
}