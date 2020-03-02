/**AI Move is used for fish movement
 * Adapted from The Messy Coder's Fish AI Tutorial on youtube
 * Modified by Dillon Wertman
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private AISpawner m_AIManager;

    private bool m_hasTarget = false;
    private bool m_isTurning;

    private Vector3 m_wayPoint;
    private Vector3 m_lastWaypoint = new Vector3(0f, 0f, 0f);

    private Animator m_animator;
    private float m_speed;
    private float m_scale;

    private Collider m_collider;
    private RaycastHit m_hit;

    // Start is called before the first frame update
    void Start()
    {
        m_AIManager = transform.parent.GetComponentInParent<AISpawner>();
        m_animator = GetComponent<Animator>();

        SetUpNPC();
    }

    void SetUpNPC()
    {
        //m_scale = UnityEngine.Random.Range(0f, 4f);
        transform.localScale += new Vector3(m_scale * 1.5f, m_scale, m_scale);
        if(transform.GetComponent<Collider>() != null && transform.GetComponent<Collider>().enabled == true)
        {
            m_collider = transform.GetComponent<Collider>();
        }
        else if (transform.GetComponentInChildren<Collider>() != null && transform.GetComponentInChildren<Collider>().enabled == true)
        {
            m_collider = transform.GetComponentInChildren<Collider>();
        }
    }
    


    // Update is called once per frame
    void Update()
    {
        //if there is a waypoint to be moved to the NPC must move there
        if (!m_hasTarget)
        {
            m_hasTarget = CanFindTarget();
        }
        else
        {
            //rotate to face waypoint
            RotateNPC(m_wayPoint, m_speed);
            //move NPC in straight line toward waypoint
            transform.position = Vector3.MoveTowards(transform.position, m_wayPoint, m_speed * Time.deltaTime);
            //check if collided then turn to new waypoint
            CollidedNPC();
        }

        //reset target when NPC reaches waypoint
        if(transform.position == m_wayPoint)
        {
            m_hasTarget = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "bobber")
        {
            Debug.Log(collision.gameObject.tag);
            collision.gameObject.tag = "taken";
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    //rotate npc to face new waypoint
    private void RotateNPC(Vector3 waypoint, float currentSpeed)
    {
        //get random speed up for turn
        float TurnSpeed = currentSpeed * UnityEngine.Random.Range(1f, 3f);

        //get new direction to look at for target
        Vector3 LookAt = waypoint - this.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime);
    }

    void CollidedNPC()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, transform.localScale.z))
        {
            //ignore raycast when htting own collider or waypoint
            if(hit.collider == m_collider | hit.collider.tag == "waypoint" | hit.collider.tag == "bobber" | hit.collider.tag == "taken")
            {
                return;
            }
            //otherwise have NPC turn randomly
            int randomNum = UnityEngine.Random.Range(1, 100);
            if (randomNum < 40)
                m_hasTarget = false;

            //debug
            //Debug.Log(hit.collider.transform.parent.name + " " + hit.collider.transform.parent.position);
        }
    }

    Vector3 GetWayPoint(bool isRandom)
    {
        //if true get random position
        if (isRandom)
        {
            return m_AIManager.RandomPosition();
        }
        //else get from list of waypoint objects
        else
        {
            return m_AIManager.RandomWaypoint();
        }
    }

    private bool CanFindTarget(float start = 1f, float end = 7f)
    {
        m_wayPoint = m_AIManager.RandomWaypoint();
        //don't go to waypoint twice
        if(m_lastWaypoint == m_wayPoint)
        {
            //get new waypoint
            m_wayPoint = GetWayPoint(true);
            return false;

        }
        else
        {
            //set new waypoint as last waypoint
            m_lastWaypoint = m_wayPoint;
            //get random speed for movement and animation
            m_speed = UnityEngine.Random.Range(start, end);
            m_animator.speed = m_speed;
            //set bool true since WP is found
            return true;
        }
    }
}
