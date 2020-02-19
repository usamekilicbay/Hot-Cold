using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCreator : MonoBehaviour
{
    [Range(0,10000)]
    [SerializeField] private static int numberRange = 1000;
    private static int secretNumber;

    private void OnEnable()
    {
        //ActionManager.Instance.CreateSecretNumber += CreateNumber;
    }

    private void OnDisable()
    {
        //ActionManager.Instance.CreateSecretNumber -= CreateNumber;
    }

    public static int CreateNumber()
    {
        secretNumber = Random.Range(0, numberRange);

        return secretNumber;
    }
}
