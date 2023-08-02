using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class boomTrigger : MonoBehaviour
{
    //The animator of this object
    public Animator animator;
    //The inputs for the project
    private StarterAssetsInputs inputs;
    //The bomb object to be destroyed
    public GameObject bomb;
    //Reference to the particle system of the explosion
    public ParticleSystem particles;

    private void Awake()
    {
        inputs = new StarterAssetsInputs(); 
        inputs.Enable();
        //Register onBoom to the explosion input
        inputs.Player.boom.performed += onBoom;
    }
    private void onBoom(InputAction.CallbackContext value)
    {
        //Destroy bomb, play the particles and animation, and then disable this component
        GameObject.Destroy(bomb);
        particles.Play();
        animator.SetTrigger("boom");
        this.enabled = false;
    }
    
}
