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

    // Database References
    DatabaseReference userReference;
    DatabaseReference roomReference;

    // Script References
    UIManager uiManager;

    void Start()
    {
        FireBaseStart();
        uiManager = FindObjectOfType<UIManager>();
        /*UpdateUserData("gold", "550");*/
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
        //userID = auth.CurrentUser.UserId; 

        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;

        // Database References Declare
        userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{auth.CurrentUser.UserId}");
        roomReference = FirebaseDatabase.DefaultInstance.GetReference($"Rooms/RoomID");




        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);



        //SetSecretNumber(4562, "-LxZlLsFAZ4iQlVecCgr");

        // CreateRoom("Second Wind", "annen", 3);
        //GetRoomList();


        //userID = auth.CurrentUser.UserId;  bunu unutma lazım
        // auth.StateChanged += AuthStateChanged;
        // auth.IdTokenChanged += IdTokenChanged;


        //  AuthStateChanged(this, null);

    }

    #region Authentication
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

            CreateUser(newUser.UserId, username);


        });
    }
    #endregion

    #region User

    public void CreateUser(string userId, string username)
    {
        Debug.Log("Kullanıcı bilgileri kaydedildi");
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");

        userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userId}");

        UserGeneral userGenerals = new UserGeneral
            (
            username,
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            System.DateTime.Now.ToString("dd/MM/yyyy"),
            "Türkiye",
            "Türkçe",
            true
            );

        string generalJson = JsonUtility.ToJson(userGenerals);
        Debug.Log(generalJson);
        userReference.Child("General").SetRawJsonValueAsync(generalJson);

        UserProgression userProgressions = new UserProgression
            (
            0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 10, 100, 10, 5
            );

        string progressionJson = JsonUtility.ToJson(userProgressions);
        Debug.Log(progressionJson);
        userReference.Child("Progression").SetRawJsonValueAsync(progressionJson);
    }

    public void GetUsers()
    {
        //Debug.Log(reference.Reference);
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

    public void GetUserData(string key, object value)
    {
        //userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}/{key}");
        userReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                object _value = snapshot.Child(key).Value;

                Debug.Log($"Key = {key}  Value = {_value}");
                //    UpdateUserData(key, value);
            }
        }
        );
    }
    public void UpdateUserData(string key, object value)
    {
        // Debug.Log($"userId = {userID}  key = {key},value = {value} son hal");

        //userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}/{key}");
        userReference.Child(key).SetValueAsync(value);
    }

    #endregion

    #region Room

    public void CreateRoom(string roomName, string roomPassword, int scoreLimit)
    {
        Debug.Log(userReference.Child("General").Child("username").Key);

        userID = auth.CurrentUser.UserId;
        string username = userReference.Child(userID).Child("Username").GetValueAsync().ToString();

        roomID = roomReference.Push().Key;

        Dictionary<string, object> roomDictionary = new Dictionary<string, object>
        {
            ["RoomID"] = roomID,
            ["RoomName"] = roomName,
            ["RoomPassword"] = roomPassword,
            ["Player1-ID"] = userID,
            ["Player2-ID"] = "",
            ["ScoreLimit"] = scoreLimit,
            ["PlayerLimit"] = 0,
            ["Player1-Username"] = username,
            ["Player2-Username"] = "",
            ["SecretNumber"] = 0,
            ["SecretNumberMaxValue"] = 0,
            ["LastEstimation"] = 0,
            ["WhoseTurn"] = ""
        };
        
        roomReference.Child(roomID).UpdateChildrenAsync(roomDictionary);
    }

    public void EnterTheRoom(string roomId)
    {
        userID = auth.CurrentUser.UserId;
        roomID = roomId;
        string username = userReference.Child(userID).Child("Username").GetValueAsync().ToString();

        roomReference.Child(roomId).Child("Player2-ID").SetValueAsync(userID);
        roomReference.Child(roomId).Child("Player2-UserName").SetValueAsync(username);
    }

    public void GetRoomList(TMPro.TMP_Dropdown dropdown) 
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

    #region Game

	public void SetSecretNumber(int currentNumber) 
    {
        roomReference.Child(roomID).Child("SecretNumber").SetValueAsync(currentNumber);
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

        }
    }
 
    public void RoomListener(object sender, ValueChangedEventArgs args) 
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Değişiklik algılandı");

        DataSnapshot snapshot = args.Snapshot;

        if (snapshot.HasChild("LastEstimation"))
        {
            Debug.Log(snapshot.Child("LastEstimation").Value);
        }

    }

	#endregion
}
