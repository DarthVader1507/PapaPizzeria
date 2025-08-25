using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BakeTimer : MonoBehaviour
{
    [SerializeField] private Text BakeTimerText;
    private int timeLeft;//In seconds
    [SerializeField] private Text TitleText;
    [SerializeField] private Text ErrorText;
    private GeneralControls generalControls;
    public bool isBaking;
    void Start()
    {
        generalControls = FindObjectOfType<GeneralControls>();
        isBaking = false;
    }
    public void SetTimer()
    {
        if (!generalControls.StopScene())
        {
            StartCoroutine(ErrorMessageCoroutine("Finish your pizza to proceed to baking", 2.0f));
            return;
        }
        if (!isBaking)
        {
            isBaking = true;
            RemovePizzaToppings();
            ErrorText.text = "";
            TitleText.text = "Baking Time";
            timeLeft = 10;
            BakeTimerText.text = timeLeft.ToString();
            StartCoroutine(Countdown());
        }
        else
        {
            StartCoroutine(ErrorMessageCoroutine("Your pizza is already baking!" ,2.0f));
        }
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
        TitleText.text = "Baking Complete!";
        yield return new WaitForSeconds(2.0f);
        TitleText.text = "";
        isBaking = false;
    }
    // ...existing code...

    private void RemovePizzaToppings()
    {
        GameObject pizzaParent = GameObject.Find("PizzaBase"); // Replace with your pizza parent name
        if (pizzaParent == null) return;

        foreach (Transform child in pizzaParent.transform)
        {
            if (child.name != "PizzaBase") // Replace with your PizzaBase object's name
            {
                Destroy(child.gameObject);
            }
        }
    }
    private IEnumerator ErrorMessageCoroutine(string message, float delay)
    {
        ErrorText.text = message;
        yield return new WaitForSeconds(delay);
        ErrorText.text = "";
    }
    // ...existing code...
}
