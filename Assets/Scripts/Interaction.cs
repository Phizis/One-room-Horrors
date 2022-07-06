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
    public Image fill;

    public GameObject noteScreen;
    private bool notesEnable = false;

    Animator anim;
    public AudioClip currentSound1;
    public AudioClip currentSound2;
    AudioSource interactionSound;
    

    public GameObject candleLight;
    private bool lightEnable = false;

    float currentValue = 0.1f, maxValue = 14.9f;

    int reading_count = 0;
    public GameObject loadingScreen;
    public GameObject morning;
    bool whisperEnable = true;
    bool reload = false;
    bool getKey = true;

    public GameObject raven;
    public GameObject nextPartCollider;

    public GameObject screamer;
    public GameObject screamerSource;

    public GameObject enemy;
    public void NotePause()
    {
        noteScreen.SetActive(true);
        interactionSound.Play();
        notesEnable = true;
        Time.timeScale = 0f;
    }
    public void NoteResume()
    {
        noteScreen.SetActive(false);
        notesEnable = false;
        Time.timeScale = 1f;
    }

    void Start()
    {
        interactionSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        if (tag == "NextPartTrigger")
            fill.fillAmount = currentValue / maxValue;
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
                    interactionSound.Play();
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            if (tag == "BookOpened")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    anim.Play("OpenBook");
                    tag = "Book";
                    GetComponent<AudioSource>().Play();
                    raven.SetActive(true);
                    GetComponent<BoxCollider>().enabled = false;
                    nextPartCollider.SetActive(true);
                }
            }

            if (tag == "Note")
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
                {
                    if (notesEnable)
                    {
                        NoteResume();
                    }
                    else 
                    { 
                        NotePause();   
                    }                        
                }
            }
            
            if (tag == "NextPartTrigger")
            {
                fill.enabled = true;                

                if (Input.GetKeyDown(KeyCode.E) && !reload)
                {
                    getKey = true;

                    if (whisperEnable)
                    {
                        interactionSound.PlayOneShot(currentSound1);
                        whisperEnable = false;
                    }
                    else
                        interactionSound.UnPause();
                }

                if (Input.GetKey(KeyCode.E) && !reload && getKey)
                {
                    Add();
                }

                if (Input.GetKeyUp(KeyCode.E) && !reload )
                {
                    interactionSound.Pause();                    
                }

                if (Input.GetKeyUp(KeyCode.E) && reload)
                {
                    interactionSound.PlayOneShot(currentSound2);
                    reload = false;
                }
            }

            if (tag == "LightUp")
            {
                if (Input.GetKeyDown(KeyCode.E))
                    if (lightEnable)                    
                    {
                        candleLight.SetActive(false);
                        interactionSound.PlayOneShot(currentSound2);
                        lightEnable = false;
                    }
                    else
                    {
                        candleLight.SetActive(true);
                        interactionSound.PlayOneShot(currentSound1);
                        lightEnable = true;
                    }
            }
        }      
    }  
    

    void OnMouseExit()
    {        
        myImage.enabled = false;
        
        if (tag == "NextPartTrigger")
        {
            fill.enabled = false;
            interactionSound.Pause();
            getKey = false;
        }
    }

    public void Add()
    {
        currentValue += Time.deltaTime;

        if (currentValue > maxValue)
            currentValue = maxValue;

        fill.fillAmount = currentValue / maxValue;

        if (currentValue == maxValue)
        {
            reading_count++;
            currentValue = 0.1f;
            interactionSound.Stop();
            reload = true;
            whisperEnable = true;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tag)
        {
            gameObject.SetActive(false);
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

        if (SceneManager.GetActiveScene().buildIndex == 2 && reading_count == 2)
        {
            screamerSource.SetActive(false);
            screamer.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 && reading_count == 1)
        {
            screamer.SetActive(true);
        }

        if (reading_count == 3)
        {
            Destroy(enemy);
            fill.enabled = false;
            morning.SetActive(true);
            morning.GetComponent<Light>().intensity += Time.deltaTime*0.1f;

            if (morning.GetComponent<Light>().intensity >= 1f)
                loadingScreen.SetActive(true);
        }
    }
}
