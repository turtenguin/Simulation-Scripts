using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Animations;
using System.IO;

public class XRRecorder : MonoBehaviour
{
    /*
     * Script expanded from Unity GameObjectRecorder example
     */

    //The number of the clip to be played or recorded
    public int clipNumber;
    //Array containing the animation clips to be played or recorded into
    public AnimationClip[] clips;
    //True if recording
    public bool record = false;
    //True if playing
    public bool play = false;
    //Internal variable used for ending recording
    private bool recording = false;
    //The animator of this object
    private Animator animator;
    //GameObjectRecorder used for recording
    private GameObjectRecorder m_Recorder;
    

    void Start()
    {
        animator = GetComponent<Animator>();

        //If both record and play are selected, default to record
        if(record && play)
        {
            play = false;
        }

        //If playing, disable all parent constraints in children and set animation
        if (play)
        {
            ParentConstraint[] parentConstraints = GetComponentsInChildren<ParentConstraint>();
            for(int i = 0; i < parentConstraints.Length; i++)
            {
                parentConstraints[i].enabled = false;
            }
            animator.SetInteger("Clip", clipNumber);
        }
        else
        {
            //Otherwise disable the animator and leave the parent constraints
            animator.enabled = false;
        }

        // Create recorder and record the script GameObject.
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (record)
        {
            if (clips[clipNumber] == null)
                return;

            // Take a snapshot and record all the bindings values for this frame.
            m_Recorder.TakeSnapshot(Time.deltaTime);

            //Set marker to see if record is changed to false
            recording = true;
        }
        else if(recording == true)
        {
            //If record was toggled off, disable this component
            this.enabled = false;
        }
    }

    void OnDisable()
    {
        if (clips[clipNumber] == null)
            return;

        if (m_Recorder.isRecording)
        {
            // Save the recorded session to the clip.
            m_Recorder.SaveToClip(clips[clipNumber]);
        }
    }
}