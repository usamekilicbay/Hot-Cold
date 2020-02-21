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
    protected DatabaseReference userReference;
    protected DatabaseReference roomReference;

    // Script References
    UIManager uiManager;
    NumberCreator numberCreator;

   
    private void OnEnable()
    {
        uiManager = UIManager.Instance;
       
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


        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);


        // Database Reference Declare
        if (auth.CurrentUser != null)
        {
            
            //StartCoroutine(GetCurrentUserProfile());
        }
       // else uiManager.ShowSignInPanel();


       


        // auth.StateChanged += AuthStateChanged;
        // auth.IdTokenChanged += IdTokenChanged;

          AuthStateChanged(this, null);

    }


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
                            JoinRoom(rooms.Key);
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
                Debug.Log("Yeni oda oluşturuluyor, oyuncu bekleniyor");
                CreateRoom();
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

            owner = true;

            StartGame();
        }
        else
        {
            Debug.Log("Zaten kurulmuş bir odanız var!");
        }
    }

    void JoinRoom(string roomsID) 
    {
        roomReference.Child(roomsID).Child("General").Child("P2-ID").SetValueAsync(userID);
        roomReference.Child(roomsID).Child("General").Child("P2-Username").SetValueAsync(CurrentUserProfileKeeper.Username);
    
        canPlay = false;
        roomID = roomsID;

        canStart = true;

        if (canStart)
        {
            owner = false;
            StartGame();
        }

        Debug.Log("Odaya giriş başarılı!");
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


    bool owner = true;

    private void StartGame() 
    {
        SetSecretNumber(NumberCreator.CreateNumber());
        StartCoroutine(GetRoomInfos());
        uiManager.ShowGamePanel();
    }

	private void SetSecretNumber(int currentNumber) 
    {
        roomReference.Child(roomID).Child("General").Child("SecretNumber").SetValueAsync(currentNumber);
    }

    private IEnumerator GetRoomInfos()
    {
        Task<DataSnapshot> task = roomReference.Child(roomID).Child("General").GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted || task.IsCanceled);

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

            if (owner)
            {
                CurrentRoomInfoKeeper.rivalID = snapshot.Child("P2-ID").Value.ToString();
                CurrentRoomInfoKeeper.rivalUsername = snapshot.Child("P2-Username").Value.ToString();
                Debug.Log("Ensar");
            }
            else
            {
                CurrentRoomInfoKeeper.rivalID = snapshot.Child("P1-ID").Value.ToString();
                CurrentRoomInfoKeeper.rivalUsername = snapshot.Child("P1-Username").Value.ToString();
                Debug.Log("Muhacir");
            }

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
