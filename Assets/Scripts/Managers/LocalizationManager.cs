using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    public Dictionary<string, string> localizationDictionary = new Dictionary<string, string>();

    public void SetDictionary(string _json)
    {
        localizationDictionary = JsonUtility.FromJson<Dictionary<string, string>>(_json);
        Debug.Log(localizationDictionary);
    }
}
