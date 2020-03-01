using ConstantKeeper;
using Firebase.Database;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class FirebaseUserManager : MonoBehaviour
{
    

    private void OnEnable()
    {
        ActionManager.Instance.CreatUserProfile += CreateUserProfile;
        ActionManager.Instance.UpdateUserData += UpdateUserData;
        ActionManager.Instance.CallGetCurrentUserProfile += CallGetCurrentUserProfile;
        ActionManager.Instance.DeleteUserProfile += DeleteUserProfile;

        if (FirebaseBaseManager.auth != null)
        {
            //AddUserDataListener();
        }
    }

    private void OnDisable()
    {
        ActionManager.Instance.CreatUserProfile -= CreateUserProfile;
        ActionManager.Instance.UpdateUserData -= UpdateUserData;
        ActionManager.Instance.CallGetCurrentUserProfile -= CallGetCurrentUserProfile;
        ActionManager.Instance.DeleteUserProfile -= DeleteUserProfile;
    }

    private void OnApplicationQuit()
    {
        ActionManager.Instance.CreatUserProfile -= CreateUserProfile;
        ActionManager.Instance.UpdateUserData -= UpdateUserData;
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
        FirebaseBaseManager.userReference.Child(UserPaths.General).SetRawJsonValueAsync(generalJson);


        // Progression
        UserProgression userProgressions = new UserProgression
          (
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1
          );

        string progressionJson = JsonUtility.ToJson(userProgressions);
        Debug.Log(progressionJson);
        FirebaseBaseManager.userReference.Child(UserPaths.Progression).SetRawJsonValueAsync(progressionJson);


        // Consumables
        UserConsumable userConsumable = new UserConsumable
            (
            10, 100, 10, 5
            );

        string consumableJson = JsonUtility.ToJson(userConsumable);
        Debug.Log(consumableJson);
        FirebaseBaseManager.userReference.Child(UserPaths.Consumable).SetRawJsonValueAsync(consumableJson);
    }

    private void GetUsers()
    {
        FirebaseBaseManager.userReference.GetValueAsync().ContinueWith(task =>
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

        DatabaseReference getCurrentUserProfileReference = FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}/{FirebaseBaseManager.auth.CurrentUser.UserId}");
       
        Task<DataSnapshot> task = getCurrentUserProfileReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(UserTasks.GetCurrentUserProfile + Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(UserTasks.GetCurrentUserProfile + Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
       
            // String General 
            CurrentUserProfileKeeper.Username = snapshot.Child(UserPaths.General).Child(UserPaths.Username).Value.ToString();
            CurrentUserProfileKeeper.Country = snapshot.Child(UserPaths.General).Child(UserPaths.Country).Value.ToString();
            CurrentUserProfileKeeper.Language = snapshot.Child(UserPaths.General).Child(UserPaths.Language).Value.ToString();
            CurrentUserProfileKeeper.SignUpDate = snapshot.Child(UserPaths.General).Child(UserPaths.SignUpDate).Value.ToString();
            CurrentUserProfileKeeper.LastSeen = snapshot.Child(UserPaths.General).Child(UserPaths.LastSeen).Value.ToString();

            // Bool General
            CurrentUserProfileKeeper.SignInStatus = bool.Parse(snapshot.Child(UserPaths.General).Child(UserPaths.SignInStatus).Value.ToString());
            CurrentUserProfileKeeper.Intermateable = bool.Parse(snapshot.Child(UserPaths.General).Child(UserPaths.Intermateable).Value.ToString());


            // Int Progression
            CurrentUserProfileKeeper.Level = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.Level).Value.ToString());
            CurrentUserProfileKeeper.Cup = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.Level).Value.ToString());
            CurrentUserProfileKeeper.Rank = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.Rank).Value.ToString());
            CurrentUserProfileKeeper.TotalPlayTime = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.TotalPlayTime).Value.ToString());
            CurrentUserProfileKeeper.TotalMatches = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.TotalMatches).Value.ToString());
            CurrentUserProfileKeeper.CompletedMatches = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.CompletedMatches).Value.ToString());
            CurrentUserProfileKeeper.AbandonedMatches = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.AbandonedMatches).Value.ToString());
            CurrentUserProfileKeeper.Wins = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.Wins).Value.ToString());
            CurrentUserProfileKeeper.Losses = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.Losses).Value.ToString());
            CurrentUserProfileKeeper.WinningStreak = int.Parse(snapshot.Child(UserPaths.Progression).Child(UserPaths.WinningStreak).Value.ToString());
            
            // Int Consumable
            CurrentUserProfileKeeper.Papcoin = int.Parse(snapshot.Child(UserPaths.Consumable).Child(UserPaths.Papcoin).Value.ToString());
            CurrentUserProfileKeeper.Gem = int.Parse(snapshot.Child(UserPaths.Consumable).Child(UserPaths.Gem).Value.ToString());
            
            ActionManager.Instance.ShowUserProfilePanel();
        }
    }

    private void DeleteUserProfile() 
    {
        FirebaseBaseManager.userReference.RemoveValueAsync();

       /* if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.res

        }*/
    }

    private void UpdateUserData(string secondaryPath, string key, object value)
    {
        AddUserDataListener();
        FirebaseBaseManager.userReference.Child(secondaryPath).Child(key).SetValueAsync(value);
    }

    private void AddUserDataListener() 
    {
        Debug.Log("Shigure");
        FirebaseBaseManager.userReference.Child(UserPaths.General).ChildChanged += GetuserDAtaBridge;
        FirebaseBaseManager.userReference.Child(UserPaths.Progression).ChildChanged += GetuserDAtaBridge;
        FirebaseBaseManager.userReference.Child(UserPaths.Consumable).ChildChanged += GetuserDAtaBridge;
    }
   
    private void RemoveUserDataListener()
    {
        FirebaseBaseManager.userReference.Child(UserPaths.General).ChildChanged -= GetuserDAtaBridge;
        FirebaseBaseManager.userReference.Child(UserPaths.Progression).ChildChanged -= GetuserDAtaBridge;
        FirebaseBaseManager.userReference.Child(UserPaths.Consumable).ChildChanged -= GetuserDAtaBridge;
    }

    void GetuserDAtaBridge(object sender, ChildChangedEventArgs args)
    {
        Debug.Log("ML");
        if (FirebaseBaseManager.auth.CurrentUser == null)
        {
            return;
        }
        else
        {
            StartCoroutine(GetCurrentUserProfile());
        }
    }
}
