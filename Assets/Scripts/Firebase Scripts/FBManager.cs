using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Firebase kitaplığı
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Auth;
using Firebase.Database;

//Task yani görev olayları sistemden alındığı için, kütüphane ekliyoruz
using UnityEngine.Events;
using System;
using System.Threading.Tasks;

public class FBManager : Singleton<FBManager>
{
    //Firebase temel ayarlarımız
    protected Firebase.Auth.FirebaseAuth auth;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByauth = new Dictionary<string, FirebaseUser>();
    Firebase.DependencyStatus DepStatus = Firebase.DependencyStatus.UnavailableOther;
    private bool fetchingToken = false;



    // Private Variables
    private string roomID;
    private string userID;
    private string username;

    // Database References
    DatabaseReference userReference;
    DatabaseReference roomReference;

    // Script References
    UIManager uiManager;
    NumberCreator numberCreator;

   
    private void OnEnable()
    {
        uiManager = UIManager.Instance;
        numberCreator = FindObjectOfType<NumberCreator>();

        ActionManager.Instance.CallCurrentUserProfile += CallGetCurrentUserProfile;

        ActionManager.Instance.SignUpEmailPassword += SignUpEmailPassword;
        ActionManager.Instance.SignInEmailPassword += SignInEmailPassword;

        ActionManager.Instance.QuickGame += CallQuickGame;

        ActionManager.Instance.SendEstimation += SendEstimation;
        FireBaseStart();
    }

    private void OnDisable()
    {
       /* ActionManager.Instance.SignUpEmailPassword -= SignUpEmailPassword;
        ActionManager.Instance.SignInEmailPassword -= SignInEmailPassword;
       
        ActionManager.Instance.QuickGame -= CallQuickGame;

        ActionManager.Instance.CallCurrentUserProfile -= CallGetCurrentUserProfile;*/
    }

    private void FireBaseStart()
    {
        DepStatus = Firebase.FirebaseApp.CheckDependencies();

        if (DepStatus == DependencyStatus.Available)
        {
            InitalizeFirebase();
            return;
        }

        if (DepStatus != Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith
                (task =>
                {
                    DepStatus = Firebase.FirebaseApp.CheckDependencies();

                    if (DepStatus == Firebase.DependencyStatus.Available)
                    {
                        InitalizeFirebase();
                    }
                    else { Debug.Log("Hata oluştu!"); }
                });
        }
        else { InitalizeFirebase(); }

        /* if (auth.CurrentUser != null)
         {
             Debug.Log("User Yok!");
             uiManager.show
         }*/

        Debug.Log("Bağlantı Sağlandı");
        // StartCoroutine(SignInAgain());
    }


    private void InitalizeFirebase()
    {
        Debug.Log("firebase");
        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;

        Debug.Log("Initial firebase");

        if (auth == null)
        {
            Debug.Log("Auth null");
        }

        // Database Reference Declare
        if (auth.CurrentUser != null) GetUserID();
        else uiManager.ShowSignInPanel();


        roomReference = FirebaseDatabase.DefaultInstance.GetReference($"Rooms/RoomID");


        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

        // auth.StateChanged += AuthStateChanged;
        // auth.IdTokenChanged += IdTokenChanged;


        //  AuthStateChanged(this, null);

    }


    #region --------------------------------------------AUTHENTICATION--------------------------------------------------------------
    /* void AuthStateChanged(object sender, System.EventArgs eventArgs)
     {
         Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
         Firebase.Auth.FirebaseUser user = null;
         if (senderAuth == auth && senderAuth.CurrentUser != user)
         {
             bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;

             if (!signedIn && user != null)
             {
                 Debug.Log("Çıkış yapan kullanıcı:" + user.UserId);
              //   StartCoroutine(SignUpAnonym());
             }
             user = senderAuth.CurrentUser;
             userByauth[senderAuth.App.Name] = user;
             if (signedIn)
             {
                 Debug.Log("Giriş yapan kullanıcı:" + user.UserId);

               //  SignUp(auth.CurrentUser.UserId.ToString());
             }
         }
     }*/



    /* void IdTokenChanged(object sender, System.EventArgs eventArgs)
     {
         Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
         if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
         {
             senderAuth.CurrentUser.TokenAsync(false).ContinueWith
                 (
                task => Debug.Log(System.String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8)))
                );

         }
     }*/
     
