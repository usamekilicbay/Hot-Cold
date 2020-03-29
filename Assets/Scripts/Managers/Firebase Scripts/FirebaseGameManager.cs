using ConstantKeeper;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseGameManager : MonoBehaviour
{
    private void OnEnable()
    {
    //    ActionManager.Instance.SendEstimation += SendEstimation;
    }

    private void OnDisable()
    {
        ActionManager.Instance.SendEstimation -= SendEstimation;
    }

    private void OnApplicationQuit()
    {
        ActionManager.Instance.SendEstimation -= SendEstimation;
    }

    private void SendEstimation(int _estimation)
    {
        FirebaseBaseManager.roomReference.Child(RoomPaths.Progression).Child(RoomPaths.LastEstimation).SetValueAsync(_estimation);
        FirebaseBaseManager.roomReference.Child(RoomPaths.Progression).Child(RoomPaths.LastEstimation).SetValueAsync(CurrentRoomInfoKeeper.rivalUsername);
        GameProgressionListener();
    }

    private void GameProgressionListener()
    {
        FirebaseBaseManager.roomReference.Child(RoomPaths.Progression).Child(RoomPaths.LastEstimation).ValueChanged += GetEstimation;
        FirebaseBaseManager.roomReference.Child(RoomPaths.Progression).Child(RoomPaths.WhoseTurn).ValueChanged += WhoseTurn;
    }

    private void WhoseTurn(object sender, ValueChangedEventArgs args) 
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Tahmin Sırasında Bir Değişiklik Algılandı");

        DataSnapshot snapshot = args.Snapshot;

        ShowWhoseTurn(snapshot.Value.ToString());
    }

    private void GetEstimation(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        Debug.Log("Son Tahminde Bir Değişiklik algılandı");

        DataSnapshot snapshot = args.Snapshot;

        ShowLastEstimation(snapshot.Value.ToString());
    }
    private void ShowLastEstimation(string _lastEstimation)
    {
        ActionManager.Instance.ShowLastEstimation(_lastEstimation);
    }

    private void ShowWhoseTurn(string _whoseTurn) 
    {
        ActionManager.Instance.ShowWhoseTurn(_whoseTurn);
    }

}
