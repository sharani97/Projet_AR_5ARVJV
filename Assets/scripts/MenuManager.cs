using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MenuManager : MonoBehaviourPunCallbacks {
    [Header(" — -Menus — -")]
    public GameObject mainMenu;
    public GameObject lobbyMenu;
    [Header(" — -Main Menu — -")]
    public Button createRoomBtn;
    public Button joinRoomBtn;
    [Header(" — -Lobby Menu — -")]
    public TMP_Text roomName;
    public TMP_Text playerListText;
    public string playerList="";
    public Button startGameBtn;
    public string GameName= "Game";

    private void Start() {
        createRoomBtn.interactable = false;
        joinRoomBtn.interactable = false;
        SetMenu(mainMenu);
    }
    public override void OnConnectedToMaster() {
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;
    }
    void SetMenu(GameObject menu) {
        mainMenu.SetActive(false);
        lobbyMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void OnCreateRoomBtn(TMP_Text roomNameInput) {
      Debug.Log("Creating Room : " + roomNameInput.text); 
      NetworkManager.instance.CreateRoom(roomNameInput.text);
      roomName.SetText(roomNameInput.text);
    }
    public void OnJoinRoomBtn(TMP_Text roomNameInput) {
        NetworkManager.instance.JoinRoom(roomNameInput.text);
        Debug.Log("Join Room : " + roomNameInput.text);
        roomName.SetText(roomNameInput.text);
    }
    public void OnPlayerNameUpdate(TMP_Text playerNameInput) {
        PhotonNetwork.NickName = playerNameInput.text;
        //Debug.Log("my playerN:" + PhotonNetwork.NickName);
    }
    public override void OnJoinedRoom() {
        SetMenu(lobbyMenu);
        Debug.Log("Room joined, updating LobbyUI");
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) {
        UpdateLobbyUI();
    }
    

    [PunRPC]
    public void UpdateLobbyUI() {
        Debug.Log("Updating lobby!");
        playerList = "";
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList) {
            if (player.IsMasterClient) {
                Debug.Log("Master player is:" + player.NickName);
                playerList += player.NickName + " (Host)\n";
                //Debug.Log("Player list (master) : " + playerList)
            }
            else { playerList += player.NickName + "\n"; }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Found Photon Master !");
            startGameBtn.interactable = true;
        }
        else {
            startGameBtn.interactable = false;
        }
        //Debug.Log("Player list : " + playerList);
        playerListText.SetText(playerList);
    }
    public void OnLeaveLobbyBtn() {
        PhotonNetwork.LeaveRoom();
        SetMenu(mainMenu);
    }
    public void OnStartGameBtn() {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, GameName);
    }
}