using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase.Auth;

public class AuthManager : Singleton<AuthManager>
{
    /*FirebaseAuth firebaseAuth;

    FirebaseManager firebaseManager;

    void Start() 
    {
        firebaseAuth = FirebaseAuth.DefaultInstance;

        firebaseManager = FirebaseManager.Instance;
    }

    public void SignUp(string username, string email, string password)
    {
        firebaseAuth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
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

             //firebaseManager.CreateUser(username);
             FirebaseUser newUser = task.Result;
           
             
         });
    }

    public void SignIn(string email, string password)
    {
        firebaseAuth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Giriş işlemi iptal edildi");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Giriş işlemi başarısız oldu");
                return;
            }

            FirebaseUser user = task.Result;
            Debug.LogFormat("Giriş işlemi başarı ile tamamlandı {0} {1}",
                user.DisplayName,
                user.UserId
                );
        });
    }

    public void ResetPassword(string email)
    {
        firebaseAuth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Şifre sıfırlama işlemi iptal edildi...");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Şifre sıfırlama işlemi başarısız oldu!");
                return;
            }

            Debug.Log("Şifre sıfırlama işlemi başarı ile tamamlandı!");
        });
    }*/
}
