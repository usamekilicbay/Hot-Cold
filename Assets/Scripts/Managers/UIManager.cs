using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("<PANELS>")]
    public GameObject pnl_Menu;
    public GameObject pnl_Lobby;
    public GameObject pnl_Game;
    public GameObject pnl_Settings;
    public GameObject pnl_Store;
    public GameObject pnl_SignUp;
    public GameObject pnl_SignIn;
    public GameObject pnl_Lose;
    public GameObject pnl_Win;

    [Space(10)]

    [Header("Panel / Menu")]
    [SerializeField] TextMeshProUGUI txt_Menu_Header;
    [SerializeField] Button btn_Menu_Play;
    [SerializeField] Button btn_Menu_Settings;
    [SerializeField] Button btn_Menu_RateUs;
    [SerializeField] Button btn_Menu_Rank;

    [Space(5)]

    [Header("Panel / Lobby")]
    [SerializeField] TextMeshProUGUI txt_Lobby_Header;
    [SerializeField] Button btn_Lobby_Home;
    [SerializeField] Button btn_Lobby_Settings;
    [SerializeField] Button btn_Lobby_Search_Game;

    [Space(5)]

    [Header("Panel / Game")]
    [SerializeField] TextMeshProUGUI txt_Game_Header;
    [SerializeField] Button btn_Game_Home;
    [SerializeField] Button btn_Game_Settings;
    [SerializeField] Button btn_Game_Answer;


    [Space(5)]

    [Header("Panel / Settings")]
    [SerializeField] TextMeshProUGUI txt_Settings_Header;
    [SerializeField] Button btn_Settings_Home;
    [SerializeField] Button btn_Settings_Vibration_On;
    [SerializeField] Button btn_Settings_Vibration_Off;
    [SerializeField] Slider sld_Settings_Music_Volume;
    [SerializeField] Slider sld_Settings_SFX_Volume;

    [Space(5)]

    [Header("Panel / Store")]
    [SerializeField] TextMeshProUGUI txt_Store_Header;
    [SerializeField] Button btn_Store_Home;

    [Space(5)]

    [Header("Panel / SignUp")]
    [SerializeField] TextMeshProUGUI txt_SignUp_Header;
    [SerializeField] TextMeshProUGUI txt_SignUp_UserName;
    [SerializeField] TextMeshProUGUI txt_SignUp_EMail;
    [SerializeField] TextMeshProUGUI txt_SignUp_Password;
    [SerializeField] Button btn_SignUp_Home;
    [SerializeField] Button btn_SignUp_Send;

    [Space(5)]

    [Header("Panel / SignIn")]
    [SerializeField] TextMeshProUGUI txt_SignIn_Header;
    [SerializeField] TextMeshProUGUI txt_SignIn_UserName;
    [SerializeField] TextMeshProUGUI txt_SignIn_EMail;
    [SerializeField] TextMeshProUGUI txt_SignIn_Password;
    [SerializeField] Button btn_SignIn_Home;
    [SerializeField] Button btn_SignIn_Send;

    [Space(5)]

    [Header("Panel / Lose")]
    [SerializeField] TextMeshProUGUI txt_Lose_Header;
    [SerializeField] TextMeshProUGUI txt_Lose_Outcome;
    [SerializeField] TextMeshProUGUI txt_Lose_PlayerScore;
    [SerializeField] TextMeshProUGUI txt_Lose_RivalScore;
    [SerializeField] Button btn_Lose_Home;
    [SerializeField] Button btn_Lose_Play;

    [Space(5)]

    [Header("Panel / Win")]
    [SerializeField] TextMeshProUGUI txt_Win_Header;
    [SerializeField] TextMeshProUGUI txt_Win_Outcome;
    [SerializeField] TextMeshProUGUI txt_Win_PlayerScore;
    [SerializeField] TextMeshProUGUI txt_Win_RivalScore;
    [SerializeField] Button btn_Win_Home;
    [SerializeField] Button btn_Win_Play;


    private void Start()
    {
        /*// Menu Panel Add Click Listener
        btn_Menu_Play.onClick.AddListener(ShowLobbyPanel);
        btn_Menu_Settings.onClick.AddListener(ShowSettingsPanel);
        btn_Menu_RateUs.onClick.AddListener(RateUs);
        btn_Menu_Rank.onClick.AddListener(GetRank);


        // Game Panel Add Click Listener
        btn_Game_Home.onClick.AddListener(ShowMenuPanel);
        btn_Game_Settings.onClick.AddListener(ShowSettingsPanel);
        btn_Game_Answer.onClick.AddListener(Answer);


        // Settings Panel Add Click Listener
        btn_Settings_Home.onClick.AddListener(ShowMenuPanel);
        btn_Settings_Vibration_Off.onClick.AddListener(VibrationOff);
        btn_Settings_Vibration_On.onClick.AddListener(VibrationOn);
        sld_Settings_Music_Volume.onValueChanged.AddListener(SetMusicVolume);
        sld_Settings_Music_Volume.onValueChanged.AddListener(SetSfxVolume);*/


        // SignUp Panel Add Click Listener
        btn_SignUp_Home.onClick.AddListener(ShowMenuPanel);
        btn_SignUp_Send.onClick.AddListener(SendSignUp);
        
        
        // SignIn Panel Add Click Listener
        btn_SignIn_Home.onClick.AddListener(ShowMenuPanel);
        btn_SignIn_Send.onClick.AddListener(SendSignIn);


       /* btn_Lobby_Home.onClick.AddListener(ShowMenuPanel);
        btn_Store_Home.onClick.AddListener(ShowMenuPanel);
        btn_Lose_Home.onClick.AddListener(ShowMenuPanel);
        btn_Win_Home.onClick.AddListener(ShowMenuPanel);*/


    }

    #region Firebase

    private void SendSignUp() { AuthManager.SignUp(txt_SignUp_EMail.text, txt_SignUp_Password.text); }
    private void SendSignIn() { AuthManager.SignIn(txt_SignIn_EMail.text, txt_SignIn_Password.text); }
    private void SearchGame() { }
    private void Answer() { }

    #endregion

    #region Media

    private void VibrationOn() { }
    private void VibrationOff() { }

    private void SetMusicVolume(float musicVolume) { }
    private void SetSfxVolume(float sfxVolume) { }

    #endregion

    #region Social

    private void RateUs() { }

    private void GetRank() { }
    #endregion

    #region Panels

    private enum Panels
    {
        Menu,
        Lobby,
        Game,
        Settings,
        Store,
        SignUp,
        SignIn,
        Lose,
        Win
    }

    private void ShowMenuPanel() { PanelChanger(Panels.Menu); }
    private void ShowLobbyPanel() { PanelChanger(Panels.Lobby); }
    private void ShowGamePanel() { PanelChanger(Panels.Game); }
    private void ShowSettingsPanel() { PanelChanger(Panels.Settings); }
    private void ShowStorePanel() { PanelChanger(Panels.Store); }
    private void ShowSignUpPanel() { PanelChanger(Panels.SignUp); }
    private void ShowSignInPanel() { PanelChanger(Panels.SignIn); }
    private void ShowLosePanel() { PanelChanger(Panels.Lose); }
    private void ShowWinPanel() { PanelChanger(Panels.Win); }

    private void PanelChanger(Panels panels)
    {
        pnl_Menu.SetActive(panels == Panels.Menu);
        pnl_Lobby.SetActive(panels == Panels.Lobby);
        pnl_Game.SetActive(panels == Panels.Game);
        pnl_Settings.SetActive(panels == Panels.Settings);
        pnl_Store.SetActive(panels == Panels.Store);
        pnl_SignUp.SetActive(panels == Panels.SignUp);
        pnl_SignIn.SetActive(panels == Panels.SignIn);
        pnl_Lose.SetActive(panels == Panels.Lose);
        pnl_Win.SetActive(panels == Panels.Win);
    }
    
    #endregion
}