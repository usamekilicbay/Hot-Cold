using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignUpUI : Singleton<SignUpUI>
{
    [Header("SignUp")]
    [SerializeField] TextMeshProUGUI txt_SignUp_Header;
    [SerializeField] TextMeshProUGUI txt_SignUp_Username;
    [SerializeField] TextMeshProUGUI txt_SignUp_Email;
    [SerializeField] TextMeshProUGUI txt_SignUp_Password;
    [SerializeField] TextMeshProUGUI txt_SignUp_ConfirmPassword;
    [SerializeField] TextMeshProUGUI txt_SignUp_Language;
    [SerializeField] Button btn_SignUp_Home;
    [SerializeField] Button btn_SignUp_Send;
    [SerializeField] Button btn_SignUp_SignIn;

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

        signUpStruct.Username = txt_SignUp_Username.text;
        signUpStruct.Email = txt_SignUp_Email.text;
        signUpStruct.Password = txt_SignUp_Password.text;
        signUpStruct.ConfirmPassword = txt_SignUp_ConfirmPassword.text;//.Replace("\u200B", "");
        signUpStruct.Language = txt_SignUp_Language.text;

        ActionManager.Instance.SignUpWithEmailPassword(signUpStruct);
    }
}
