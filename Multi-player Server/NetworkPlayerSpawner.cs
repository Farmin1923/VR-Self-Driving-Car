using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
{
    //We want to spwaned a player and on the left room we want to disable the player 
    private GameObject spawnedPlayerPrefab;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //If other player joined the room //the new player viable has some data that we can use
    {
        base.OnPlayerEnteredRoom(newPlayer);
        spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, transform.rotation, 0);
        //spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", new Vector3(-3142.959f, -3.709437f, 2129.648f), transform.rotation, 0);
        //Debug.Log(transform.position);
        //PhotonNetwork.Instantiate(Path.Combine)


    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }
}

