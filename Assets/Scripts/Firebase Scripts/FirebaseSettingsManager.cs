using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Firebase.Database;
public class FirebaseSettingsManager : FirebaseBaseManager
{
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    #region Localization

    private void GetLocalization() 
    {
       /*Task<DataSnapshot> task = settingsReference.Child(ConstantKeeper.GameSettingsPaths.Localization).GetValueAsync();

        DataSnapshot snapshot = task.Result;

        foreach (DataSnapshot item in snapshot)
        {
            LocalizationKeeper()
        }

        LocalizationKeeper.AbandonedMatches = snapshot.GetValue.*/
    }
    
    #endregion
}
