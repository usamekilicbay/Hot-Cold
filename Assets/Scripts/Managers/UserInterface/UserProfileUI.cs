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
        Debug.Log("Bu sayılır mı ?");
        ActionManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;

        GetCurrentUserProfile(CurrentUserProfileKeeper.UserProfileSetter());
    }

    private void OnDisable()
    {
      //  ActionManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
    }

    private void OnClickAddListener()
    {
        btn_User_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_User_SignOut.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
    }

    public void GetCurrentUserProfile(Dictionary<string, object> _dictionary)
    {
        txt_User_Level.SetText(_dictionary["Level"].ToString());
        txt_User_Cup.SetText(_dictionary["Cup"].ToString());
        txt_User_Rank.SetText(_dictionary["Rank"].ToString());
        txt_User_Username.SetText(_dictionary["Username"].ToString());
        txt_User_SignUpDate.SetText(_dictionary["SignUpDate"].ToString());
        if (bool.Parse(_dictionary["SignInStatus"].ToString())) { txt_User_LastSeen.SetText("Online"); }
        else { txt_User_LastSeen.SetText(_dictionary["LastSeen"].ToString()); }
        txt_User_TotalPlayTime.SetText(_dictionary["TotalPlayTime"].ToString());
        txt_User_TotalMatches.SetText(_dictionary["TotalMatches"].ToString());
        txt_User_CompletedMathces.SetText(_dictionary["CompletedMatches"].ToString());
        txt_User_AbandonedMathces.SetText(_dictionary["AbandonedMatches"].ToString());
        txt_User_Wins.SetText(_dictionary["Wins"].ToString());
        txt_User_Losses.SetText(_dictionary["Losses"].ToString());
        txt_User_WinningStreak.SetText(_dictionary["WinningStreak"].ToString());

        //UIManager.Instance.ShowUserProfilePanel();
    }
}
