using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
       /* if (GameObject.FindGameObjectsWithTag("Item").Length == 0)
        {
            anim.Play("OpenBook");            
        }*/
    }
}
