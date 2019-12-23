using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static void ControlAnswer(int guessedNumber)
    {
        int trueNumber = NumberCreator.trueNumber;
        if (guessedNumber == trueNumber)
        {
            Debug.Log("Bildin!");
        }
        else if (guessedNumber != trueNumber)
        {           
            int majority = Mathf.Abs(guessedNumber - trueNumber);

            string assistance;
            string nearlyAssistance;
            Debug.Log(majority);

            if (guessedNumber < trueNumber)
            {
                assistance = "Büyük";                
            }
            else if (guessedNumber > trueNumber)
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
