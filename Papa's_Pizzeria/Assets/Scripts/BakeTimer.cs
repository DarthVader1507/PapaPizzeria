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
    private bool isBaking;
    void Start()
    {
        generalControls = FindObjectOfType<GeneralControls>();
        isBaking = false;
    }
    public void SetTimer()
    {
        if (!generalControls.StopScene())
        {
            ErrorText.text = "Please complete your pizza before proceeding to bake.";
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
            ErrorText.text = "Your pizza is already baking!";
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
    // ...existing code...
}
