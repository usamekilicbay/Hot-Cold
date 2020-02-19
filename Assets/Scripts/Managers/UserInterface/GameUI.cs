using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : Singleton<GameUI>
{
    [Header("Game")]
    [SerializeField] private TextMeshProUGUI txt_Game_Header;
    [SerializeField] private TextMeshProUGUI txt_Game_CurrentUsername;
    [SerializeField] private TextMeshProUGUI txt_Game_RivalUsername;
    [SerializeField] private TextMeshProUGUI txt_Game_WhoseTurn;
    [SerializeField] private TextMeshProUGUI txt_Game_AnswerTimeLimit;
    [SerializeField] private TextMeshProUGUI txt_Game_LastEstimation;
    [SerializeField] private TextMeshProUGUI txt_Game_MyEstimation;
    [SerializeField] private Button btn_Game_Home;
    [SerializeField] private Button btn_Game_Answer;

    private void OnEnable()
    {
        OnClickAddListener();
        PrepareUIForGame();
    
        ActionManager.Instance.ShowLastEstimation += ShowLastEstimation;
    }

    private void OnDisable()
    {
        ActionManager.Instance.ShowLastEstimation -= ShowLastEstimation;
    }

    private void OnClickAddListener()
    {
        btn_Game_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        //btn_Game_Settings.onClick.AddListener(ShowSettingsPanel);
        btn_Game_Answer.onClick.AddListener(Estimate);
    }

    private void PrepareUIForGame() 
    {
        txt_Game_CurrentUsername.SetText(CurrentUserProfileKeeper.Username);
        txt_Game_RivalUsername.SetText(CurrentRoomInfoKeeper.rivalUsername);
        txt_Game_AnswerTimeLimit.SetText(CurrentRoomInfoKeeper.answerTimeLimit.ToString());
       // txt_Game_WhoseTurn.SetText(CurrentRoomInfoKeeper.whoseTurn);
    }

    private void Estimate()
    {
        string estimationString = txt_Game_MyEstimation.text.Replace("\u200B", "");
      
        ActionManager.Instance.ControlAnswer(int.Parse(estimationString));
    }

    public void ShowLastEstimation(string lastEstimation) 
    { 
        txt_Game_LastEstimation.SetText(lastEstimation); 
    }
}
