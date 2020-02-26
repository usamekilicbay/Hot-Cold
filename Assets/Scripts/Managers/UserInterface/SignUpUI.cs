using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignUpUI : Singleton<SignUpUI>
{
    [Header("SignUp")]
    [SerializeField] private TextMeshProUGUI txt_SignUp_Header;
    [SerializeField] private TextMeshProUGUI txt_SignUp_Username;
    [SerializeField] private TextMeshProUGUI txt_SignUp_Email;
    [SerializeField] private TextMeshProUGUI txt_SignUp_Password;
    [SerializeField] private TextMeshProUGUI txt_SignUp_ConfirmPassword;
    [SerializeField] private TextMeshProUGUI txt_SignUp_Language;
    [SerializeField] private Button btn_SignUp_Home;
    [SerializeField] private Button btn_SignUp_Send;
    [SerializeField] private Button btn_SignUp_SignIn;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {

    }

    private void OnClickAddListener()
    {
        btn_SignUp_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_SignUp_SignIn.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
        btn_SignUp_Send.onClick.AddListener(SignUp);
    }

    private void SignUp() 
    {
        SignUpStruct signUpStruct = new SignUpStruct();

        signUpStruct.Username = txt_SignUp_Username.text.Replace("\u200B", "");
        signUpStruct.Email = txt_SignUp_Email.text.Replace("\u200B", "");
        signUpStruct.Password = txt_SignUp_Password.text.Replace("\u200B", "");
        signUpStruct.ConfirmPassword = txt_SignUp_ConfirmPassword.text.Replace("\u200B", "");//.Replace("\u200B", "");
        signUpStruct.Language = txt_SignUp_Language.text.Replace("\u200B", "");

        ActionManager.Instance.SignUpWithEmailPassword(signUpStruct);
    }
}
