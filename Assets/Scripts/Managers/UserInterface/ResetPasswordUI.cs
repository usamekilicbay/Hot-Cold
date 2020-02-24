using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResetPasswordUI : Singleton<ResetPasswordUI>
{
    [Header("Reset Password")]
    [SerializeField] TextMeshProUGUI txt_ResetPassword_Header;
    [SerializeField] TextMeshProUGUI txt_ResetPassword_Email;
    [SerializeField] Button btn_ResetPassword_Home;
    [SerializeField] Button btn_ResetPassword_Send;

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        
    }

    void OnClickAddListener()
    {
        //btn_ResetPassword_Home.onClick.AddListener(ShowMenuPanel);
        btn_ResetPassword_Send.onClick.AddListener(ResetPassword);
    }

    private void ResetPassword() 
    {
        ActionManager.Instance.ResetPasswordWithMail(txt_ResetPassword_Email.text);
    }
}
