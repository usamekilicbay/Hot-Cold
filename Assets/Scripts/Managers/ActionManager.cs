using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : Singleton<ActionManager>
{
    
    public UnityAction QuickGame;

    public UnityAction<string, string, string> SignUpEmailPassword;
    public UnityAction<string, string> SignInEmailPassword;

    public UnityAction<Dictionary<string, object>> GetCurrentUserProfile;
    public UnityAction CallCurrentUserProfile;


    public UnityAction<string> ShowLastEstimation;
    public UnityAction<int> SendEstimation;
    public UnityAction<int> ControlAnswer;

    public UnityAction CreateSecretNumber;

   // public UnityAction<>
    
    
    
    
    public UnityAction<UIManager.Panels> showMainMenuPanelTrigger;
    public UnityAction<UIManager.Panels> showGamePanelTrigger;
    public UnityAction<UIManager.Panels> showSettingsPanelTrigger;
    public UnityAction<UIManager.Panels> showStorePanelTrigger;
    public UnityAction<UIManager.Panels> showSignInPanelTrigger;
    public UnityAction<UIManager.Panels> showSignUpPanelTrigger;
    public UnityAction<UIManager.Panels> showResetPasswordPanelTrigger;
    public UnityAction<UIManager.Panels> showUserProfilePanelTrigger;
}
