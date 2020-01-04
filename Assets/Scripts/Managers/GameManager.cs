using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    FBManager fBManager;

    private void Start()
    {
        fBManager = FBManager.Instance;    
    }

    public  void ControlAnswer(int guessedNumber)
    {
        int secretNumber = fBManager.GetSecretNumber();

        if (guessedNumber == secretNumber)
        {
            Debug.Log("Bildin!");
        }
        else if (guessedNumber != secretNumber)
        {           
            int majority = Mathf.Abs(guessedNumber - secretNumber);

            string assistance;
            string nearlyAssistance;
            Debug.Log(majority);

            if (guessedNumber < secretNumber)
            {
                assistance = "Büyük";                
            }
            else if (guessedNumber > secretNumber)
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

        }
    }
}
