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
    public int pointsForCorrectTopping = 10; // fallback value

    void Start()
    {
        spawner = FindObjectOfType<ToppingSpawner>();
        scoreManager = FindObjectOfType<ScoringControls>();
        orderManager = FindObjectOfType<OrderManager>();
        ingredientManager = FindObjectOfType<IngredientManager>();
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
        if (ingredient == null) return;

        if (ingredient.category == "Crust")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.crust))
            {
                Debug.Log("Correct Crust Added");
                scoreManager.AddPoints(CurrentOrder.crust.score);
            }
        }
        else if (ingredient.category == "Sauce")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.sauce))
            {
                scoreManager.AddPoints(CurrentOrder.sauce.score);
            }
        }
        else if (ingredient.category == "Cheese")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.cheese))
            {
                scoreManager.AddPoints(CurrentOrder.cheese.score);
            }
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