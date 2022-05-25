using UnityEngine;

public class trigger : MonoBehaviour
{
    public AudioSource screamerSound;
    public GameObject activateObj;
    public bool screamer = false;
    public GameObject screamerObject;
    public float screamerTime = 0f;

    public void InteractionTrigger()
    {
        GetComponent<BoxCollider>().enabled = false;
        screamerSound.Play();        
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
            activateObj.SetActive(true);
        }
    }        
}
