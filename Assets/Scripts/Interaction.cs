using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject myPlayer;
    public float interactionDistance = 2f;
    private float distance;

    public Transform arm;

    public Image myImage;

    public Animator anim;
    public AudioClip ScreamerSound;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(myPlayer.GetComponent <Transform>().position, transform.position);
        
        if (distance < interactionDistance)
        {
            myImage.enabled = true;
            if (tag == "Item" || tag == "Book" || tag == "Candle" || tag == "Chalk")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    transform.position = arm.position;
                    transform.rotation = arm.rotation;
                    transform.SetParent(arm);
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            if (tag == "BookOpened")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.Play("OpenBook");
                    tag = "Inactive";
                    GetComponent<AudioSource>().PlayOneShot(ScreamerSound);
                }
            }
        }
        else
        {
            myImage.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseExit()
    {
        myImage.enabled = false;   
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            transform.parent = null;
            GetComponent<Rigidbody>().isKinematic = false;
        }
            
    }
}
