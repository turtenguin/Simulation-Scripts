using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    /*
     * This script is not performant and is only meant for creating animations. Do not use in final build.
     */

    //True when explosion is triggered
    public bool boom = false;
    //Information about the strength of the explosion
    public float force;
    public Vector3 position;
    public float radius;

    void Update()
    {
        //If the explosion was triggered
        if (boom)
        {
            //For every child, add the explosive force
            foreach(Transform child in transform)
            {
                if(child.transform != transform)
                {
                    child.GetComponent<Rigidbody>().AddExplosionForce(force, position, radius);
                }
            }

            boom = false;
        }
    }
}
