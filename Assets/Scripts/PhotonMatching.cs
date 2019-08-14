using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonMatching : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Player Name Inputfield
    /// </summary>
    public InputField playerName;

    /// <summary>
    /// Enter Username
    /// </summary>
    public GameObject EnterNamePanel;

    /// <summary>
    /// Photon View
    /// </summary>
    public PhotonView pv;

    /// <summary>
    /// Room Layout Group
    /// </summary>
    public Transform roomlayoutGroup;

    /// <summary>
    /// Players prefab
    /// </summary>
    public GameObject playerPrefab;

    /// <summary>
    /// Room Panel
    /// </summary>
    public GameObject roomPanel;

    /// <summary>
    /// Maximum player
    /// </summary>
    public byte maxPlayer;
    
    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    void Update()
    {
        
    }


    public override void OnConnectedToMaster()
    {
        print("Connected");
        PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.LocalPlayer.NickName = DataManager.Instance.playerName;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("" + Random.Range(1000, 9999));
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        roomPanel.SetActive(true);
        print("RoomJoined " + PhotonNetwork.CurrentRoom.Name);
        pv.RPC("RPC_Join", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer);

    }

    public void OnclickedEnterButton() {
        DataManager.Instance.playerName = playerName.text;
        EnterNamePanel.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    [PunRPC]
    public void RPC_Join(Player player) {
        GameObject playerobj = Instantiate(playerPrefab, roomlayoutGroup);
        playerobj.GetComponent<PlayerPrefabs>().playerName.text = player.NickName;

        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayer) {

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
