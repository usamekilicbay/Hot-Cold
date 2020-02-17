using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameUI : Singleton<GameUI>
{
    [Header("Game")]
    [SerializeField] TextMeshProUGUI txt_Game_Header;
    [SerializeField] TextMeshProUGUI txt_Game_LastEstimation;
    [SerializeField] TextMeshProUGUI txt_Game_MyEstimation;
    [SerializeField] Button btn_Game_Home;
    [SerializeField] Button btn_Game_Answer;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {

    }

    private void OnClickAddListener()
    {
        btn_Game_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        //btn_Game_Settings.onClick.AddListener(ShowSettingsPanel);
        //btn_Game_Answer.onClick.AddListener(Answer);
    }


    public void ShowEstimation(string lastEstimation) { txt_Game_LastEstimation.SetText(lastEstimation); }
}
