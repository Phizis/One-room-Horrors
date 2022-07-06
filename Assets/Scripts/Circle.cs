using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Circle : MonoBehaviour
{
    public float timer = 0f;

    public GameObject activeObject;
    public GameObject deactiveObject;

    public GameObject ghostIdle;

    public GameObject defenceCircle;

    AudioSource triggerSounds;
    public AudioClip drawSound;
    public AudioClip visionSound;

    bool bookPlaced = false;
    bool updated = true;
    bool itemPlaced = false;
    void Start()
    {
        triggerSounds = GetComponent<AudioSource>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Chalk")
        {
            triggerSounds.PlayOneShot(drawSound);
            defenceCircle.SetActive(true);
            GetComponent<NavMeshObstacle>().enabled = true;
        }        
    }
    private void Update()
    {
        if ( !itemPlaced && (GameObject.FindGameObjectsWithTag("Chalk").Length == 1 ||
             GameObject.FindGameObjectsWithTag("Candle").Length == 1 || GameObject.FindGameObjectsWithTag("Book").Length == 1))
        {
            activeObject.SetActive(true);
            if (SceneManager.GetActiveScene().buildIndex != 3)
                deactiveObject.SetActive(false);
            itemPlaced = true;
        }

        if ( !bookPlaced && GameObject.FindGameObjectsWithTag("Book").Length == 1 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            bookPlaced = true;
            ghostIdle.SetActive(true);
            //triggerSounds.volume = 0.7f;
            triggerSounds.PlayOneShot(visionSound);            
        }

        if (bookPlaced && updated)
        {
            timer += Time.deltaTime;
            if (timer >= 6f)
            {
                ghostIdle.SetActive(false);
                updated = false;
            }
        }
    }
}
