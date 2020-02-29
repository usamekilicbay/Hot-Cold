using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panel / Menu")]
    [SerializeField] TextMeshProUGUI txt_Menu_Header;
    [SerializeField] Button btn_Menu_Play;
    [SerializeField] Button btn_Menu_Settings;
    [SerializeField] Button btn_Menu_Store;
    [SerializeField] Button btn_Menu_RateUs;
    [SerializeField] Button btn_Menu_User;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    private void OnClickAddListener() 
    {
        btn_Menu_Play.onClick.AddListener(QuickGame);
        btn_Menu_Settings.onClick.AddListener(UIManager.Instance.ShowSettingsPanel);
        btn_Menu_Store.onClick.AddListener(UIManager.Instance.ShowStorePanel);
        btn_Menu_User.onClick.AddListener(UIManager.Instance.ShowUserProfilePanel);
        //btn_Menu_User.onClick.AddListener(() => ActionManager.Instance.CallCurrentUserProfile());
        //btn_Menu_RateUs.onClick.AddListener(RateUs);
    }

    private void QuickGame() 
    {
        Debug.Log("hiraishin");
        ActionManager.Instance.QuickGame();
    }

    private void RateUs() { }

    private void GetRank() { }
}
