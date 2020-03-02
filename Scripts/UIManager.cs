using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject catchCanvas;
    public Text score;
    public Text fishName;
    public Text fishRarity;
    public Text fishSize;
    public Text timeCaught;
    public GameObject panel;
    public GameObject player;
    public GameObject fish;
    public Camera canvasCamera;
    bool changed = false;
    //public FishData fishData;
    public bool fishCaught;
    public bool fishNotNull;
    

    private void Awake()
    {
        
        player = transform.parent.gameObject;
        //Debug.Log("UIManager: Awake -- " + player);
        score = GameObject.Find("Score").GetComponent<Text>();
        fishName = GameObject.Find("fishName").GetComponent<Text>();
        fishRarity = GameObject.Find("fishRarity").GetComponent<Text>();
        fishSize = GameObject.Find("fishSize").GetComponent<Text>();
        timeCaught = GameObject.Find("timeCaught").GetComponent<Text>();
        
        catchCanvas = GameObject.Find("Canvas");
        canvasCamera = GameObject.Find("HoldFishCamera").GetComponent<Camera>();
        catchCanvas.SetActive(false);

    }


    // Start is called before the first frame update


    void Start()
    {
        
        //changed = false;
        fishNotNull = false;
        //Debug.Log(fish);
        //Debug.Log(catchCanvas.enabled);

    }

    public void UpdateDisplayUI(FishData fishData)
    {
        score.text += " " + fishData.FishValue;
        fishName.text += " " + fishData.FishSpecies;
        fishRarity.text += " " + fishData.FishRarity;
        fishSize.text += " " + fishData.FishSize;
        timeCaught.text += Time.realtimeSinceStartup.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UIManager: is this the local player?" + player.GetComponent<NetworkIdentity>().hasAuthority);
        if (fishCaught == true && changed == false && player.GetComponent<NetworkIdentity>().hasAuthority/*fishCaught*/)
        {
            
            player.transform.GetChild(2).gameObject.SetActive(false);
            catchCanvas.SetActive(true);
            catchCanvas.GetComponent<Canvas>().enabled = true;
            //Debug.Log("UIManager Update: " + catchCanvas.GetComponent<Canvas>().enabled);
            changed = true;
            fishCaught = false;

            //Debug.Log(fish);
            fish = (GameObject)Instantiate(fish, new Vector3(canvasCamera.transform.position.x, canvasCamera.transform.position.y, 5f), canvasCamera.transform.rotation);
            fish.layer = 5;
            fish.transform.localScale = new Vector3(1, 1, 2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fishNotNull = true;
            //Debug.Log("UIManager Update: " + catchCanvas.GetComponent<Canvas>().enabled);
            //fish.
            // Debug.Log(fish);
           

        }
        //Debug.Log(fish);
        //if (fishCaught == true && changed == false/*fishCaught*/)
        //{
        //    //fishCaught = false;
        //    player.SetActive(false);
        //    c.enabled = true;
        //    canvasCamera.enabled = true;
        //    changed = true;

        //    //Debug.Log(fish);
        //    fish = (GameObject)Instantiate(fish, new Vector3(canvasCamera.transform.position.x, canvasCamera.transform.position.y, 5f), canvasCamera.transform.rotation);
        //    fish.layer = 5;
        //    fish.transform.localScale = new Vector3(1, 1, 2);
        //    Cursor.lockState = CursorLockMode.None;
        //    Cursor.visible = true;
        //    fishNotNull = true;

        //   // Debug.Log(fish);


        //}

        //if (fish != null)
        // {
        StartCoroutine("rotateFish");
        //Debug.Log("UIManager Update: " + catchCanvas.GetComponent<Canvas>().enabled);
        ///}

        //if (Input.GetKeyDown(KeyCode.D))
        //{

        //    Destroy(fish);
        //    fish = null;


        //}
        //} else {
        //    player.SetActive(true);
        //    canvasCamera.enabled = false;
        //    c.enabled = false;
        //    changed = !changed;
        //}
    }

    public void closeUI()
    {
        score.text = "Score:";
        fishName.text = "Name:";
        fishRarity.text = "Rarity:";
        timeCaught.text = "Time Caught:";
        fishSize.text = "Size:";
        StopCoroutine("rotateFish");
        //Debug.Log("Close it");
        fishCaught = false;
        changed = false;
        catchCanvas.SetActive(false);
        //canvasCamera.enabled = false;
        //Debug.Log("UIManager Update: " + catchCanvas.GetComponent<Canvas>().enabled);
        Destroy(fish);
        fish = null;
        //Debug.Log(player.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
        Destroy(player.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
        player.transform.GetChild(2).gameObject.SetActive(true);
        fishNotNull = false;


    }

    IEnumerator rotateFish()
    {
        //fish = null;
        if(fishNotNull)
        {
            //Debug.Log(fish);
            yield return new WaitForSeconds(0.01f);
            fish.transform.Rotate(0, 10 * Time.deltaTime, 0);
        }
        else
        {
            //Debug.Log("Frick");
        }
            
        
        
    }
}
