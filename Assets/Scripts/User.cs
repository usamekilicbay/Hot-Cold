using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    string userName { get; set; }
    int level { get; set; }
    bool signInStatus { get; set; }


    public User(string userName, int level, bool signInStatus)
    {
        this.userName = userName;
        this.level = level;
        this.signInStatus = signInStatus;
    }
}
