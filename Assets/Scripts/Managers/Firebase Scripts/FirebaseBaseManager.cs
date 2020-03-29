using ConstantKeeper;
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

public class FirebaseBaseManager : Singleton<FirebaseBaseManager>
{
    //Firebase temel ayarlarımız
    public static FirebaseAuth auth;
    public static FirebaseUser user;
    protected Dictionary<string, FirebaseUser> userByauth = new Dictionary<string, FirebaseUser>();
    DependencyStatus DependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    private bool fetchingToken = false;



    // Database References
    public static DatabaseReference userReference;
    public static DatabaseReference roomReference;
    protected static DatabaseReference settingsReference;

    // User References
    string displayName;
    string emailAddress;

    private void Start()
    {
        FireBaseStart();
        auth.StateChanged += AuthStateChanged;
    }

    private void OnDisable()
    {
       
    }

    private void OnApplicationQuit()
    {
        //auth.StateChanged -= AuthStateChanged;
    }

    private void FireBaseStart()
    {
        DependencyStatus = FirebaseApp.CheckDependencies();

        if (DependencyStatus == DependencyStatus.Available)
        {
            InitalizeFirebase();
            return;
        }

        if (DependencyStatus != DependencyStatus.Available)
        {
            FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
                {
                    DependencyStatus = FirebaseApp.CheckDependencies();

                    if (DependencyStatus == DependencyStatus.Available)
                    {
                        InitalizeFirebase();
                    }
                    else { Debug.Log("Hata oluştu!"); }
                });
        }
        else
        {
            Debug.Log("Bağlantı ayarlanıyor");
            InitalizeFirebase();
        }

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
        FirebaseApp app = FirebaseApp.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        
        Debug.Log("Initial firebase " + auth);

       /* if (auth == null)
        {
            Debug.Log("Auth null");
        }

    */
        app.SetEditorDatabaseUrl("https://hot-cold-guess-game.firebaseio.com/");
        if (app.Options.DatabaseUrl != null) app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);


        // Database Reference Declare
        if (auth.CurrentUser != null)
        {
            SetUserReference();
           // ActionManager.Instance.CallGetCurrentUserProfile();
            //ActionManager.Instance.ShowUserProfilePanel();
            Debug.Log(auth.CurrentUser.DisplayName);
        }
        else
        {
            SetUserFirstReference();
            UIManager.Instance.ShowSignInPanel();
        }
        
        // AuthStateChanged(this, null);
    }

    

    void AuthStateChanged(object sender, EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                ActionManager.Instance.ShowSignInPanel();
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                
                Debug.Log("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                ActionManager.Instance.CallGetCurrentUserProfile();
            }
        }
        else
        {
            Debug.Log("Kullanıcı yok galiba");
        }
    }
    
    protected void SetSettingsReference() 
    {
        settingsReference = FirebaseDatabase.DefaultInstance.GetReference($"{GameSettingsPaths.GameSettings}");
    }

    public static void SetUserFirstReference() 
    {
        FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}");
    }

    public static void SetUserReference()
    {
        userReference = FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}/{auth.CurrentUser.UserId}");
    }
}
