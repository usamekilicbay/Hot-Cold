using ConstantKeeper;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseRoomManager : FirebaseBaseManager
{
    bool canPlay = true;
    bool canStart = false;
    bool owner = true;

    
    private void Start()
    {
        Debug.Log("Double");
        ActionManager.Instance.QuickGame += CallQuickGame;

        SetRoomReference();
    }

    private void OnDisable()
    {
        ActionManager.Instance.QuickGame -= CallQuickGame;
    }

    private void OnApplicationQuit()
    {
        ActionManager.Instance.QuickGame -= CallQuickGame;
        /*roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-ID").SetValueAsync("");
        roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-Username").SetValueAsync("");*/
    }

    private void SetRoomReference()
    {
        roomReference = FirebaseDatabase.DefaultInstance.GetReference($"{RoomPaths.Rooms}/{RoomPaths.RoomID}");
    }

    public void CallQuickGame()
    {
        Debug.Log("kekke");
        //StartCoroutine(QuickGame());
    }

    private IEnumerator QuickGame()
    {
        Task<DataSnapshot> task = roomReference.GetValueAsync();

        yield return new WaitUntil(() => task.IsCanceled || task.IsCompleted || task.IsFaulted);

        if (task.IsFaulted)
        {
            Debug.Log("Oyun bulma işlemi hata verdi!");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.HasChildren)
            {
                if (canPlay)
                {
                    foreach (DataSnapshot rooms in snapshot.Children)
                    {
                        bool penetrability = bool.Parse(rooms.Child(RoomPaths.General).Child("Penetrability").Value.ToString());

                        if (penetrability)
                        {
                            CurrentRoomInfoKeeper.roomID = rooms.Key;
                            JoinRoom();
                        }
                        else
                        {
                            CreateRoom();
                            Debug.Log("Girilebilir bir oda bulunamadı, oda oluşturuluyor...");
                        }
                    }
                }
                else
                {
                    Debug.Log("Zaten bir odaya dahilsiniz tekrar giriş yapamazsınız");
                }
            }
            else if (!snapshot.HasChildren)
            {
                Debug.Log("Yeni oda oluşturuluyor, oyuncu bekleniyor");
                CreateRoom();
            }
        }
    }

    private void CreateRoom(string roomName = "", string roomPassword = "")
    {
        if (canPlay)
        {
            CurrentRoomInfoKeeper.roomID = roomReference.Push().Key;
            roomReference = roomReference.Child(CurrentRoomInfoKeeper.roomID);
           
            // General
            Dictionary<string, object> roomGeneralDictionary = new Dictionary<string, object>
            {
                /*["RoomName"] = roomName,
                  ["RoomPassword"] = roomPassword,*/
                [RoomPaths.RoomID] = CurrentRoomInfoKeeper.roomID,
                [RoomPaths.P1_ID] = auth.CurrentUser.UserId,
                [RoomPaths.P1_Username] = CurrentUserProfileKeeper.Username,
                /*[RoomPaths.P2_ID] = "",
                [RoomPaths.P2_Username] = "",*/
                [RoomPaths.ScoreLimit] = 1,
                [RoomPaths.AnswerTimeLimit] = 5,
                [RoomPaths.PlayerLimit] = 2,
                [RoomPaths.SecretNumber] = 0,
                [RoomPaths.SecretNumberMaxValue] = 1000,
                [RoomPaths.Penetrability] = true
            };

            roomReference.Child(RoomPaths.General).SetValueAsync(roomGeneralDictionary);


            // Progression
            Dictionary<string, object> roomProgressionDictionary = new Dictionary<string, object>
            {
                [RoomPaths.LastEstimation] = 0,
                [RoomPaths.WhoseTurn] = ""
            };

            roomReference.Child(RoomPaths.Progression).SetValueAsync(roomProgressionDictionary);

            canPlay = false;

            Debug.Log("Yeni oda oluşturuldu, oyuncu bekleniyor");

            owner = true;

            JoinListener();
        }
        else
        {
            Debug.Log("Zaten kurulmuş bir odanız var!");
        }
    }

    private void JoinRoom()
    {
        roomReference = roomReference.Child(CurrentRoomInfoKeeper.roomID);

        roomReference.Child(RoomPaths.General).Child(RoomPaths.P2_ID).SetValueAsync(auth.CurrentUser.UserId);
        roomReference.Child(RoomPaths.General).Child(RoomPaths.P2_Username).SetValueAsync(CurrentUserProfileKeeper.Username);

        canPlay = false;

        canStart = true;

        if (canStart)
        {
            owner = false;
            JoinListener();
            //  StartGame();
        }

        Debug.Log("Odaya giriş başarılı!");
    }

    private void JoinListener() 
    {
        Debug.Log("ama neden");
        roomReference.Child(RoomPaths.General).Child(RoomPaths.P2_ID).ValueChanged += StartGame;
    }

    private void StartGame(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        SetSecretNumber(NumberCreator.CreateNumber());
        StartCoroutine(GetRoomInfos());
        UIManager.Instance.ShowGamePanel();
    }

    private void SetSecretNumber(int currentNumber)
    {
        roomReference.Child(RoomPaths.General).Child(RoomPaths.SecretNumber).SetValueAsync(currentNumber);
    }

    private IEnumerator GetRoomInfos()
    {
        Task<DataSnapshot> task = roomReference.Child(RoomPaths.General).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted || task.IsFaulted || task.IsCanceled);

        if (task.IsCanceled)
        {
            Debug.Log("Gizli sayı çekme işlemi iptal edildi");
        }
        else if (task.IsFaulted)
        {
            Debug.Log("Gizli sayı çekme işlemi iptal başarısız");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (owner)
            {
                CurrentRoomInfoKeeper.rivalID = snapshot.Child(RoomPaths.P2_ID).Value.ToString();
                CurrentRoomInfoKeeper.rivalUsername = snapshot.Child(RoomPaths.P2_Username).Value.ToString();
                Debug.Log("Ensar");
            }
            else
            {
                CurrentRoomInfoKeeper.rivalID = snapshot.Child(RoomPaths.P1_ID).Value.ToString();
                CurrentRoomInfoKeeper.rivalUsername = snapshot.Child(RoomPaths.P1_Username).Value.ToString();
                Debug.Log("Muhacir");
            }

            CurrentRoomInfoKeeper.answerTimeLimit = int.Parse(snapshot.Child(RoomPaths.AnswerTimeLimit).Value.ToString());
            CurrentRoomInfoKeeper.scoreLimit = int.Parse(snapshot.Child(RoomPaths.ScoreLimit).Value.ToString());
            CurrentRoomInfoKeeper.secretNumber = int.Parse(snapshot.Child(RoomPaths.SecretNumber).Value.ToString());
            CurrentRoomInfoKeeper.secretNumberMaxValue = int.Parse(snapshot.Child(RoomPaths.SecretNumberMaxValue).Value.ToString());
        }
    }

    /* private void GetCurrentRoomInfoKeeper.roomID(string roomName)
     {
         roomReference.GetValueAsync().ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 return;
             }
             else if (task.IsFaulted)
             {
                 return;
             }

             DataSnapshot snapshot = task.Result;

             foreach (DataSnapshot CurrentRoomInfoKeeper.roomID in snapshot.Children)
             {
                 if (CurrentRoomInfoKeeper.roomID.Child("General").Child("RoomName").Value.ToString() == roomName)
                 {
                     CurrentRoomInfoKeeper.roomID = CurrentRoomInfoKeeper.roomID.Key.ToString();
                 }
             }

         });
     }




     private void EnterTheRoom(string roomName, string roomPassword)
     {
         GetCurrentRoomInfoKeeper.roomID(roomName);

         string correctRoomPassword = "";

         roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("RoomPassword").GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 return;
             }

             DataSnapshot snapshot = task.Result;

             correctRoomPassword = snapshot.Value.ToString();
         });

         if (correctRoomPassword != "")
         {
             if (roomPassword == correctRoomPassword)
             {
                 roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-ID").SetValueAsync(userID);
                 roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-UserName").SetValueAsync(username);
                 return;
             }
             else
             {
                 Debug.Log("Oda ismi ile şifre uyuşmuyor, tekrar deneyin...");
                 return;
             }
         }
         else if (correctRoomPassword == "")
         {
             Debug.Log("şifresiz giriş");
             roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-ID").SetValueAsync(userID);
             roomReference.Child(CurrentRoomInfoKeeper.roomID).Child("General").Child("P2-UserName").SetValueAsync(username);
             return;
         }
     }

     private void AddRoomListToDropdown(TMPro.TMP_Dropdown dropdown)
     {
         roomReference.GetValueAsync().ContinueWith(task =>
         {
             if (task.IsCanceled)
             {
                 return;
             }
             else if (task.IsFaulted)
             {
                 return;
             }

             DataSnapshot snapshot = task.Result;

             List<string> roomList = new List<string>();

             foreach (DataSnapshot r in snapshot.Children)
             {
                 string CurrentRoomInfoKeeper.roomID = r.Key;
                 string roomName = snapshot.Child(CurrentRoomInfoKeeper.roomID).Child("RoomName").Value.ToString();
                 string roomOwner = snapshot.Child(CurrentRoomInfoKeeper.roomID).Child("Player1-ID").Value.ToString();

                 roomList.Add(roomName);
                 Debug.Log($"Room ID = {CurrentRoomInfoKeeper.roomID}  Room Creator = {roomOwner}");
             }

             dropdown.AddOptions(roomList);

         });
     }*/
}
