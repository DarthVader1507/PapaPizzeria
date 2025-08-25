using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BakeTimer : MonoBehaviour
{
    [SerializeField] private Text BakeTimerText;
    private int timeLeft;//In seconds
    private GeneralControls generalControls;
    void Start() {
        generalControls = FindObjectOfType<GeneralControls>();
    }
    public void SetTimer()
    {
        if (generalControls.StopScene())
        {
            Debug.Log("Please complete your pizza before proceeding to bake.");
            return;
        }
        timeLeft = 10;
        BakeTimerText.text = timeLeft.ToString();
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
            BakeTimerText.text = timeLeft.ToString();
        }
        BakeTimerText.text = "";
    }
}
