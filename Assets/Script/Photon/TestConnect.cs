using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestConnect : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject createRoomPanel;
    private static TestConnect instance;
    private int roomCount = 0;

    #region Properties
    public static TestConnect Instance { get => instance; }
    #endregion
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    private void Start()
    {
        print("Connecting to server. ");
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server. ");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        roomCount = roomList.Count;
    }
    public void OnClick_CreateRoom(string name, bool isPrivate)
    {
        RoomOptions options = new RoomOptions();
        options.IsVisible = isPrivate;
        options.MaxPlayers = 4;
        // create a gameobject rooms and storage in a list. (or get list of rooms and genarate id for created room)
        string id = string.Format("{0}", roomCount + 1);
        PhotonNetwork.CreateRoom(id, options);
    }
}
