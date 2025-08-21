using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IngredientManager ingredientManager;
    public Text OrderText;
    void Start()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();
        if (ingredientManager == null)
            Debug.LogError("IngredientManager not found in scene!");
        StartCoroutine(StartOrder());
    }
    private IEnumerator StartOrder()
    {
        //Wait for 5 seconds before generating the next order
        int order_count = 0;
        while (order_count < 5)
        {
            PizzaOrder order = ingredientManager.GenerateOrder();
            OrderText.text = order.crust.name + ", " + order.sauce.name + ", " + order.cheese.name 
                + ", " + order.toppings[0].name + ", " + order.toppings[1].name;
            order_count++;
            yield return new WaitForSeconds(5f);
        }
    }
}
