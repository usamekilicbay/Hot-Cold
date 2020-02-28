using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignInUI : Singleton<SignInUI>
{
    [Header("SignIn")]
    [SerializeField] private TextMeshProUGUI txt_SignIn_Header;
    /*[SerializeField] private TextMeshProUGUI txt_SignIn_Username;
    [SerializeField] private TextMeshProUGUI txt_SignIn_Email;
    [SerializeField] private TextMeshProUGUI txt_SignIn_Password;*/
    [SerializeField] private TMP_InputField inpFld_Email;
    [SerializeField] private TMP_InputField inpFld_Password;
    [SerializeField] private Button btn_Home;
    [SerializeField] private Button btn_SignIn;
    [SerializeField] private Button btn_ResetPassword;
    [SerializeField] private Button btn_SignUp;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {
        
    }

    private void OnClickAddListener()
    {
        btn_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        btn_ResetPassword.onClick.AddListener(UIManager.Instance.ShowResetPasswordPanel);
        btn_SignUp.onClick.AddListener(UIManager.Instance.ShowSignUpPanel);
        btn_SignIn.onClick.AddListener(SignIn);
    }

    private void SignIn()
    {
        string email = inpFld_Email.textComponent.text.Replace("\u200B", "");
        string password = inpFld_Password.textComponent.text.Replace("\u200B", "");

        ActionManager.Instance.SignInWithEmailPassword(email, password);
    }
}
