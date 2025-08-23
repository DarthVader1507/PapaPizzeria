using UnityEngine;
using UnityEngine.UI;
public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Text timerText;
    public static float timeLeft = 180.0f; // 3 minutes in seconds
    void Start()
    {
        timerText.text = timeLeft.ToString("F2");
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("F2");
            if (timeLeft < 5)
            {
                timerText.color = Color.red; // Change text color to red when time is less than 5 seconds
            }
            else
            {
                timerText.color = Color.white; // Reset text color to white when time is above 5 seconds
            }
        }
        else
        {
            timeLeft = 0;
            timerText.text = "Time's up!";
        }
    }
}
