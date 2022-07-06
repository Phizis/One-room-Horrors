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

    AudioSource creepySound;
    public AudioClip[] creepySound_AR;
    [SerializeField] float timer = 5f;
    [SerializeField] float timerDown = 0f;

    public bool raven = false;
    void Start()
    {
        creepySound = GetComponent<AudioSource>();

        if (screamerComponent)
        {
            myAgent = GetComponent<NavMeshAgent>();
            creepySound.Play();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(screamerComponent)
            myAgent.destination = myPlayer.position;

        if (!screamerComponent)
        {
            if (timerDown > 0)
                timerDown -= Time.deltaTime;
            if (timerDown <= 0)
            {
                creepySound.pitch = Random.Range(0.8f, 1.1f);
                creepySound.volume = Random.Range(0.7f, 1f);
                creepySound.PlayOneShot(creepySound_AR[Random.Range(0, creepySound_AR.Length)]);
                timerDown = timer;
            }
        }
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
