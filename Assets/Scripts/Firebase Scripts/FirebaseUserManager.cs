using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class FirebaseUserManager : FBManager
{
    private void OnEnable()
    {
        ActionManager.Instance.CreatUserProfile += CreateUserProfile;
        ActionManager.Instance.CallCurrentUserProfile += CallGetCurrentUserProfile;
        ActionManager.Instance.DeleteUserProfile += DeleteUserProfile;
    }

    private void OnDisable()
    {
        ActionManager.Instance.CreatUserProfile -= CreateUserProfile;
        ActionManager.Instance.CallCurrentUserProfile -= CallGetCurrentUserProfile;
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
        userReference.Child("General").SetRawJsonValueAsync(generalJson);


        // Progression
        UserProgression userProgressions = new UserProgression
          (
          0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1
          );

        string progressionJson = JsonUtility.ToJson(userProgressions);
        Debug.Log(progressionJson);
        userReference.Child("Progression").SetRawJsonValueAsync(progressionJson);


        // Consumables
        UserConsumable userConsumable = new UserConsumable
            (
            10, 100, 10, 5
            );

        string consumableJson = JsonUtility.ToJson(userConsumable);
        Debug.Log(consumableJson);
        userReference.Child("Consumable").SetRawJsonValueAsync(consumableJson);
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

    public void CallGetCurrentUserProfile()
    {
        Debug.Log("Geldik inebilirsin");
        StartCoroutine(GetCurrentUserProfile());
    }

    private IEnumerator GetCurrentUserProfile()
    {
        Task<DataSnapshot> task = userReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
       
            // String General 
            CurrentUserProfileKeeper.Username = snapshot.Child("General").Child("Username").Value.ToString();
            CurrentUserProfileKeeper.Country = snapshot.Child("General").Child("Country").Value.ToString();
            CurrentUserProfileKeeper.Language = snapshot.Child("General").Child("Language").Value.ToString();
            CurrentUserProfileKeeper.SignUpDate = snapshot.Child("General").Child("SignUpDate").Value.ToString();
            CurrentUserProfileKeeper.LastSeen = snapshot.Child("General").Child("LastSeen").Value.ToString();

            // Bool General
            CurrentUserProfileKeeper.SignInStatus = bool.Parse(snapshot.Child("General").Child("SignInStatus").Value.ToString());
            CurrentUserProfileKeeper.Intermateable = bool.Parse(snapshot.Child("General").Child("Intermateable").Value.ToString());


            // Int Progression
            CurrentUserProfileKeeper.Level = int.Parse(snapshot.Child("Progression").Child("Level").Value.ToString());
            CurrentUserProfileKeeper.Cup = int.Parse(snapshot.Child("Progression").Child("Cup").Value.ToString());
            CurrentUserProfileKeeper.Rank = int.Parse(snapshot.Child("Progression").Child("Rank").Value.ToString());
            CurrentUserProfileKeeper.TotalPlayTime = int.Parse(snapshot.Child("Progression").Child("TotalPlayTime").Value.ToString());
            CurrentUserProfileKeeper.TotalMatches = int.Parse(snapshot.Child("Progression").Child("TotalMatches").Value.ToString());
            CurrentUserProfileKeeper.CompletedMatches = int.Parse(snapshot.Child("Progression").Child("CompletedMatches").Value.ToString());
            CurrentUserProfileKeeper.AbandonedMatches = int.Parse(snapshot.Child("Progression").Child("AbandonedMatches").Value.ToString());
            CurrentUserProfileKeeper.Wins = int.Parse(snapshot.Child("Progression").Child("Wins").Value.ToString());
            CurrentUserProfileKeeper.Losses = int.Parse(snapshot.Child("Progression").Child("Losses").Value.ToString());
            CurrentUserProfileKeeper.WinningStreak = int.Parse(snapshot.Child("Progression").Child("WinningStreak").Value.ToString());
            
            // Int Consumable
            CurrentUserProfileKeeper.Gold = int.Parse(snapshot.Child("Consumable").Child("Gold").ToString());

            ActionManager.Instance.ShowUserProfilePanel();
        }

       // LogTaskCompletion(task, "Şimdiki kullanıcı bilgileri çekme ");
    }

    private void DeleteUserProfile() 
    {
        Task task = userReference.RemoveValueAsync();

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
