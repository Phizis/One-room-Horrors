using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadialBarFiller : MonoBehaviour
{
    public int currentValue = 0, maxValue = 1000;

    public Image fill;
    void Start()
    {
        fill.fillAmount = Normalise();
    }

    public void Add()
    {        
        currentValue += 1;

        if (currentValue > maxValue)
            currentValue = maxValue;

        fill.fillAmount = Normalise();

        if(currentValue == maxValue)        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Add();
        }
    }
    private float Normalise()
    {
        return (float)currentValue / maxValue;
    }
}
