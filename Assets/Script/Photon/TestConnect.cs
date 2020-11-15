using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections.Generic;

public class TestConnect : MonoBehaviourPunCallbacks
{
    private int roomCount = 0;
    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject roomContent;
    [SerializeField]
    private List<Room> rooms;
    #region Properties
    #endregion
    #region Singleton
    private static TestConnect instance;
    public static TestConnect Instance { get => instance; }
    private void Awake()
    {
        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.GameVersion = "0.0.1";
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.LocalPlayer.NickName);
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if(info.RemovedFromList)
            {
                foreach(Room room in rooms)
                {
                    if (room.Name == info.Name)
                        Destroy(room.gameObject);
                }
            }
        }
    }
    public void OnClick_CreateRoom(string name, bool isPrivate)
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("you have already join a room.");
            return;
        }
        RoomOptions options = new RoomOptions();
        options.IsVisible = isPrivate;
        options.MaxPlayers = 4;
        // create a gameobject rooms and storage in a list. (or get list of rooms and genarate id for created room).
        string id = string.Format("{0}", roomCount + 1);
        GameObject newObject = Instantiate(roomPrefab, roomContent.transform);
        Room newRoom = newObject.GetComponent<Room>();
        newRoom.Construct(id, name, "", 1);
        rooms.Add(newRoom);
        PhotonNetwork.CreateRoom(id, options);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("created room successfully");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("fail to create room, u already join a room??");
    }
}
