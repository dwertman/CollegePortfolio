using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //is this my own local PlayerObject
        if (isLocalPlayer == false)
        {
            //This object belongs to another player
            return;
        }


        //since playerObject is invis and not part of world
        //give me something to move around
        Debug.Log("PlayerObject:Start -- Spawning my own personal unit");

        //instantiate only creates object on local com
        //even if it has network identity it still will not exist
        //on network and therefore not on any other client 
        //unless NetworkServer.Spawn is called on this object

        //Instantiate(PlayerUnitPrefab);

        //Command the server to spawn our unit
        CmdSpawnMyUnit();
    }

    public GameObject PlayerUnitPrefab;
    // SyncVars are variables where if their value changes on server, then all clients are automatically informed of the new value
    [SyncVar(hook = "OnPlayerNameChanged")]
    public string PlayerName = "Anonymous";//grab from database
    // Update is called once per frame
    void Update()
    {
        //Update runs on every computer whether or not they own this particualr player object   
        if (isLocalPlayer == false)
        {
            //This object belongs to another player
            return;
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    //Spacebar was hit -- we could instruct the server
        //    //to do something with our unit
        //    CmdSpawnMyUnit();
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    string n = "Quill" + Random.Range(1, 100);

        //    Debug.Log("Sending server request to change player name " + n);
        //    CmdChangePlayerName(n);
        //}
    }

    void OnPlayerNameChanged(string newName)
    {
        Debug.Log("OnPlayerNameChanged: OldName: " + PlayerName + "NewName: " + newName);

        PlayerName = newName;

        gameObject.name = "PlayerConnectionObject [" + newName + "]";
    }

    //GameObject myPlayerUnit;
    [Command]
    void CmdSpawnMyUnit()
    {

        // We are guaranteed to be on server right now.
        GameObject go = Instantiate(PlayerUnitPrefab);
        //go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        //Now that obj exists on server propigate it to all
        //clients (and wire up the NetworkIdentity)
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }

    [Command]
    void CmdChangePlayerName(string n)
    {
        Debug.Log("CmdChangePlayerName: " + n);
        PlayerName = n;

        //Maybe we should check name doesn't have any blacklisted words in it?
        //If so, ignore request or call rpc with orig name

        //Tell all clients what player's name now is
        //RpcChangePlayerName(PlayerName);
    }
}
