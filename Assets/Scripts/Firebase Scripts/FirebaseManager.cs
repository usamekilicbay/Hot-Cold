using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity;
using Firebase.Unity.Editor;

public class FirebaseManager : Singleton<FirebaseManager>
{  

    /*public DatabaseReference usersReference;
   
    private void Start()
    {
        Initialization();
        
    }

    void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {                    
                    usersReference = FirebaseDatabase.DefaultInstance.GetReference("Users");
                    Debug.Log("Connected!");
                    CreateUser("PapillonZ");
                }
                else
                {                 
                    Debug.LogError(System.String.Format("Hata: {0}", dependencyStatus));
                }
            });
    }

    // ok UserId nerden alınıyor?
    public void CreateUser(string username)
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        User user = new User(username);
        string json = JsonUtility.ToJson(user);

        string userID = usersReference.Push().Key;
        Debug.Log("User Id:" + userID);

        reference.Child(userID).SetRawJsonValueAsync(json);
        //usersReference.Child(userID).SetRawJsonValueAsync(json);
        Debug.Log("bot!");
    }

    void UpdateData(string userID, string username, int level, bool signInStatus)
    {
        Dictionary<string, object> user = new Dictionary<string, object>();
        user["username"] = username;
        user["level"] = level;
        user["signInStatus"] = signInStatus;

        usersReference.Child(userID).UpdateChildrenAsync(user);
    }*/
}
