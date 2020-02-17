using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignInUI : Singleton<SignInUI>
{
    [Header("SignIn")]
    [SerializeField] TextMeshProUGUI txt_SignIn_Header;
    [SerializeField] TextMeshProUGUI txt_SignIn_Username;
    [SerializeField] TextMeshProUGUI txt_SignIn_Email;
    [SerializeField] TextMeshProUGUI txt_SignIn_Password;
    [SerializeField] Button btn_SignIn_Home;
    [SerializeField] Button btn_SignIn_Send;
    [SerializeField] Button btn_SignIn_ResetPassword;
    [SerializeField] Button btn_SignIn_SignUp;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    private void OnClickAddListener()
    {
        btn_SignIn_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_SignIn_ResetPassword.onClick.AddListener(UIManager.Instance.ShowResetPasswordPanel);
        btn_SignIn_SignUp.onClick.AddListener(UIManager.Instance.ShowSignUpPanel);
        btn_SignIn_Send.onClick.AddListener(() => ActionManager.Instance.SignInEmailPassword(txt_SignIn_Email.text, txt_SignIn_Password.text));
    }
}
