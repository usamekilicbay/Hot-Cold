using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UserProfile : Singleton<UserProfile>
{
    [Header("User Profile")]
    [SerializeField] TextMeshProUGUI txt_User_Level;
    [SerializeField] TextMeshProUGUI txt_User_Cup;
    [SerializeField] TextMeshProUGUI txt_User_Rank;
    [SerializeField] TextMeshProUGUI txt_User_Username;
    [SerializeField] TextMeshProUGUI txt_User_SignUpDate;
    [SerializeField] TextMeshProUGUI txt_User_LastSeen;
    [SerializeField] TextMeshProUGUI txt_User_TotalPlayTime;
    [SerializeField] TextMeshProUGUI txt_User_TotalMatches;
    [SerializeField] TextMeshProUGUI txt_User_CompletedMathces;
    [SerializeField] TextMeshProUGUI txt_User_AbandonedMathces;
    [SerializeField] TextMeshProUGUI txt_User_Wins;
    [SerializeField] TextMeshProUGUI txt_User_Losses;
    [SerializeField] TextMeshProUGUI txt_User_WinningStreak;
    [SerializeField] Button btn_User_Home;
    [SerializeField] Button btn_User_SignOut;


    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    private void OnClickAddListener()
    {
        btn_User_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_User_SignOut.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
    }

    public void GetOwnInfos(Dictionary<string, object> dictionary)
    {
        txt_User_Level.SetText(dictionary["Level"].ToString());
        txt_User_Cup.SetText(dictionary["Cup"].ToString());
        txt_User_Rank.SetText(dictionary["Rank"].ToString());
        txt_User_Username.SetText(dictionary["Username"].ToString());
        txt_User_SignUpDate.SetText(dictionary["SignUpDate"].ToString());
        if (bool.Parse(dictionary["SignInStatus"].ToString())) { txt_User_LastSeen.SetText("Online"); }
        else { txt_User_LastSeen.SetText(dictionary["LastSeen"].ToString()); }
        txt_User_TotalPlayTime.SetText(dictionary["TotalPlayTime"].ToString());
        txt_User_TotalMatches.SetText(dictionary["TotalMatches"].ToString());
        txt_User_CompletedMathces.SetText(dictionary["CompletedMatches"].ToString());
        txt_User_AbandonedMathces.SetText(dictionary["AbandonedMatches"].ToString());
        txt_User_Wins.SetText(dictionary["Wins"].ToString());
        txt_User_Losses.SetText(dictionary["Losses"].ToString());
        txt_User_WinningStreak.SetText(dictionary["WinningStreak"].ToString());

        UIManager.Instance.ShowUserProfilePanel();
    }
}
