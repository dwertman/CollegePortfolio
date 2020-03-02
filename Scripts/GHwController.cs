//Grappling hook handles trnalsation between hook and player. Uses controller input
//Created by Dillon Wertman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GHwController : MonoBehaviour
{
    public GameObject hook; 
    public GameObject hookHolder;
    //public UIManager UIManager;
    public float hookTravelSpeed;
    public float playerTravelSpeed;

    public static bool fired;
    public bool hooked;
    public GameObject hookedObj;
    public Player playerScript;
    private Fish fishData;
    private bool pressedFireButton;
    public float maxDistance;
    private float currentDistance;

    PlayerControls controls;

    private bool grounded;
    //int playerScore;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Fire.performed += ctx => Fired();

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Fired()
    {
        fired = true;
        //Debug.Log("fired");
            
    }

    private void Start()
    {
        playerScript = gameObject.GetComponent<Player>();
        //UIManager = null;
    }


   

    private void Update()
    {




        if (fired)
        {
            //display line renderer
            LineRenderer line = hook.GetComponent<LineRenderer>();
            line.positionCount = 2;
            line.SetPosition(0, hookHolder.transform.position);
            line.SetPosition(1, hook.transform.position);

        }
        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed); //fire hook
            currentDistance = Vector3.Distance(transform.position, hook.transform.position); //find current distance of hook from player

            if (currentDistance >= maxDistance) //return hook after it reaches beyond max distance
                ReturnHook();
        }

        if (hooked == true && fired == true && hookedObj != null)
        {
            //hook.transform.parent = hookedObj.transform;
            hookedObj.transform.parent = hook.transform;
            hookedObj.GetComponent<AIMove>().enabled = false;
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, playerTravelSpeed * Time.deltaTime);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            //this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 2)
            {
                //StartCoroutine("caught");
                caught(hookedObj);
        
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
        }
    }

    //IEnumerator caught(GameObject fish)
    //{

    //    yield return new WaitForSeconds(0.1f);
    //    fish = hookedObj;
    //    Debug.Log(fish);
    //    Debug.Log("Display release screen");

    //}

    void caught(GameObject fish)
    {
        
        fish.tag = "Hooked";
        //Debug.Log("GrapplingHook: " + fish);
        //UIManager.fishCaught = true;
        //UIManager.fish = Instantiate(fish, fish.transform);
        //Debug.Log("GrapplingHook: UIManager.fish: " + UIManager.fish);
        //Destroy(fish);
        //fish = null;
        playerScript.addScore(fish.GetComponent<Fish>().fishData.FishValue);
        //Debug.Log(playerScript.getScore());
        Destroy(hookedObj);
        //hookedObj = null;
        //Debug.Log(fish);
        ReturnHook();
        //Debug.Log("Hooke retuirned");

    }

    void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        hook.transform.localScale = new Vector3(1, 1, 1);
        LineRenderer line = hook.GetComponent<LineRenderer>();
        line.positionCount = 0;
        fired = false;
        hooked = false;
        
    }

    void CheckifGrounded()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = new Vector3(0, 1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
