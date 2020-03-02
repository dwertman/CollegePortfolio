using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


//Handles Ai spwaning, adapted from The Messy Coder's AI Fish tutorial
//modified by Dillon Wertman

[System.Serializable]
public class AIObjects
{
    //variables
    public string AIGroupName { get { return m_aiGroupName; } }
    public GameObject objectPrefab { get { return m_prefab; } }
    public int maxAI { get { return m_maxAI; } }
    public int spawnRate {get { return m_spawnRate; } }
    public int spawnAmount { get { return m_maxSpawnAmount; } }
    public bool randomizeStats {  get { return m_randomizeStats; } }
    public bool enableSpawner { get { return m_enableSpawner; } }

    //serialize private variables
    [Header("AI Group Stats")]
    [SerializeField]
    private string m_aiGroupName;
    [SerializeField]
    private GameObject m_prefab;
    [SerializeField]
    [Range(0f, 250f)]
    private int m_maxAI;
    [SerializeField]
    [Range(0f, 50f)]
    private int m_spawnRate;
    [SerializeField]
    [Range(0f, 20f)]
    private int m_maxSpawnAmount;

    [Header("Main Settings")]
    [SerializeField]
    private bool m_enableSpawner;
    [SerializeField]
    private bool m_randomizeStats;

    public AIObjects(string name, GameObject obj, int maxAI, int spawnRate, int spawnAmt, bool random)
    {
        this.m_aiGroupName = name;
        this.m_prefab = obj;
        this.m_maxAI = maxAI;
        this.m_spawnRate = spawnRate;
        this.m_maxSpawnAmount = spawnAmt;
        this.m_randomizeStats = random;
    }

    public void setValues(int MaxAI, int spawnRate, int SpawnAmount)
    {
        this.m_maxAI = MaxAI;
        this.m_spawnRate = spawnRate;
        this.m_maxSpawnAmount = SpawnAmount;
    }

}


public class AISpawner : MonoBehaviour
{
    //variables
    public List<Transform> Waypoints = new List<Transform>();//2b removed

    public float spawnTimer { get { return m_SpawnTimer; } }
    public Vector3 spawnArea { get { return m_SpawnArea; } }
    [Header("Global Stats")]
    [Range(0f, 600f)]
    [SerializeField]
    private float m_SpawnTimer;
    [SerializeField]
    private Color m_SpawnColor = new Color(1.000f, 0.000f, 0.000f, 0.300f);
    [SerializeField]
    private Vector3 m_SpawnArea = new Vector3(20f, 10f, 20f);
    /// with Linq
    //public Transform[] WaypointsLinq;

    //array of new class
    [Header("AI Group Settings")]
    public AIObjects[] AIObject = new AIObjects[5];

    //empty game object for AI
    private GameObject m_AIGroupSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
        RandomizeGroups();
        CreateAIGroups();
        InvokeRepeating("SpawnNPC", 0.5f, spawnTimer);
        //GetWaypointsLinq();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //handle NPC spawning
    void SpawnNPC()
    {
        for(int i = 0; i < AIObject.Count(); i++)
        {
            //is spawner enabled?
            if (AIObject[i].enableSpawner && AIObject[i].objectPrefab != null)
            {
                //max NPCs for group?
                GameObject tempGroup = GameObject.Find(AIObject[i].AIGroupName);
                if(tempGroup.GetComponentInChildren<Transform>().childCount < AIObject[i].maxAI)
                {
                    //spawn random number of NPCs, 0 to max spawn amt
                    for(int j = 0; j < Random.Range(0, AIObject[i].spawnAmount); j++)
                    {
                        //spawn at random rotation
                        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
                        //create the object
                        GameObject tempSpawn = Instantiate(AIObject[i].objectPrefab, RandomPosition(), randomRotation);
                        //make spawned object child of group
                        tempSpawn.transform.parent = tempGroup.transform;
                        //Add move script to NPC
                        tempSpawn.AddComponent<AIMove>();
       
                    }
                }
            }
        }
    }


    //random position for spawns in spawn area
    public Vector3 RandomPosition()
    {
        //get random position within spawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            Random.Range(-spawnArea.y, spawnArea.y),
            Random.Range(-spawnArea.z, spawnArea.z)
            );
        randomPosition = transform.TransformPoint(randomPosition * .5f);
        return randomPosition;
    }

    //get random waypoint
    public Vector3 RandomWaypoint()
    {
        int randomWP = Random.Range(0, (Waypoints.Count - 1));
        Vector3 randomWaypoint = Waypoints[randomWP].transform.position;
        return randomWaypoint;
    }

    //Randomize AI group settings
    void RandomizeGroups()
    {
        for(int i = 0; i < AIObject.Count(); i++)
        {
            if (AIObject[i].randomizeStats)
            {
                //AIObject[i] = new AIObjects(AIObject[i].AIGroupName, AIObject[i].objectPrefab, Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10), AIObject[i].randomizeStats);
                AIObject[i].setValues(Random.Range(1, 30), Random.Range(1, 20), Random.Range(1, 10));
            }
        }

    }

    //void GetWaypointsLinq()
    //{
    //    WaypointsLinq = transform.GetComponentsInChildren<Transform>().Where(c => c.gameObject.tag == "waypoint").ToArray();
    //}


//Method for creating empty world object groups
void CreateAIGroups()
    {
        for (int i = 0; i < AIObject.Count(); i++)
        {
            
            m_AIGroupSpawn = new GameObject(AIObject[i].AIGroupName);
            m_AIGroupSpawn.transform.parent = this.gameObject.transform;
        }
    }

    //2b removed
    void GetWaypoints()
    {
        //standard library list
        //loop through nested children
        Transform[] wpList = transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < wpList.Length; i++)
        {
            if (wpList[i].tag == "waypoint")
            {
                Waypoints.Add(wpList[i]); //add to list
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_SpawnColor;
        Gizmos.DrawCube(transform.position, spawnArea);
    }
}
