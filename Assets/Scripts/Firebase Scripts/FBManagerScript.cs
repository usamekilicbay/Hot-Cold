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



public class FBManagerScript : Singleton<FBManagerScript>
{  
    //Firebase temel ayarlarımız
    protected Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseAuth digerauth;
    protected Dictionary<string, Firebase.Auth.FirebaseUser> userByauth = new Dictionary<string, FirebaseUser>();
    private Firebase.AppOptions digerAuthOption;
    Firebase.DependencyStatus DepStatus = Firebase.DependencyStatus.UnavailableOther;
    private bool fetchingToken = false;

    int GirisSayisi = 1;

    // Start is called before the first frame update
    void Start()
    {

        GirisSayisi = PlayerPrefs.GetInt("GirisSayisi", 1) + 1;
        PlayerPrefs.SetInt("GirisSayisi", GirisSayisi);  

        FireBaseBaslat();
    }


    public void FireBaseBaslat()
    {
        DepStatus = Firebase.FirebaseApp.CheckDependencies();
        if (DepStatus != Firebase.DependencyStatus.Available)
        {
            Firebase.FirebaseApp.FixDependenciesAsync().ContinueWith
                (task =>
                {
                    DepStatus = Firebase.FirebaseApp.CheckDependencies();
                    if (DepStatus == Firebase.DependencyStatus.Available) { InitalizeFirebase(); }
                    else
                    {
                        Debug.Log("Hata oluştu!");

                    }
                });
        }
        else { InitalizeFirebase(); }

        if (auth.CurrentUser == null)
        {
            Debug.Log("User Yok!");
            StartCoroutine("AnonimGiris");  //kullanıcı ilk defa giriş yaptı, user kaydedilsin
            return; //İlk girişse burdan aşağısı çalışmasın
        }

        Debug.Log("User tekrar giriş yaptı");
        StartCoroutine("TekrarGiris");
    }


    void InitalizeFirebase()
    {
        FirebaseApp app = Firebase.FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);

        //Firebase kullanıcı oturum açma isteği
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        auth.IdTokenChanged += IdTokenChanged;


        AuthStateChanged(this, null);

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        Firebase.Auth.FirebaseUser user = null;
        if (senderAuth == auth && senderAuth.CurrentUser != user)
        {
            bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Çıkış yapan kullanıcı:" + user.UserId);
                StartCoroutine(AnonimGiris());
            }
            user = senderAuth.CurrentUser;
            userByauth[senderAuth.App.Name] = user;
            if (signedIn)
            {
                Debug.Log("Giriş yapan kullanıcı:" + user.UserId);

                KullaniciKaydet(auth.CurrentUser.UserId.ToString());
            }
        }
    }



    void IdTokenChanged(object sender, System.EventArgs eventArgs)
    {
        Firebase.Auth.FirebaseAuth senderAuth = sender as Firebase.Auth.FirebaseAuth;
        if (senderAuth == auth && senderAuth.CurrentUser != null && !fetchingToken)
        {
            senderAuth.CurrentUser.TokenAsync(false).ContinueWith
                (
               task => Debug.Log(System.String.Format("Token[0:8] = {0}", task.Result.Substring(0, 8)))
               );

        }
    }


    //Kullanıcımız Anonim giriş yaptı
    IEnumerator AnonimGiris()
    {
        Debug.Log("AnonimGiris olarak ilk giriş yapıldı...");
        auth.SignInAnonymouslyAsync().ContinueWith(SigninAnonim);
        yield return new WaitForSeconds(2.0f);
    }

    private void SigninAnonim(Task<Firebase.Auth.FirebaseUser> authTask)
    {
        if (LogTaskCompletion(authTask, "Giriş Yapıldı")) ;
    }

    //Kullanıcımız oyuna yeniden girdi
    IEnumerator TekrarGiris()
    {
        Debug.Log("Tekrar Giriş yapıldı....");
        KullaniciKaydet(auth.CurrentUser.UserId.ToString());
        yield return new WaitForSeconds(2.0f);

    }

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

    public void KullaniciKaydet(string userId) //Dikkat et kullanıcının, girişteki UserId si geliyor
    {
        Debug.Log("User Kayda Girdik");
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        DatabaseReference refer = FirebaseDatabase.DefaultInstance.RootReference;  // ana yola bağlı tablo
        User user = new User();
        string json = JsonUtility.ToJson(user);
        Debug.Log("json açılımı" + json);
        refer.Child("Users").Child(userId).SetRawJsonValueAsync(json);

        PlayerPrefs.SetString("KullaniciID", userId);
    }


    public class User
    {
        public int GirisSayisi;
        public string UserName;
        public string Ulke;
        public string IlkGirisTarihi;  //
        public string SonGirisTarihi;


        public User()
        {


            this.GirisSayisi = PlayerPrefs.GetInt("GirisSayisi", 1);
            this.UserName = PlayerPrefs.GetString("UserName", "Anonim Kullanıcı");
            this.Ulke = PlayerPrefs.GetString("Ulke", "Dünyalı");
            this.IlkGirisTarihi = PlayerPrefs.GetString("IlkGirisTarihi", System.DateTime.Now.ToString("dd-MM-yyyy"));
            this.SonGirisTarihi = System.DateTime.Now.ToString("dd-MM-yyyy");
        }

    }
}
