using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionManager : Singleton<ActionManager>
{


    public UnityAction QuickGame;

    public UnityAction<string, string, string> SignUpEmailPassword;
    public UnityAction<string, string> SignInEmailPassword;
    
    public UnityAction UserProfile;
    
    
    
    
    
    
    
    
    
    
    public UnityAction<UIManager.Panels> showMainMenuPanelTrigger;
    public UnityAction<UIManager.Panels> showGamePanelTrigger;
    public UnityAction<UIManager.Panels> showSettingsPanelTrigger;
    public UnityAction<UIManager.Panels> showStorePanelTrigger;
    public UnityAction<UIManager.Panels> showSignInPanelTrigger;
    public UnityAction<UIManager.Panels> showSignUpPanelTrigger;
    public UnityAction<UIManager.Panels> showResetPasswordPanelTrigger;
    public UnityAction<UIManager.Panels> showUserProfilePanelTrigger;
}
