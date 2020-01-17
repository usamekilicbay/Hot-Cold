using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Firebase kitaplığı
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Auth;
using Firebase.Database;

//Task yani görev olayları sistemden alındığı için, kütüphane ekliyoruz
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Events;

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

    void Start()
    {
        FireBaseStart();
        uiManager = UIManager.Instance;
        numberCreator = FindObjectOfType<NumberCreator>();
        /*UpdateUserData("gold", "550");*/

        // Action Maker
        SetAction();
    }


    public void FireBaseStart()
    {
        DepStatus = Firebase.FirebaseApp.CheckDependencies();
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


    void InitalizeFirebase()
    {
        //Firebase kullanıcı oturum açma isteği
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;


        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        // Database References Declare
        if (auth.CurrentUser != null) GetUserID();


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
            Debug.Log(operation + " çıkıldı...");
        }
        else if (task.IsFaulted)
        {
            Debug.Log(operation + " hata oluştu...");
        }
        else if (task.IsCompleted)
        {
            Debug.Log("İşlem Tamam.");
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

    public void SignUpEmailPasssword(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Giriş işlemi iptal edildi");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Kayıt işlemi başarısız oldu!");
                return;
            }

            FirebaseUser newUser = task.Result;

            GetUserID();

            CreateUser(username);
        });
    }


    public void SignInEmailPassword(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
             {
                 if (task.IsFaulted)
                 {
                     Debug.Log("Giriş işlemi başarısız");
                 }
                 else if (task.IsCompleted)
                 {
                     uiManager.ShowMenuPanelBridge();
                 }
             });
    }


    public void GetUserID()
    {
        userID = auth.CurrentUser.UserId;
        SetUserReference();
    }

    public void SetUserReference()
    {
        userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}");
        //CreateUser("Papillon The Fallen");
    }

    #endregion


    #region --------------------------------------------USER--------------------------------------------------------------

    public void CreateUser(string username)
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

    public void GetUsers()
    {
        userReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
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

    private UnityAction<Dictionary<string, object>> trigger;
    void SetAction()
    {
        trigger += (dictionary) => uiManager.GetOwnInfos(dictionary);
    }

    bool deneme = true;
    public void GetUserData()
    {
        Dictionary<string, object> userInfoDictionary = new Dictionary<string, object>();

        userReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                //LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
                Debug.Log("başarısız veri çekme işlemi");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                userInfoDictionary["Level"] = snapshot.Child("Progression").Child("Level").Value;
                userInfoDictionary["Cup"] = snapshot.Child("Progression").Child("Cup").Value;
                userInfoDictionary["Rank"] = snapshot.Child("Progression").Child("Rank").Value;
                userInfoDictionary["Username"] = snapshot.Child("General").Child("Username").Value;
                userInfoDictionary["SignUpDate"] = snapshot.Child("General").Child("SignUpDate").Value;
                userInfoDictionary["LastSeen"] = snapshot.Child("General").Child("LastSeen").Value;
                userInfoDictionary["TotalPlayTime"] = snapshot.Child("Progression").Child("TotalPlayTime").Value;
                userInfoDictionary["TotalMatches"] = snapshot.Child("Progression").Child("TotalMatches").Value;
                userInfoDictionary["CompletedMatches"] = snapshot.Child("Progression").Child("CompletedMatches").Value;
                userInfoDictionary["AbandonedMatches"] = snapshot.Child("Progression").Child("AbandonedMatches").Value;
                userInfoDictionary["Wins"] = snapshot.Child("Progression").Child("Wins").Value;
                userInfoDictionary["Losses"] = snapshot.Child("Progression").Child("Losses").Value;
                userInfoDictionary["WinningStreak"] = snapshot.Child("Progression").Child("WinningStreak").Value;
                userInfoDictionary["SignInStatus"] = snapshot.Child("General").Child("SignInStatus").Value;


                /* uiManager.GetOwnInfos
                    (
                        userInfoDictionary["Level"].ToString(),
                        userInfoDictionary["Cup"].ToString(),
                        userInfoDictionary["Rank"].ToString(),
                        userInfoDictionary["Username"].ToString(),
                        userInfoDictionary["signUpDate"].ToString(),
                        userInfoDictionary["LastSeen"].ToString(),
                        userInfoDictionary["TotalPlayTime"].ToString(),
                        userInfoDictionary["CompletedMatches"].ToString(),
                        userInfoDictionary["AbandonedMatches"].ToString(),
                        userInfoDictionary["Wins"].ToString(),
                        userInfoDictionary["Losses"].ToString(),
                        userInfoDictionary["WinningStreak"].ToString(),
                        bool.Parse(userInfoDictionary["SignInStatus"].ToString())
             */
                deneme = true;
            }
        });
            GetUserDataControl();
    }

    bool GetUserDataControl() 
    {
        return deneme;
    }

    IEnumerator GetOwnInfosBridge(Dictionary<string, object> dictionary)
    {
        yield return new WaitUntil(GetUserDataControl);
        Debug.Log(dictionary["Level"]);
        Debug.Log(dictionary["Cup"]);
        uiManager.GetOwnInfos(dictionary);
    }

    public void UpdateUserData(string key, object value, string path)
    {
        userReference.Child(path).Child(key).SetValueAsync(value);
    }
    
    #endregion

    #region --------------------------------------------ROOM--------------------------------------------------------------

    bool canPlay = true;
    bool canStart = false;

    public void QuickGame()
    {
        roomReference.GetValueAsync().ContinueWith(task =>
        {
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
                                roomReference.Child(rooms.Key).Child("General").Child("Player2-ID").SetValueAsync(userID);
                                roomReference.Child(rooms.Key).Child("General").Child("Player2-Username").SetValueAsync(username);
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
                    Debug.Log("Yeni oda oluşturuldu, oyuncu bekleniyor");
                }
            }
        });
    }

    public void CreateRoom(string roomName = "", string roomPassword = "")
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
                ["Player1-ID"] = userID,
                ["Player1-Username"] = username,
                ["Player2-ID"] = "",
                ["Player2-Username"] = "",
                ["ScoreLimit"] = 1,
                ["PlayerLimit"] = 2,
                ["Penetrability"] = true
            };

            roomReference.Child(roomID).Child("General").UpdateChildrenAsync(roomGeneralDictionary);


            // Progression
            Dictionary<string, object> roomProgressionDictionary = new Dictionary<string, object>
            {
                ["SecretNumber"] = 0,
                ["SecretNumberMaxValue"] = 0,
                ["LastEstimation"] = 0,
                ["WhoseTurn"] = ""
            };

            roomReference.Child(roomID).Child("Progression").UpdateChildrenAsync(roomProgressionDictionary);

            canPlay = false;
        }
        else
        {
            Debug.Log("Zaten kurulmuş bir odanız var!");
        }
    }
    

    public void GetRoomID(string roomName) 
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




    public void EnterTheRoom(string roomName, string roomPassword)
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
                roomReference.Child(roomID).Child("General").Child("Player2-ID").SetValueAsync(userID);
                roomReference.Child(roomID).Child("General").Child("Player2-UserName").SetValueAsync(username);
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
            roomReference.Child(roomID).Child("General").Child("Player2-ID").SetValueAsync(userID);
            roomReference.Child(roomID).Child("General").Child("Player2-UserName").SetValueAsync(username);
            return;
        }
    }

    public void AddRoomListToDropdown(TMPro.TMP_Dropdown dropdown) 
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

    public void StartGame() 
    {
        numberCreator.CreateNumber();
        uiManager.ShowGamePanelBridge();
    }

	public void SetSecretNumber(int currentNumber) 
    {
        roomReference.Child(roomID).Child("Progression").Child("SecretNumber").SetValueAsync(currentNumber);
    }

    public int GetSecretNumber() 
    {
        return int.Parse(roomReference.Child(roomID).Child("SecretNumber").GetValueAsync().ToString());
    }

    public void Estimate(int estimation) 
    {
        int secretNumber = GetSecretNumber();

        if (estimation == secretNumber)
        {
            Debug.Log("Kazandın!");
        }
        else
        {
            Listen();
        }
    }

    void Listen() 
    {
        roomReference.Child(roomID).Child("Progression").Child("LastEstimation").ValueChanged += GetEstimation;
    }

    public void GetEstimation(object sender, ValueChangedEventArgs args) 
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Değişiklik algılandı");

        DataSnapshot snapshot = args.Snapshot;

        SetEstimation(snapshot.Value.ToString());
       /* if (snapshot.HasChild("LastEstimation"))
        {
            Debug.Log(snapshot.Child("LastEstimation").Value);
        }
        */
    }
    void SetEstimation(string lastEstimation)
    {
        uiManager.ShowEstimation(lastEstimation);
    }

	#endregion
}
