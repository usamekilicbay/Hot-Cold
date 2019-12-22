using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{  
    string username { get; set; }
    int level { get; set; }
    int cup { get; set; }
    int highScore { get; set; }
    int win { get; set; }
    int lose { get; set; }
    int playTime { get; set; }
    //bool signInStatus { get; set; }


    public User(string username)
    {
        this.username = username;
        level = 0;
        cup = 0;
        highScore = 0;
        win = 0;
        lose = 0;
        playTime = 0;        
    }
}
