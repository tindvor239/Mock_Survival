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
    private Text players;
    private GameManager gameManager;
    #endregion
    #region Properties
    public string ID { get => id.text; set => id.text = string.Format("ID: {0}", value); }
    public string Name { get => name.text; set => name.text = string.Format("Name: {0}", value); }
    public string Host { get => host.text; set => host.text = string.Format("Host: {0}", value); }
    public sbyte Players { get => sbyte.Parse(players.text); set => players.text = string.Format("Players: {0} / 4", value.ToString()); }
    #endregion
    #region Contructor
    public void Construct (string id, string name, string host, sbyte players)
    {
        ID = id;
        Name = name;
        Host = host;
        Players = players;
    }

    #endregion
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void OnClick_Join()
    {
        PhotonNetwork.JoinRoom(ID);
        gameManager.LoadScene(1);
    }
}
