using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class FirebaseAuthenticationManager : FirebaseBaseManager
{
    private void OnEnable()
    {
        //ActionManager.Instance.SignUpWithEmailPassword += CallSignUpWithEmailPassword;
        //ActionManager.Instance.SignInWithEmailPassword += CallSignInWithEmailPassword;
        //ActionManager.Instance.ResetPasswordWithMail += CallResetPasswordWithMail;
        //ActionManager.Instance.SignOut += SignOut;
        //ActionManager.Instance.DeleteUser += DeleteUser;
    }

    private void OnDisable()
    {
        ActionManager.Instance.SignUpWithEmailPassword -= CallSignUpWithEmailPassword;
        ActionManager.Instance.SignInWithEmailPassword -= CallSignInWithEmailPassword;
        ActionManager.Instance.ResetPasswordWithMail -= CallResetPasswordWithMail;
        ActionManager.Instance.SignOut -= SignOut;
        ActionManager.Instance.DeleteUser -= DeleteUser;
    }

    private void OnApplicationQuit()
    {
        ActionManager.Instance.SignUpWithEmailPassword -= CallSignUpWithEmailPassword;
        ActionManager.Instance.SignInWithEmailPassword -= CallSignInWithEmailPassword;
        ActionManager.Instance.ResetPasswordWithMail -= CallResetPasswordWithMail;
        ActionManager.Instance.SignOut -= SignOut;
        ActionManager.Instance.DeleteUser -= DeleteUser;
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

    

	#region Sign Up

	private void CallSignUpWithEmailPassword(SignUpStruct sign)//string _username, string _email, string _password, string _confirmPassword, string _language) 
    {

        if (sign.Password.Equals(sign.ConfirmPassword))
        {
            StartCoroutine(SignUpEmailPassword(sign.Username, sign.Email, sign.Password, sign.Language));
        }
        else
        {
            Debug.Log("Eşleşmeyen şifre!");
        }
    }

    private IEnumerator SignUpEmailPassword(string _username, string _email, string _password, string _language)
    {
        Task task = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            //LogTaskCompletion(task, "Giriş işlemi iptal edildi");
            Debug.LogWarning(ConstantKeeper.Authentications.SignUp + ConstantKeeper.Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            // LogTaskCompletion(task, "Kayıt işlemi başarısız oldu!");
            Debug.LogError(ConstantKeeper.Authentications.SignUp + ConstantKeeper.Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            // LogTaskCompletion(task, "Kayıt işlemi başarıyla tamamlandı!");
            Debug.Log(ConstantKeeper.Authentications.SignUp + ConstantKeeper.Debugs.IsCompleted);
            SetUserReference();

            ActionManager.Instance.CreatUserProfile(_username, _language);
            ActionManager.Instance.CallGetCurrentUserProfile();
        }
    }

	#endregion

	#region Sign In

	private void CallSignInWithEmailPassword(string _email,string _password) 
    {
        StartCoroutine(SignInEmailPassword(_email, _password));
    }

    private IEnumerator SignInEmailPassword(string _email, string _password)
    {
        Task task = auth.SignInWithEmailAndPasswordAsync(_email, _password);

       // string log = 

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(ConstantKeeper.Authentications.SignIn + ConstantKeeper.Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(ConstantKeeper.Authentications.SignIn + ConstantKeeper.Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            SetUserReference();

            Debug.Log(ConstantKeeper.Authentications.SignIn + ConstantKeeper.Debugs.IsCompleted);
            ActionManager.Instance.CallGetCurrentUserProfile();
        }
    }

    #endregion

    #region Reset Password

    private void CallResetPasswordWithMail(string _email) 
    {
        StartCoroutine(ResetPassword(_email));
    }

    private IEnumerator ResetPassword(string _email)
    {
        Task task = auth.SendPasswordResetEmailAsync(_email);

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(ConstantKeeper.Authentications.ResetPassword + ConstantKeeper.Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(ConstantKeeper.Authentications.ResetPassword + ConstantKeeper.Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            Debug.Log(ConstantKeeper.Authentications.ResetPassword + ConstantKeeper.Debugs.IsCompleted);
        }
    }

    #endregion

    #region Sign Out

    private void SignOut() 
    {
        auth.SignOut();
        
        Debug.Log(ConstantKeeper.Authentications.SignOut + ConstantKeeper.Debugs.IsCompleted);
        ActionManager.Instance.ShowSignInPanel();
    }

    #endregion

    #region Delete User

    private void DeleteUser()
    {
        auth.CurrentUser.DeleteAsync();
        //FirebaseUser user = auth.CurrentUser;
        /*auth.CurrentUser.DeleteAsync().ContinueWith(task =>
           {
               Debug.Log("Kullanıcı silme işlemi deneniyor");

               if (task.IsCanceled)
               {
                   Debug.Log("Kullanıcı silme işlemi iptal edildi.");
               }
               else if (task.IsFaulted)
               {
                   Debug.Log("Kullanıcı silme işlemi başarısız oldu.");
               }
               else if (task.IsCompleted)
               {
                   Debug.Log("Kullanıcı silme işlemi başarıyla tamamlandı.");
               }
           });*/

    }

	#endregion

	/*Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
    auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
        if (task.IsCanceled)
        {
            Debug.LogError("SignInWithCredentialAsync was canceled.");
            return;
        }
        if (task.IsFaulted)
        {
            Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
            return;
        }

        Firebase.Auth.FirebaseUser newUser = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.DisplayName, newUser.UserId);
    });*/

}
