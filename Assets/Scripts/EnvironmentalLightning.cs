using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalLightning : MonoBehaviour
{
    private float constLight;
    public bool GeneralLight = false;
    public GameObject lightning;
    // Start is called before the first frame update
    void Start()
    {
        constLight = GetComponent<Light>().intensity;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            if (GetComponent<Light>().intensity > 0.0f)
                GetComponent<Light>().intensity -= 0.0003f;
            else
            {
                if (GeneralLight)
                {
                    lightning.SetActive(false);
                }
            }
        }
    }
}
