using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //Should also be raised during host migration somehow.
    [SerializeField]
    private UnityEvent hostingEvent;

    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to join a room and failed");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 8 });

        hostingEvent.Invoke();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined a room succesfully");
    }
}
