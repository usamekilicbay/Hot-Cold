using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UserProfileUI : MonoBehaviour
{
    [Header("User Profile")]
    [SerializeField] private TextMeshProUGUI txt_User_Level;
    [SerializeField] private TextMeshProUGUI txt_User_Cup;
    [SerializeField] private TextMeshProUGUI txt_User_Rank;
    [SerializeField] private TextMeshProUGUI txt_User_Username;
    [SerializeField] private TextMeshProUGUI txt_User_SignUpDate;
    [SerializeField] private TextMeshProUGUI txt_User_LastSeen;
    [SerializeField] private TextMeshProUGUI txt_User_TotalPlayTime;
    [SerializeField] private TextMeshProUGUI txt_User_TotalMatches;
    [SerializeField] private TextMeshProUGUI txt_User_CompletedMathces;
    [SerializeField] private TextMeshProUGUI txt_User_AbandonedMathces;
    [SerializeField] private TextMeshProUGUI txt_User_Wins;
    [SerializeField] private TextMeshProUGUI txt_User_Losses;
    [SerializeField] private TextMeshProUGUI txt_User_WinningStreak;
    [SerializeField] private Button btn_User_Home;
    [SerializeField] private Button btn_User_SignOut;


    private void Start()
    {
        OnClickAddListener();
       // ActionManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;

        GetCurrentUserProfile();
    }

    private void OnDisable()
    {
      //  ActionManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
    }

    private void OnClickAddListener()
    {
        btn_User_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_User_SignOut.onClick.AddListener(SignOut);
    }

    private void GetCurrentUserProfile()
    {
        txt_User_Username.SetText(CurrentUserProfileKeeper.Username);
        txt_User_SignUpDate.SetText(CurrentUserProfileKeeper.SignUpDate.ToString());

        if (bool.Parse(CurrentUserProfileKeeper.SignInStatus.ToString()))
        {
            txt_User_LastSeen.SetText("ONLINE");//LocalizationKeeper.Online);
        }
        else
        {
            txt_User_LastSeen.SetText(CurrentUserProfileKeeper.LastSeen.ToString());
        }
        Debug.Log(CurrentUserProfileKeeper.Cup);
        txt_User_Level.SetText(CurrentUserProfileKeeper.Level.ToString());
        txt_User_Cup.SetText(CurrentUserProfileKeeper.Cup.ToString());
        txt_User_Rank.SetText(CurrentUserProfileKeeper.Rank.ToString());
        txt_User_TotalPlayTime.SetText(CurrentUserProfileKeeper.TotalPlayTime.ToString());
        txt_User_TotalMatches.SetText(CurrentUserProfileKeeper.TotalMatches.ToString());
        txt_User_CompletedMathces.SetText(CurrentUserProfileKeeper.CompletedMatches.ToString());
        txt_User_AbandonedMathces.SetText(CurrentUserProfileKeeper.AbandonedMatches.ToString());
        txt_User_Wins.SetText(CurrentUserProfileKeeper.Wins.ToString());
        txt_User_Losses.SetText(CurrentUserProfileKeeper.Losses.ToString());
        txt_User_WinningStreak.SetText(CurrentUserProfileKeeper.WinningStreak.ToString());
    }

    private void SignOut() 
    {
       // ActionManager.Instance.DeleteUser();
        ActionManager.Instance.SignOut();
        //ActionManager.Instance.DeleteUserProfile();
    }
}
