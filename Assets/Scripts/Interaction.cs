using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    public GameObject myPlayer;
    public float interactionDistance = 2f;
    private float distance;

    public Transform arm;
    public bool draggable = false;

    public Image myImage;

    public GameObject noteScreen;
    private bool notesEnable = false;

    public Animator anim;
    public AudioClip currentSound1;
    public AudioClip currentSound2;

    public GameObject candleLight;
    private bool lightEnable = false;

    public GameObject radialBar;

    public void notePause()
    {
        noteScreen.SetActive(true);
        notesEnable = true;
        Time.timeScale = 0f;
    }
    public void noteResume()
    {
        noteScreen.SetActive(false);
        notesEnable = false;
        Time.timeScale = 1f;
    }

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
            if (draggable)
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
                    GetComponent<AudioSource>().PlayOneShot(currentSound1);
                }
            }

            if (tag == "Note")
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (notesEnable)
                    {
                        noteResume();
                    }
                    else 
                    { 
                        notePause();   
                    }                        
                }
            }
            
            if (tag == "NextPartTrigger")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    radialBar.SetActive(true);                    
                }
                NextSceneTrigger();
            }

            if (tag == "LightUp")
            {
                if (Input.GetKeyDown(KeyCode.E))
                    if (lightEnable)                    
                    {
                        candleLight.SetActive(false);
                        GetComponent<AudioSource>().PlayOneShot(currentSound2);
                        lightEnable = false;
                    }
                    else
                    {
                        candleLight.SetActive(true);
                        GetComponent<AudioSource>().PlayOneShot(currentSound1);
                        lightEnable = true;
                    }
            }
        }
        else
        {            
            myImage.enabled = false;
            if (tag == "NextPartTrigger")
                radialBar.SetActive(false);
        }        
    }  
    
    void NextSceneTrigger()
    {
        if (Input.GetKeyDown(KeyCode.E))

        radialBar.GetComponent<RadialBarFiller>().enabled = true;
            
    }

    void OnMouseExit()
    {        
        myImage.enabled = false;
        if (tag == "NextPartTrigger")
            radialBar.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (draggable)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                transform.parent = null;
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
               
    }
}
