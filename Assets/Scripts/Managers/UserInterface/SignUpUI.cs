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
        btn_SignUp_Send.onClick.AddListener(() => ActionManager.Instance.SignUpEmailPassword(txt_SignUp_Username.text, txt_SignUp_Email.text, txt_SignUp_Password.text));
    }

}
