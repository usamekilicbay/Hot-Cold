using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCreator : MonoBehaviour
{
    [Range(0,10000)]
    [SerializeField] private int numberRange;
    [SerializeField] private static int secretNumber;

    // Script References
    FBManager fBManager;


    void Start()
    {
        fBManager = FBManager.Instance;

    //    CreateNumber();
    }

    public void CreateNumber()
    {
        secretNumber = Random.Range(0, numberRange);
        fBManager.SetSecretNumber(secretNumber);
    }
}
