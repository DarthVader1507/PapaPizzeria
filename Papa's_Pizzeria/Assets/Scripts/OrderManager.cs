using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class OrderManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public IngredientManager ingredientManager;
    public Text OrderText;
    public PizzaOrder order = null;
    void Start()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();
        if (ingredientManager == null)
            Debug.LogError("IngredientManager not found in scene!");
        order = ingredientManager.GenerateOrder();
        OrderText.text = order.crust.name + "\n" + order.sauce.name + "\n " + order.cheese.name;
        if (order.toppings.Count == 0)
        {
            OrderText.text += "\n No toppings";
        }
        else
        {
            foreach (var topping in order.toppings)
            {
                OrderText.text += "\n " + topping.name;
            }
        }
    }
}
