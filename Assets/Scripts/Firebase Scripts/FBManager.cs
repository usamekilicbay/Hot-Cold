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
    private Firebase.Auth.FirebaseAuth digerauth;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByauth = new Dictionary<string, FirebaseUser>();
    private Firebase.AppOptions digerAuthOption;
    Firebase.DependencyStatus DepStatus = Firebase.DependencyStatus.UnavailableOther;
    private bool fetchingToken = false;
    private string userID;

    void Start() { FireBaseStart(); /*UpdateUserData("gold", "550");*/ }


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

        /*if (auth.CurrentUser == null)
        {
            Debug.Log("User Yok!");
          //StartCoroutine(SignUpAnonym());  //kullanıcı ilk defa giriş yaptı, user kaydedilsin
            return; //İlk girişse burdan aşağısı çalışmasın
        }
        */
        Debug.Log("Bağlantı Sağlandı");
        // StartCoroutine(SignInAgain());
    }


    void InitalizeFirebase()
    {
        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

        //Firebase kullanıcı oturum açma isteği
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

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

    public void CreateUser(string userId, string username)
    {
        Debug.Log(username);
        Debug.Log("Kullanıcı bilgileri kaydedildi");
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userId}");

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
        reference.Child("General").SetRawJsonValueAsync(generalJson);

        UserProgression userProgressions = new UserProgression
            (
            0,0,0,1,0,0,0,0,0,0,10,100,10,5
            );
       
        string progressionJson = JsonUtility.ToJson(userProgressions);
        Debug.Log(progressionJson);
        reference.Child("Progression").SetRawJsonValueAsync(progressionJson);
    }

    public void GetUsers()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}");
        //Debug.Log(reference.Reference);
        reference.GetValueAsync().ContinueWith(task =>
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
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}/{key}");
        reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                object _value = snapshot.Value;

                Debug.Log($"Key = {key}  Value = {_value}");
                UpdateUserData(key, value);

            }
        }
        );
    }
    public void UpdateUserData(string key, object value)
    {
       // Debug.Log($"userId = {userID}  key = {key},value = {value} son hal");

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{userID}/{key}");
        //reference.UpdateChildrenAsync(dictionary);
        reference.SetValueAsync(value);
    }


}