    bool LogTaskCompletion(Task task, string operation)
    {
        bool complete = false;
        if (task.IsCanceled)
        {
            Debug.Log(operation + " işleminden çıkıldı...");
        }
        else if (task.IsFaulted)
        {
            Debug.Log(operation + " işlemi tamamlanamadı...");
        }
        else if (task.IsCompleted)
        {
            Debug.Log("işlemi başarıyla tamamlandı.");
            complete = true;
        }
        return complete;
    }

    /* //Kullanıcımız Anonim giriş yaptı
     IEnumerator SignUpAnonym()
     {
         Debug.Log("AnonimGiris olarak ilk giriş yapıldı...");
         auth.SignInAnonymouslyAsync().ContinueWith(SignInAnonym);
         yield return new WaitForSeconds(2.0f);
     }

     private void SignInAnonym(Task<Firebase.Auth.FirebaseUser> authTask)
     {
         if (LogTaskCompletion(authTask, "Giriş Yapıldı")) ;
     }*/

    //Kullanıcımız oyuna yeniden girdi
    IEnumerator SignInAgain()
    {
        Debug.Log("Tekrar Giriş yapıldı....");
        //SignUp(auth.CurrentUser.UserId.ToString());
        yield return new WaitForSeconds(2.0f);

    }

    private void SignUpEmailPassword(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                //LogTaskCompletion(task, "Giriş işlemi iptal edildi");
                Debug.Log("Giriş işlemi iptal edildi");
                return;
            }
            else if (task.IsFaulted)
            {
               // LogTaskCompletion(task, "Kayıt işlemi başarısız oldu!");
               Debug.Log("Kayıt işlemi başarısız oldu!");
                return;
            }
            else if (task.IsCompleted) 
            {
               // LogTaskCompletion(task, "Kayıt işlemi başarıyla tamamlandı!");
               Debug.Log("Kayıt işlemi başarıyla tamamlandı!");

                FirebaseUser newUser = task.Result;

                GetUserID();

                CreateUser(username);
            }
            
        });
    }


    private void SignInEmailPassword(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
             {
                 if (task.IsFaulted)
                 {
                     Debug.Log("Giriş işlemi başarısız");
                 }
                 else if (task.IsCompleted)
                 {
                     uiManager.ShowMenuPanel();
                 }
             });
    }


    private void GetUserID()
    {
        userID = auth.CurrentUser.UserId;
        Debug.Log("User Id " + userID);
        SetUserReference();
    }

    private void SetUserReference()
    {
        userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}");

        StartCoroutine(GetCurrentUserProfile());
    }

    #endregion


    #region --------------------------------------------USER--------------------------------------------------------------

    private void CreateUser(string username)
    {

        // General
        UserGeneral userGenerals = new UserGeneral
            (
            username,
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            "Türkiye",
            "Türkçe",
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
      //  StartCoroutine(GetCurrentUserProfile());
    }

    private IEnumerator GetCurrentUserProfile()
    {
        Task<DataSnapshot> task = userReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        //Dictionary<string, object> currentUserProfileDictionary = new Dictionary<string, object>();

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



            //currentUserProfileDictionary["Level"] = snapshot.Child("Progression").Child("Level").Value;
            //currentUserProfileDictionary["Cup"] = snapshot.Child("Progression").Child("Cup").Value;
            //currentUserProfileDictionary["Rank"] = snapshot.Child("Progression").Child("Rank").Value;
            //currentUserProfileDictionary["Username"] = snapshot.Child("General").Child("Username").Value;
            //currentUserProfileDictionary["SignUpDate"] = snapshot.Child("General").Child("SignUpDate").Value;
            //currentUserProfileDictionary["LastSeen"] = snapshot.Child("General").Child("LastSeen").Value;
            //currentUserProfileDictionary["TotalPlayTime"] = snapshot.Child("Progression").Child("TotalPlayTime").Value;
            //currentUserProfileDictionary["TotalMatches"] = snapshot.Child("Progression").Child("TotalMatches").Value;
            //currentUserProfileDictionary["CompletedMatches"] = snapshot.Child("Progression").Child("CompletedMatches").Value;
            //currentUserProfileDictionary["AbandonedMatches"] = snapshot.Child("Progression").Child("AbandonedMatches").Value;
            //currentUserProfileDictionary["Wins"] = snapshot.Child("Progression").Child("Wins").Value;
            //currentUserProfileDictionary["Losses"] = snapshot.Child("Progression").Child("Losses").Value;
            //currentUserProfileDictionary["WinningStreak"] = snapshot.Child("Progression").Child("WinningStreak").Value;
            //currentUserProfileDictionary["SignInStatus"] = snapshot.Child("General").Child("SignInStatus").Value;

            //ActionManager.Instance.GetCurrentUserProfile(currentUserProfileDictionary);
        }

        LogTaskCompletion(task, "Şimdiki kullanıcı bilgileri çekme ");
    }

    private void UpdateUserData(string key, object value, string path)
    {
        userReference.Child(path).Child(key).SetValueAsync(value);
    }
    
    #endregion


    #region --------------------------------------------ROOM--------------------------------------------------------------

    bool canPlay = true;
    bool canStart = false;

    public void CallQuickGame() { StartCoroutine(QuickGame()); }

    private IEnumerator QuickGame()
    {
        Task<DataSnapshot> task = roomReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        if (task.IsFaulted)
        {
            Debug.Log("Oyun bulma işlemi hata verdi!");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.HasChildren)
            {
                if (canPlay)
                {
                    foreach (DataSnapshot rooms in snapshot.Children)
                    {
                        bool penetrability = bool.Parse(rooms.Child("General").Child("Penetrability").Value.ToString());

                        if (penetrability)
                        {
                            roomReference.Child(rooms.Key).Child("General").Child("P2-ID").SetValueAsync(userID);
                            roomReference.Child(rooms.Key).Child("General").Child("P2-Username").SetValueAsync(CurrentUserProfileKeeper.Username);
                            canPlay = false;
                            roomID = rooms.Key;

                            canStart = true;

                            if (canStart)
                            {
                                StartGame();
                            }

                            Debug.Log("Odaya giriş başarılı!");
                        }
                        else
                        {
                            CreateRoom();
                            Debug.Log("Girilebilir bir oda bulunamadı, oda oluşturuluyor...");
                        }
                    }
                }
                else
                {
                    Debug.Log("Zaten bir odaya dahilsiniz tekrar giriş yapamazsınız");
                }
            }
            else if (!snapshot.HasChildren)
            {
                CreateRoom();
                Debug.Log("Yeni oda oluşturuluyor, oyuncu bekleniyor");
            }
        }
    }

    private void CreateRoom(string roomName = "", string roomPassword = "")
    {
        if (canPlay)
        {
            roomID = roomReference.Push().Key;

            // General
            Dictionary<string, object> roomGeneralDictionary = new Dictionary<string, object>
            {
              /*["RoomName"] = roomName,
                ["RoomPassword"] = roomPassword,*/
                ["RoomID"] = roomID,
                ["P1-ID"] = userID,
                ["P1-Username"] = CurrentUserProfileKeeper.Username,
                ["P2-ID"] = "",
                ["P2-Username"] = "",
                ["ScoreLimit"] = 1,
                ["AnswerTimeLimit"] = 5,
                ["PlayerLimit"] = 2,
                ["SecretNumber"] = 0,
                ["SecretNumberMaxValue"] = 1000,
                ["Penetrability"] = true
            };

            roomReference.Child(roomID).Child("General").UpdateChildrenAsync(roomGeneralDictionary);


            // Progression
            Dictionary<string, object> roomProgressionDictionary = new Dictionary<string, object>
            {
                ["LastEstimation"] = 0,
                ["WhoseTurn"] = ""
            };

            roomReference.Child(roomID).Child("Progression").UpdateChildrenAsync(roomProgressionDictionary);

            canPlay = false;

            Debug.Log("Yeni oda oluşturuldu, oyuncu bekleniyor");

            StartGame();
        }
        else
        {
            Debug.Log("Zaten kurulmuş bir odanız var!");
        }
    }
    

    private void GetRoomID(string roomName) 
    {
        roomReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            else if (task.IsFaulted)
            {
                return;
            }

            DataSnapshot snapshot = task.Result;

            foreach (DataSnapshot roomId in snapshot.Children)
            {
                if (roomId.Child("General").Child("RoomName").Value.ToString() == roomName)
                {
                    roomID = roomId.Key.ToString();
                }
            }

        });
    }




    private void EnterTheRoom(string roomName, string roomPassword)
    {
        GetRoomID(roomName);

        string correctRoomPassword = "";

        roomReference.Child(roomID).Child("General").Child("RoomPassword").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                return;
            }

            DataSnapshot snapshot = task.Result;

            correctRoomPassword = snapshot.Value.ToString();
        });

        if (correctRoomPassword != "")
        {
            if (roomPassword == correctRoomPassword)
            {
                roomReference.Child(roomID).Child("General").Child("P2-ID").SetValueAsync(userID);
                roomReference.Child(roomID).Child("General").Child("P2-UserName").SetValueAsync(username);
                return;
            }
            else
            {
                Debug.Log("Oda ismi ile şifre uyuşmuyor, tekrar deneyin...");
                return;
            }
        }
        else if (correctRoomPassword == "")
        {
            Debug.Log("şifresiz giriş");
            roomReference.Child(roomID).Child("General").Child("P2-ID").SetValueAsync(userID);
            roomReference.Child(roomID).Child("General").Child("P2-UserName").SetValueAsync(username);
            return;
        }
    }

    private void AddRoomListToDropdown(TMPro.TMP_Dropdown dropdown) 
    {
        roomReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            else if (task.IsFaulted)
            {
                return;
            }

            DataSnapshot snapshot = task.Result;

            List<string> roomList = new List<string>();

            foreach (DataSnapshot r in snapshot.Children)
            {
                string roomId = r.Key;
                string roomName = snapshot.Child(roomId).Child("RoomName").Value.ToString();
                string roomOwner = snapshot.Child(roomId).Child("Player1-ID").Value.ToString();

                roomList.Add(roomName);
                Debug.Log($"Room ID = {roomId}  Room Creator = {roomOwner}");
            }

            dropdown.AddOptions(roomList);
            
        });
    }

    #endregion


    #region --------------------------------------------GAME--------------------------------------------------------------

    private void StartGame() 
    {
        SetSecretNumber(NumberCreator.CreateNumber());
        GetRoomInfos();
        uiManager.ShowGamePanel();
    }

	private void SetSecretNumber(int currentNumber) 
    {
        roomReference.Child(roomID).Child("General").Child("SecretNumber").SetValueAsync(currentNumber);
    }

    private void GetRoomInfos()
    {
        Task<DataSnapshot> task = roomReference.Child(roomID).Child("General").GetValueAsync();

        //yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted || task.IsCanceled);

        if (task.IsCanceled)
        {
            Debug.Log("Gizli sayı çekme işlemi iptal edildi");
        }
        else if (task.IsFaulted)
        {
            Debug.Log("Gizli sayı çekme işlemi iptal başarısız");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            
            CurrentRoomInfoKeeper.rivalID = snapshot.Child("P2-ID").Value.ToString();
            CurrentRoomInfoKeeper.rivalUsername = snapshot.Child("P2-Username").Value.ToString();
            CurrentRoomInfoKeeper.answerTimeLimit = int.Parse(snapshot.Child("AnswerTimeLimit").Value.ToString());
            CurrentRoomInfoKeeper.scoreLimit = int.Parse(snapshot.Child("ScoreLimit").Value.ToString());
            CurrentRoomInfoKeeper.secretNumber = int.Parse(snapshot.Child("SecretNumber").Value.ToString());
            CurrentRoomInfoKeeper.secretNumberMaxValue = int.Parse(snapshot.Child("SecretNumberMaxValue").Value.ToString());
        }
    }


   /* private void Estimate(int _estimation) 
    {
        int currentUserEstimation = _estimation;

        if (currentUserEstimation == CurrentRoomInfoKeeper.secretNumber)
        {
            Debug.Log("Kazandın!");
        }
        else
        {
            SetCurrentUserEstimation(currentUserEstimation);
        }
    }*/

    private void SendEstimation(int _estimation) 
    {
        roomReference.Child(roomID).Child("Progression").Child("LastEstimation").SetValueAsync(_estimation);
        GameProgressionListener();
    }

    private void GameProgressionListener() 
    {
        roomReference.Child(roomID).Child("Progression").Child("LastEstimation").ValueChanged += GetEstimation;
    }

    private void GetEstimation(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Değişiklik algılandı");

        DataSnapshot snapshot = args.Snapshot;

        ShowLastEstimation(snapshot.Value.ToString());
    }
    private void ShowLastEstimation(string lastEstimation)
    {
        ActionManager.Instance.ShowLastEstimation(lastEstimation);
    }

	#endregion
}
