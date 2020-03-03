using ConstantKeeper;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;
public class FirebaseSettingsManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GetLocalization());
       // GetLocalization();
    }

    private void OnDisable()
    {
        
    }

    #region Localization

    private IEnumerator GetLocalization()
    {

        Debug.Log("yeter be yeter");

        DatabaseReference localizationReference = FirebaseDatabase.DefaultInstance.GetReference($"{GameSettingsPaths.GameSettings}/{GameSettingsPaths.Localization}");

        Task<DataSnapshot> task = localizationReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

        if (task.IsCanceled)
        {
            Debug.LogWarning(GetDataTaskDebugs.GetData + Debugs.IsCanceled);
        }
        else if (task.IsFaulted)
        {
            Debug.LogError(GetDataTaskDebugs.GetData + Debugs.IsFaulted);
        }
        else if (task.IsCompleted)
        {
            Debug.Log(GetDataTaskDebugs.GetData + Debugs.IsCompleted);

            DataSnapshot snapshot = task.Result;
           
            string json = snapshot.GetRawJsonValue();
            LocalizationManager.Instance.SetDictionary(json);
            Debug.Log("AL sana bir adet " + json);
        }
    }
    
    #endregion
}
