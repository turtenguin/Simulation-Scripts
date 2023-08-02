using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakWall : MonoBehaviour
{

    //The inputs for the project
    private StarterAssetsInputs inputs;
    //The good wall
    public GameObject wall;
    //The destroyed wall
    public GameObject brokeWall;
    //Reference to the particle system
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
        particles.Play();
        wall.SetActive(false);
        brokeWall.SetActive(true);
        this.enabled = false;
    }
}
