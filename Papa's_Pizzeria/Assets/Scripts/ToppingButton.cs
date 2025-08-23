using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ToppingButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject toppingPrefab;   // assign in Inspector
    public int pointsForCorrectTopping; // Points for correct topping
    private ToppingSpawner spawner;
    private ScoringControls scoreManager;
    public string category; // e.g., "Crust", "Sauce", "Cheese", etc.
    private OrderManager orderManager;
    public PizzaOrder CurrentOrder;

    void Start()
    {
        spawner = FindObjectOfType<ToppingSpawner>();
        scoreManager = FindObjectOfType<ScoringControls>();
        orderManager = FindObjectOfType<OrderManager>();
        CurrentOrder = orderManager.order;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        spawner.StartDragTopping(toppingPrefab);
        // Check if correct topping is added
        if (category == "Crust")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.crust))
            {
                scoreManager.AddPoints(pointsForCorrectTopping); // Add 10 points (change as needed)
            }
        }
        else if (category == "Sauce")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.sauce))
            {
                scoreManager.AddPoints(pointsForCorrectTopping); // Add 10 points (change as needed)
            }
        }
        else if (category == "Cheese")
        {
            if (spawner.IsCorrectTopping(toppingPrefab, CurrentOrder.cheese))
            {
                scoreManager.AddPoints(pointsForCorrectTopping); // Add 10 points (change as needed)
            }
        }
        bool isCorrect = false;
        foreach (var topping in CurrentOrder.toppings)
        {
            if (spawner.IsCorrectTopping(toppingPrefab, topping))
            {
                isCorrect = true;
                break;
            }
        }
        if (isCorrect)
        {
            scoreManager.AddPoints(pointsForCorrectTopping); // Add 10 points (change as needed)
        }
        
    }
}