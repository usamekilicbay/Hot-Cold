using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignInUI : Singleton<SignInUI>
{
    [Header("SignIn")]
    [SerializeField] private TextMeshProUGUI txt_SignIn_Header;
    [SerializeField] private TextMeshProUGUI txt_SignIn_Username;
    [SerializeField] private TextMeshProUGUI txt_SignIn_Email;
    [SerializeField] private TextMeshProUGUI txt_SignIn_Password;
    [SerializeField] private TMP_InputField txt_SignIn_mail;
    [SerializeField] private TMP_InputField txt_SignIn_pasword;
    [SerializeField] private Button btn_SignIn_Home;
    [SerializeField] private Button btn_SignIn_Send;
    [SerializeField] private Button btn_SignIn_ResetPassword;
    [SerializeField] private Button btn_SignIn_SignUp;

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
        btn_SignIn_Send.onClick.AddListener(SignIn);
    }

    private void SignIn() 
    {
      //string email = txt_SignIn_mail.textComponent.text;
      // string password = txt_SignIn_pasword.textComponent.text;
        string email = txt_SignIn_Email.text.Replace("\u200B", ""); ;//.Replace("\u200B", "");
        string password = txt_SignIn_Password.text.Replace("\u200B", "");

        ActionManager.Instance.SignInWithEmailPassword(email, password);
    }
}
