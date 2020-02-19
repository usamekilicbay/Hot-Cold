using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int secretNumber;
    private void OnEnable()
    {
        ActionManager.Instance.ControlAnswer += ControlAnswer;
        secretNumber = CurrentRoomInfoKeeper.secretNumber;
    }

    private void OnDisable()
    {
        ActionManager.Instance.ControlAnswer -= ControlAnswer;
    }

    public  void ControlAnswer(int _estimatedNumber)
    {
        if (_estimatedNumber == secretNumber)
        {
            Debug.Log("Bildin!");
        }
        else if (_estimatedNumber != secretNumber)
        {           
            int majority = Mathf.Abs(_estimatedNumber - secretNumber);

            string assistance;
            string nearlyAssistance;
            Debug.Log(majority);

            if (_estimatedNumber < secretNumber)
            {
                assistance = "Büyük";                
            }
            else if (_estimatedNumber > secretNumber)
            {
                assistance = "Küçük";
            }
            else
            {
                assistance = "Mal mısın";
            }

            if (majority < 50)
            {
                nearlyAssistance = "Azıcık";
            }
            else if (majority < 100)
            {
                nearlyAssistance = "Neredeyse";
            }
            else if (majority < 300)
            {
                nearlyAssistance = "Biraz";
            }
            else if (majority < 500)
            {
                nearlyAssistance = "Fazla";
            }
            else if (majority < 700)
            {
                nearlyAssistance = "Bayağı";
            }
            else if (majority < 900)
            {
                nearlyAssistance = "Çok";
            }
            else
            {
                nearlyAssistance = "Alakası bile yok";
            }

            string mainAsistance = $"{nearlyAssistance} {assistance}";

            Debug.Log($"Tekrar dene: {mainAsistance}");

            ActionManager.Instance.SendEstimation(_estimatedNumber);
            
        }
    }
}
