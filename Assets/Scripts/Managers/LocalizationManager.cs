﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    public Dictionary<string, string> localizationDictionary = new Dictionary<string, string>();

    public void SetDictionary(string _key, string _value)
    {
        // var variable = JsonUtility.FromJson<Dictionary<string, string>>(_key);
        localizationDictionary.Add(_key, _value);


        Debug.Log(localizationDictionary);
    }
}