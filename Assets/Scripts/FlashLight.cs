using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour

    
{
    Light flashLight;
    public AudioClip flashLightSound;
    // Start is called before the first frame update
    void Start()
    {
        flashLight = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("f"))
        {
            flashLight.enabled = !flashLight.enabled;
            GetComponent<AudioSource> ().PlayOneShot (flashLightSound);
        }        
    }
}
