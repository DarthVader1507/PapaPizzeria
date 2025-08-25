using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToppingButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject toppingPrefab;   // assign in Inspector
    private ToppingSpawner spawner;
    private ScoringControls scoreManager;
    private IngredientManager ingredientManager;
    private Ingredient ingredient; // Use Ingredient instead of string category
    private OrderManager orderManager;
    public PizzaOrder CurrentOrder;
    private GeneralControls generalControls;
    void Start()
    {
        spawner = FindObjectOfType<ToppingSpawner>();
        scoreManager = FindObjectOfType<ScoringControls>();
        orderManager = FindObjectOfType<OrderManager>();
        ingredientManager = FindObjectOfType<IngredientManager>();
        generalControls = FindObjectOfType<GeneralControls>();
        CurrentOrder = orderManager.order;
        if (scoreManager == null)
        {
            Debug.LogError("ScoringControls not found in the scene.");
        }
        if (ingredientManager == null)
        {
            Debug.LogError("IngredientManager not found in the scene.");
        }

        // Assign ingredient by prefab name
        if (ingredientManager != null && toppingPrefab != null)
        {
            ingredient = ingredientManager.allIngredients.Find(i => i.name == toppingPrefab.name);
            if (ingredient == null)
            {
                Debug.LogError("Ingredient not found for prefab: " + toppingPrefab.name);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        spawner.StartDragTopping(toppingPrefab, this);
    }
    public void OnToppingAdded()
    {
        CurrentOrder = orderManager.order; // Update to the latest order
        if (ingredient == null)
        {
            Debug.LogError("Ingredient is not assigned for this topping button.");
            return;
        }
        if (spawner == null)
        {
            Debug.LogError("ToppingSpawner is not assigned.");
            return;
        }
        if (toppingPrefab == null)
        {
            Debug.LogError("ToppingPrefab is not assigned.");
            return;
        }
        if (ingredient.category == "Crust" && !(generalControls.isCrustAdded))
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.crust))
            {
                Debug.Log("Correct Crust Added");
                scoreManager.AddPoints(CurrentOrder.crust.score);
            }
            generalControls.isCrustAdded = true;
        }
        else if (ingredient.category == "Sauce" && !(generalControls.isSauceAdded) && generalControls.isCrustAdded)
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.sauce))
            {
                scoreManager.AddPoints(CurrentOrder.sauce.score);
            }
            generalControls.isSauceAdded = true;
        }
        else if (ingredient.category == "Cheese" && !(generalControls.isCheeseAdded) && generalControls.isSauceAdded)
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.cheese))
            {
                scoreManager.AddPoints(CurrentOrder.cheese.score);
            }
            generalControls.isCheeseAdded = true;
        }
        else
        {
            bool isCorrect = false;
            var topping = ingredient;
            foreach (var toppings in CurrentOrder.toppings)
            {
                if (spawner.IsCorrectTopping(toppingPrefab, toppings))
                {
                    isCorrect = true;
                    topping = toppings;
                    break;
                }
            }
            if (isCorrect)
            {
                scoreManager.AddPoints(topping.score);
            }
        }
    }
}