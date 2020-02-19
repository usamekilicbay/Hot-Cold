using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("<PANELS>")]
    public GameObject pnl_Menu;
    public GameObject pnl_Lobby;
    public GameObject pnl_CreateRoom;
    public GameObject pnl_Game;
    public GameObject pnl_Settings;
    public GameObject pnl_Store;
    public GameObject pnl_SignUp;
    public GameObject pnl_SignIn;
    public GameObject pnl_ResetPassword;
    public GameObject pnl_UserProfile;
    public GameObject pnl_Lose;
    public GameObject pnl_Win;

   
    [Space(5)]

    [Header("Panel / Lobby")]
    [SerializeField] TextMeshProUGUI txt_Lobby_Header;
    [SerializeField] TMP_Dropdown drpdwn_Lobby_RoomList;
    [SerializeField] Button btn_Lobby_Home;
    //[SerializeField] Button btn_Lobby_Settings;
    [SerializeField] Button btn_Lobby_Refresh;
    [SerializeField] Button btn_Lobby_Enter;
    [SerializeField] Button btn_Lobby_Create;
    //[SerializeField] Button btn_Lobby_Search_Game;


    [Space(5)]

    [Header("Panel / Create Room")]
    [SerializeField] TextMeshProUGUI txt_CreateRoom_Header;
    //[SerializeField] TMP_Dropdown drpdwn_Lobby_RoomList;
    [SerializeField] TextMeshProUGUI txt_CreateRoom_RoomName;
    [SerializeField] TextMeshProUGUI txt_CreateRoom_RoomPassword;
    [SerializeField] Button btn_CreateRoom_Home;
    [SerializeField] Button btn_CreateRoom_Create;
    //[SerializeField] Button btn_Lobby_Settings;
    //[SerializeField] Button btn_Lobby_Refresh;
    //[SerializeField] Button btn_CreateRoom_Enter;
    //[SerializeField] Button btn_Lobby_Search_Game;

   

    [Header("Panel / Store")]
    [SerializeField] TextMeshProUGUI txt_Store_Header;
    [SerializeField] Button btn_Store_Home;


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
        

        // Lobby Panel Add Click Listener
        btn_Lobby_Home.onClick.AddListener(ShowMenuPanel);
        btn_Lobby_Refresh.onClick.AddListener(RefreshRoomList);
       // btn_Lobby_Enter.onClick.AddListener(EnterRoom);
        btn_Lobby_Create.onClick.AddListener(ShowCreateRoomPanel);
       

        // Create Room Panel Add Click Listener
        btn_CreateRoom_Home.onClick.AddListener(ShowMenuPanel);
       // btn_CreateRoom_Create.onClick.AddListener(CreateRoom);


      
        /* 
         btn_Store_Home.onClick.AddListener(ShowMenuPanel);
         btn_Lose_Home.onClick.AddListener(ShowMenuPanel);
         btn_Win_Home.onClick.AddListener(ShowMenuPanel);*/

        // Script Specification
      
    }

    #region Firebase

    // Authentication
     
      
   // private void SendPassword() { authManager.ResetPassword(txt_SignIn_Email.text); }

    // Before Game Begin
   // private void CreateRoom() { fBManager.CreateRoom(txt_CreateRoom_RoomName.text, txt_CreateRoom_RoomPassword.text); }
    
   // private void LoadRoomList() { fBManager.AddRoomListToDropdown(drpdwn_Lobby_RoomList); }
    private void RefreshRoomList() { }
    //private void EnterRoom() { fBManager.EnterTheRoom(drpdwn_Lobby_RoomList.options[drpdwn_Lobby_RoomList.value].ToString()); }

    // While Game

  //  private void StartTheMatch() { fBManager. }
    private void Answer() { }
   


   
  
    #endregion


    #region Panels

    public enum Panels
    {
        Menu,
        Lobby,
        CreateRoom,
        Game,
        Settings,
        Store,
        SignUp,
        SignIn,
        ResetPassword,
        UserProfile,
        Lose,
        Win
    }


    public void ShowMenuPanel() { PanelChanger(Panels.Menu); }
    public void ShowLobbyPanel() { PanelChanger(Panels.Lobby); }
    public void ShowCreateRoomPanel() { PanelChanger(Panels.CreateRoom); }
    public void ShowGamePanel() { PanelChanger(Panels.Game); }
    public void ShowSettingsPanel() { PanelChanger(Panels.Settings); }
    public void ShowStorePanel() { PanelChanger(Panels.Store); }
    public void ShowSignUpPanel() { PanelChanger(Panels.SignUp); }
    public void ShowSignInPanel() { PanelChanger(Panels.SignIn); }
    public void ShowResetPasswordPanel() { PanelChanger(Panels.ResetPassword); }
    public void ShowUserProfilePanel() { PanelChanger(Panels.UserProfile); }
    public void ShowLosePanel() { PanelChanger(Panels.Lose); }
    public void ShowWinPanel() { PanelChanger(Panels.Win); }

    private void PanelChanger(Panels panels)
    {
        pnl_Menu.SetActive(panels == Panels.Menu);
        pnl_Lobby.SetActive(panels == Panels.Lobby);
        pnl_Game.SetActive(panels == Panels.Game);
        pnl_Settings.SetActive(panels == Panels.Settings);
        pnl_Store.SetActive(panels == Panels.Store);
        pnl_SignUp.SetActive(panels == Panels.SignUp);
        pnl_SignIn.SetActive(panels == Panels.SignIn);
        pnl_ResetPassword.SetActive(panels == Panels.ResetPassword);
        pnl_UserProfile.SetActive(panels == Panels.UserProfile);
        pnl_Lose.SetActive(panels == Panels.Lose);
        pnl_Win.SetActive(panels == Panels.Win);
    }
    
    #endregion
}