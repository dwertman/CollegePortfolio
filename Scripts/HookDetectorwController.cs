using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetectorwController : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hookable")
        {
            //Debug.Log("Hello World");
            player.GetComponent<GHwController>().hooked = true;
            player.GetComponent<GHwController>().hookedObj = other.gameObject;
            
        }
    }
}
