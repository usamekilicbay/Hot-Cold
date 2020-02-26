using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class FirebaseUserManager : FirebaseBaseManager
{
    private void OnEnable()
    {
        ActionManager.Instance.CreatUserProfile += CreateUserProfile;
        ActionManager.Instance.CallGetCurrentUserProfile += CallGetCurrentUserProfile;
        ActionManager.Instance.DeleteUserProfile += DeleteUserProfile;
    }

    private void OnDisable()
    {
        ActionManager.Instance.CreatUserProfile -= CreateUserProfile;
        ActionManager.Instance.CallGetCurrentUserProfile -= CallGetCurrentUserProfile;
        ActionManager.Instance.DeleteUserProfile -= DeleteUserProfile;
    }

    private void CreateUserProfile(string username, string language)
    {
        // General
        UserGeneral userGenerals = new UserGeneral
            (
            username,
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            "Türkiye",
            language,
            true,
            true
            );

        string generalJson = JsonUtility.ToJson(userGenerals);
        Debug.Log(generalJson);
        userReference.Child(ConstantKeeper.UserPaths.General).SetRawJsonValueAsync(generalJson);


        // Progression
        UserProgression userProgressions = new UserProgression
          (
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1
          );

        string progressionJson = JsonUtility.ToJson(userProgressions);
        Debug.Log(progressionJson);
        userReference.Child(ConstantKeeper.UserPaths.Progression).SetRawJsonValueAsync(progressionJson);


        // Consumables
        UserConsumable userConsumable = new UserConsumable
            (
            10, 100, 10, 5
            );

        string consumableJson = JsonUtility.ToJson(userConsumable);
        Debug.Log(consumableJson);
        userReference.Child(ConstantKeeper.UserPaths.Consumable).SetRawJsonValueAsync(consumableJson);
    }

    private void GetUsers()
    {
        userReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                //           LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();

                foreach (DataSnapshot x in snapshot.Children)
                {
                    string key = x.Key;
                    object value = snapshot.Child(key).Value;
                    //Debug.Log(key);
                    //Debug.Log(value);
                    dictionary.Add(key, value);
                    Debug.Log(dictionary);
                }
            }
        }
        );
    }

    private void CallGetCurrentUserProfile()
    {
        Debug.Log("Geldik inebilirsin");
        StartCoroutine(GetCurrentUserProfile());
    }

    private IEnumerator GetCurrentUserProfile()
    {
        Task<DataSnapshot> task = userReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(ConstantKeeper.UserTasks.GetCurrentUserProfile + ConstantKeeper.Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(ConstantKeeper.UserTasks.GetCurrentUserProfile + ConstantKeeper.Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
       
            // String General 
            CurrentUserProfileKeeper.Username = snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.Username).Value.ToString();
            CurrentUserProfileKeeper.Country = snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.Country).Value.ToString();
            CurrentUserProfileKeeper.Language = snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.Language).Value.ToString();
            CurrentUserProfileKeeper.SignUpDate = snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.SignUpDate).Value.ToString();
            CurrentUserProfileKeeper.LastSeen = snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.LastSeen).Value.ToString();

            // Bool General
            CurrentUserProfileKeeper.SignInStatus = bool.Parse(snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.SignInStatus).Value.ToString());
            CurrentUserProfileKeeper.Intermateable = bool.Parse(snapshot.Child(ConstantKeeper.UserPaths.General).Child(ConstantKeeper.UserPaths.Intermateable).Value.ToString());


            // Int Progression
            CurrentUserProfileKeeper.Level = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.Level).Value.ToString());
            CurrentUserProfileKeeper.Cup = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.Cup).Value.ToString());
            CurrentUserProfileKeeper.Rank = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.Rank).Value.ToString());
            CurrentUserProfileKeeper.TotalPlayTime = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.TotalPlayTime).Value.ToString());
            CurrentUserProfileKeeper.TotalMatches = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.TotalMatches).Value.ToString());
            CurrentUserProfileKeeper.CompletedMatches = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.CompletedMatches).Value.ToString());
            CurrentUserProfileKeeper.AbandonedMatches = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.AbandonedMatches).Value.ToString());
            CurrentUserProfileKeeper.Wins = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.Wins).Value.ToString());
            CurrentUserProfileKeeper.Losses = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.Losses).Value.ToString());
            CurrentUserProfileKeeper.WinningStreak = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Progression).Child(ConstantKeeper.UserPaths.WinningStreak).Value.ToString());
            
            // Int Consumable
            CurrentUserProfileKeeper.Papcoin = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Consumable).Child(ConstantKeeper.UserPaths.Papcoin).Value.ToString());
            CurrentUserProfileKeeper.Gem = int.Parse(snapshot.Child(ConstantKeeper.UserPaths.Consumable).Child(ConstantKeeper.UserPaths.Gem).Value.ToString());
            
            ActionManager.Instance.ShowUserProfilePanel();
        }

       // LogTaskCompletion(task, "Şimdiki kullanıcı bilgileri çekme ");
    }

    private void DeleteUserProfile() 
    {
        userReference.RemoveValueAsync();

       /* if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.res

        }*/
    }

    private void UpdateUserData(string key, object value, string path)
    {
        userReference.Child(path).Child(key).SetValueAsync(value);
    }

    
}
