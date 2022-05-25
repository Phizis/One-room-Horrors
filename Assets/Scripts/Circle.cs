using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Circle : MonoBehaviour
{
    //public NavMeshObstacle myObstacle;
    public GameObject Enemy;
    public GameObject EnemyModel;
    private NavMeshObstacle myObstacle;

    private void OnTriggerEnter(Collider other)
    {
        myObstacle = GetComponent<NavMeshObstacle>();

        if (other.tag == "Chalk")
        {
            myObstacle.enabled = true;
        }
        
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Chalk").Length == 1 &&
            GameObject.FindGameObjectsWithTag("Book").Length == 1 && GameObject.FindGameObjectsWithTag("Candle").Length == 1)
        {
            EnemyModel.SetActive(false);
            Enemy.SetActive(true);
        }
    }

}
