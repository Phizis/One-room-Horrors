using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public AudioClip ScreamerSound;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void InteractionTrigger()
    {

        GetComponent<AudioSource>().PlayOneShot(ScreamerSound);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            InteractionTrigger();
    }    
}
