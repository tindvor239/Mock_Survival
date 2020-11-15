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
    private List<Room> rooms = new List<Room>();
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
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = "0.0.1";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
            Debug.Log("You're Already Connected");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.NickName = "Steave";
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {

    }
    public override void OnDisconnected(DisconnectCause cause)
    {

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo roomInfo in roomList)
        {
            if(roomInfo.RemovedFromList)
            {
                int index = rooms.FindIndex(x => x.GetInfo() == roomInfo.Name);
                if(index != -1)
                {
                    Destroy(rooms[index].gameObject);
                    rooms.RemoveAt(index);
                }
            }
            else
            {
                GameObject newObject = Instantiate(roomPrefab, roomContent.transform);
                Room newRoom = newObject.GetComponent<Room>();
                string[] info = roomInfo.Name.Split('_');
                newRoom.Construct(info[0], info[1], info[2], (byte)roomInfo.PlayerCount, roomInfo.MaxPlayers);
                rooms.Add(newRoom);
            }
        }
    }
    public void OnClick_CreateRoom(string roomName, bool isPrivate)
    {
        RoomOptions options = new RoomOptions() { IsVisible = !isPrivate,  IsOpen = true, MaxPlayers = 4 };
        string id = string.Format("{0}_{1}_{2}", roomCount + 1, roomName, PhotonNetwork.NickName);
        if (PhotonNetwork.CreateRoom(id, options, TypedLobby.Default))
        {
            Console.Instance.Print("Create Room Successfully", Color.green);
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
            Console.Instance.Print("Create Room Failed", Color.red);
        }
    }
    public override void OnJoinedRoom()
    {
        
    }
    public override void OnCreatedRoom()
    {
        Console.Instance.Print("Room Created Successfully", Color.green);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Console.Instance.Print("Create Room Failed: "+ message, Color.red);
    }
    public void OnClick_JoinRoom(Room room)
    {
        PhotonNetwork.JoinRoom(room.GetInfo());
        PhotonNetwork.LoadLevel(1);
    }
}
