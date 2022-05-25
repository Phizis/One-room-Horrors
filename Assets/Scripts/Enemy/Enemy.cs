using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform myPlayer;
    private NavMeshAgent myAgent;
    public GameObject playerTarget;
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public bool screamerComponent;
    // Start is called before the first frame update
    void Start()
    {
        if (screamerComponent)
            myAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(screamerComponent)
            myAgent.destination = myPlayer.position;        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !screamerComponent)
        {
            playerTarget.SetActive(false);
            Destroy(pauseMenu);
            deathMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }

        if(other.tag == "Destination" && screamerComponent)
        {
                Destroy(gameObject);
        }
    }
}
