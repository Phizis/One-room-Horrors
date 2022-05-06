using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Circle : MonoBehaviour
{
    public NavMeshObstacle myObstacle;
    public GameObject Enemy;
    
    private void OnTriggerEnter(Collider other)
    {
        myObstacle = GetComponent<NavMeshObstacle>();

        if (other.tag == "Chalk")
        {
            myObstacle.enabled = true;
        }
        if (GameObject.FindGameObjectsWithTag("Chalk").Length == 1)
        {
            Enemy.SetActive(true);
        }
    }

}
