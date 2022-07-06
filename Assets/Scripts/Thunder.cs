using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    Light lightning;
    public float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        lightning = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0.7f)
        {
            lightning.intensity = Random.Range(0.5f, 2.5f);
            timer += Time.deltaTime;            
        }
        else
            lightning.intensity = 0;
        //Destroy(gameObject, 5f);
    }
}
