using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class FirebaseAuthenticationManager : FBManager
{

    private void OnEnable()
    {
        ActionManager.Instance.SignUpEmailPassword += SignUpEmailPassword;
        ActionManager.Instance.SignInEmailPassword += SignInEmailPassword;
    }

    private void OnDisable()
    {
        ActionManager.Instance.SignUpEmailPassword -= SignUpEmailPassword;
        ActionManager.Instance.SignInEmailPassword -= SignInEmailPassword;
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        FirebaseAuth senderAuth = sender as FirebaseAuth;
        FirebaseUser user = null;
        if (senderAuth == auth && senderAuth.CurrentUser != user)
        {
            bool signedIn = user != senderAuth.CurrentUser && senderAuth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                Debug.Log("Çıkış yapan kullanıcı:" + user.UserId);
                UIManager.Instance.ShowSignInPanel();
            }
            user = senderAuth.CurrentUser;
            userByauth[senderAuth.App.Name] = user;
            if (signedIn)
            {
                Debug.Log("Giriş yapan kullanıcı:" + user.UserId);
                UIManager.Instance.ShowMenuPanel();
            }
        }
    }



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

    /*private void SignInAnonym(Task<Firebase.Auth.FirebaseUser> authTask)
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
                //newUser. BUNU İNCELE
                SetUserReference();

               // CreateUser(username);
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
                UIManager.Instance.ShowMenuPanel();
            }
        });
    }


   /* private void GetUserID()
    {
        userID = auth.CurrentUser.UserId;
        Debug.Log("User Id " + userID);
        SetUserReference();
    }*/

    private void SetUserReference()
    {
        userReference = FirebaseDatabase.DefaultInstance.GetReference($"Users/UserID/{auth.CurrentUser.UserId}");

        //StartCoroutine(GetCurrentUserProfile());
    }
}
