using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberCreator : MonoBehaviour
{
    [Range(0,10000)]
    public int numberRange;
    public static int trueNumber;

    void Start()
    {
        CreateNumber();
    }

    void CreateNumber()
    {
        trueNumber = Random.Range(0, numberRange);
        Debug.Log(trueNumber);
    }
}
