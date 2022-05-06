using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform myPlayer;
    private NavMeshAgent myAgent;
    public GameObject playerTarget;
    public GameObject deathMenu;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        myAgent.destination = myPlayer.position;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerTarget.SetActive(false);
            deathMenu.SetActive(true);
        }
    }
}
