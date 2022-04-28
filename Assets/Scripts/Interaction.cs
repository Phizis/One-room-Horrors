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

    void OnMouseOver()
    {
        distance = Vector3.Distance(myPlayer.GetComponent <Transform>().position, transform.position);

        if(distance < interactionDistance)
        {
            myImage.enabled = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.position = arm.position;
                transform.rotation = arm.rotation;
                transform.SetParent(arm);
                GetComponent <Rigidbody> ().isKinematic = true;
            }
        }
        else
        {
            myImage.enabled = false;
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
