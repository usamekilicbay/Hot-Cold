using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignUpUI : Singleton<SignUpUI>
{
    [Header("SignUp")]
    [SerializeField] private TextMeshProUGUI txt_SignUp_Header;
    [SerializeField] private TMP_InputField inpfld_Username;
    [SerializeField] private TMP_InputField inpfld_Email;
    [SerializeField] private TMP_InputField inpfld_Password;
    [SerializeField] private TMP_InputField inpfld_ConfirmPassword;
    [SerializeField] private TMP_Dropdown drpdwn_Language;
    [SerializeField] private Button btn_Home;
    [SerializeField] private Button btn_SignUp;
    [SerializeField] private Button btn_SignIn;

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
        btn_SignIn.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
        btn_SignUp.onClick.AddListener(SignUp);
    }

    private void SignUp() 
    {
        SignUpStruct signUpStruct = new SignUpStruct();

        signUpStruct.Username = inpfld_Username.textComponent.text.Replace("\u200B", "");
        signUpStruct.Email = inpfld_Email.textComponent.text.Replace("\u200B", "");
        signUpStruct.Password = inpfld_Password.textComponent.text.Replace("\u200B", "");
        signUpStruct.ConfirmPassword = inpfld_ConfirmPassword.textComponent.text.Replace("\u200B", "");//.Replace("\u200B", "");
        signUpStruct.Language = drpdwn_Language.itemText.text.Replace("\u200B", "");

        ActionManager.Instance.SignUpWithEmailPassword(signUpStruct);
    }
}
