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
    public bool StopScene(){
        if ((!isCrustAdded || !isSauceAdded || !isCheeseAdded))
        {
            return false;
        }
        return true;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        return;
    }
}
