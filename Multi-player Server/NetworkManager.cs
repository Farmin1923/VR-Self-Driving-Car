using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The goal of this script is to connect automatically at the start of the game on the network 
public class NetworkManager : MonoBehaviourPunCallbacks //To know if we are connected to the server we want to have a function
                                                        //that is being called when we are connected we are using this attribute. We will be able to override some of the initial
                                                        //function that are being called when we are connected to server, when somebody join the server, when we join a room
{
    // Start is called before the first frame update
    void Start()
    {
        //at the start of the game we want to call it
        ConnectToServer();
    }

    // Update is called once per frame
    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Serve...");
    }
    public override void OnConnectedToMaster()//When we are connected to the master this function will be called
    {
        Debug.Log("Connected To server..");
        base.OnConnectedToMaster();
        RoomOptions roomOption = new RoomOptions();
        roomOption.MaxPlayers = 10;
        roomOption.IsVisible = true; //So that all player will be able to see this room
        roomOption.IsOpen = true; // So that all player will be able to join this room even after it is created
        PhotonNetwork.JoinOrCreateRoom("Room 1", roomOption, TypedLobby.Default); //We need to be in the same room with all the member to share the data
    }
    //For the connection of the server we can override a certain function to know if we join the room or not
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the room..");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //If other player joined the room //the new player viable has some data that we can use
    {
        Debug.Log("A new player joined the room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

}