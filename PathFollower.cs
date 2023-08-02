using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    //Speed of rotation (fraction of distance turned after one second)
    public float rotSpeed = 1;
    //Acceleration for normal movement changes
    public float acceleration = 1;
    //Acceleration when stopping
    public float stopAcceleration = 5;
    //Threshold speed which is considered stopped
    public float stoppedSpeed = .5f;
    //Array containing waypoints
    public Waypoint[] waypoints;
    //Distance threshold for when the object is considered to have reached a waypoint
    public float targetDistance = 2;

    //Reference to the animator
    private Animator animator;
    //Which waypoint the object is currently moving to or stopped at
    private int nextPoint = 0;
    //Speed of the object at all times
    private float speed = 0;
    //True when waiting at a waypoint (including stopping time)
    private bool waiting = false;
    //True when stopping at a waypoint
    private bool stopping = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!waiting)
        {
            //Update rotation
            Quaternion targetRot = Quaternion.LookRotation(waypoints[nextPoint].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);

            //Increase or decrese speed
            if(speed < waypoints[nextPoint].speed)
            {
                speed += acceleration * Time.deltaTime;
            }
            else
            {
                speed -= acceleration * Time.deltaTime;
            }

            //Update position
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        //If waypoint reached, switch to next waypoint or begin stopping sequence if there is a hold at current waypoint
        if(!waiting && Vector3.Distance(transform.position, waypoints[nextPoint].transform.position) < targetDistance)
        {
            waiting = true;
            if(waypoints[nextPoint].stay > 0)
            {
                stopping = true;
            }
            Invoke("Continue", waypoints[nextPoint].stay);
        }

        //If stopping update speed and position
        if (stopping)
        {
            speed -= stopAcceleration * Time.deltaTime;
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            //Check of a low enough speed has been reached to stop
            if(speed <= stoppedSpeed)
            {
                speed = 0;
                stopping = false;
            }
        }

        //Update animator parameter
        animator.SetFloat("Speed", speed);
    }

    private void Continue()
    {
        //Set up for the next waypoint
        waiting = false;
        nextPoint++;
        if(nextPoint >= waypoints.Length)
        {
            nextPoint = 0;
        }
    }
}
