using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Room : MonoBehaviour
{
    #region Member Field
    [SerializeField]
    private Text id;
    [SerializeField]
    private new Text name;
    [SerializeField]
    private Text host;
    [SerializeField]
    private Text currentPlayer;

    private static byte maxPlayer = 4;
    private TestConnect connection;
    #endregion
    #region Properties
    public string ID { get => id.text.Split(' ')[1]; set => id.text = string.Format("ID: {0}", value); }
    public string Name { get => name.text.Split(' ')[1]; set => name.text = string.Format("Name: {0}", value); }
    public string Host { get => host.text.Split(' ')[1]; set => host.text = string.Format("Host: {0}", value); }
    public byte MaxPlayer { get => maxPlayer; set => maxPlayer = value; }
    public byte CurrentPlayer { get => byte.Parse(currentPlayer.text); set => currentPlayer.text = string.Format("Players: {0} / {1}", value.ToString(), maxPlayer); }
    #endregion
    #region Contructor
    public void Construct (string id, string name, string host, byte currentPlayer, byte maxplayers)
    {
        ID = id;
        Name = name;
        Host = host;
        MaxPlayer = maxplayers;
        CurrentPlayer = currentPlayer;
    }

    #endregion
    private void Start()
    {
        connection = TestConnect.Instance;
    }
    public string GetInfo()
    {
        return string.Format("{0}_{1}_{2}", ID, Name, Host);
    }
    public void OnClick_Join()
    {
        connection.OnClick_JoinRoom(this);
    }
}
