using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerEstimationManager : MonoBehaviour
{

    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        inputField.onEndEdit.AddListener(GetGuess);
    }
    public void GetGuess(string getNumber)
    {
      //  GameManager.ControlAnswer(int.Parse(getNumber));
    }   
}
