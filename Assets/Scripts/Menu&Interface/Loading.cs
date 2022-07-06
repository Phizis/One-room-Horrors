using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;
    public TextMeshProUGUI loadingText;
    public Slider loadingSlider;
    public bool mainMenuLoading = true;

    void Start()
    {
        if (!mainMenuLoading)
        {
            Load();
        }
    }

    public void Load()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            loadingSlider.value = asyncLoad.progress;
            if(asyncLoad.progress >= 0.9f && !asyncLoad.allowSceneActivation)
            {
                loadingSlider.value = 100;
                loadingText.text = "Press any key";
                if (Input.anyKeyDown)
                {                    
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
