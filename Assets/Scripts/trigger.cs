using UnityEngine;

public class trigger : MonoBehaviour
{
    AudioSource interactionSound;
    public GameObject activateObj;

    public bool screamer = false;
    public GameObject screamerObject;
    public float screamerTime = 0f;

    void Start()
    {
        if(!screamer)
            interactionSound = GetComponent<AudioSource>();
    }
    public void InteractionTrigger()
    {
        GetComponent<BoxCollider>().enabled = false;     
        screamerObject.SetActive(true);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && screamer)
        {
            InteractionTrigger();            
            Destroy(gameObject,screamerTime);
        }
        if (other.tag == tag)
        {
            interactionSound.Play();
            activateObj.SetActive(true);
        }
    }        
}
