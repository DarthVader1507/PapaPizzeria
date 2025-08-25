using UnityEngine;
using UnityEngine.SceneManagement;
public class GeneralControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isCrustAdded;
    public bool isSauceAdded;
    public bool isCheeseAdded;
    public bool isTopping1Added;
    public bool isTopping2Added;
    void Start(){
        isCrustAdded = false;
        isSauceAdded = false;
        isCheeseAdded = false;
        isTopping1Added = false;
        isTopping2Added = false;
    }
    public void ChangeScene(string sceneName)
    {
        if ((!isCrustAdded || !isSauceAdded || !isCheeseAdded) && sceneName == "BakeScene")
        {
            Debug.Log("Please complete your pizza before proceeding to bake.");
            return;
        }
        else if (sceneName == "SampleScene"){
            SceneManager.LoadScene(sceneName);
            return;
        }
        SceneManager.LoadScene(sceneName);
        return;

    }
}
