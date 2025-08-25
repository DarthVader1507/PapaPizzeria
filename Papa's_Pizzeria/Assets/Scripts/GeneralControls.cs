using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GeneralControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isCrustAdded;
    public bool isSauceAdded;
    public bool isCheeseAdded;
    public bool isTopping1Added;
    public bool isTopping2Added;
    public IngredientManager ingredientManager;
    public Text OrderText;
    public PizzaOrder order = null;
    void Start(){
        ingredientManager = FindObjectOfType<IngredientManager>();
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
        isCrustAdded = false;
        isSauceAdded = false;
        isCheeseAdded = false;
        isTopping1Added = false;
        isTopping2Added = false;
        return true;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        return;
    }
    public void OrderGenerate()
    {
        if (!StopScene())
        {
            return;
        }
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
