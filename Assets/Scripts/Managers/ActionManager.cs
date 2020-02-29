using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : Singleton<ActionManager>
{
    // Prepare Game
    public UnityAction QuickGame;

    // Authentication
    public UnityAction<SignUpStruct> SignUpWithEmailPassword;
    public UnityAction<string, string> SignInWithEmailPassword;
    public UnityAction<string> ResetPasswordWithMail;
    public UnityAction DeleteUser;
    public UnityAction SignOut;

    // User
    public UnityAction<string,string> CreatUserProfile;
    public UnityAction<string, string, string, object> UpdateUserData;
    public UnityAction CallGetCurrentUserProfile;
    public UnityAction DeleteUserProfile;

    // Game
    public UnityAction<string> ShowWhoseTurn;
    public UnityAction<string> ShowLastEstimation;
    public UnityAction<int> SendEstimation;
    public UnityAction<int> ControlAnswer;

    public UnityAction CreateSecretNumber;

    // Panels
    public UnityAction ShowMenuPanel;
    public UnityAction ShowSignUpPanel;
    public UnityAction ShowSignInPanel;
    public UnityAction ShowUserProfilePanel;
}
