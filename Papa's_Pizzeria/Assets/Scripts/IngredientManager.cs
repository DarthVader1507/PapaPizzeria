using System.Collections.Generic;
using UnityEngine;

// Ingredient data structure
[System.Serializable]
public class Ingredient
{
    public string name;
    public string category; // Crust, Sauce, Cheese, Veggie, Meat, Fun
    public int weight;      // Higher weight = more common
    public int score;       // Score value if correct

    public Ingredient(string name, string category, int weight, int score)
    {
        this.name = name;
        this.category = category;
        this.weight = weight;
        this.score = score;
    }
}

// Order structure
public class PizzaOrder
{
    public Ingredient crust;
    public Ingredient sauce;
    public Ingredient cheese;
    public List<Ingredient> toppings = new List<Ingredient>();
}

public class IngredientManager : MonoBehaviour
{
    public List<Ingredient> allIngredients = new List<Ingredient>();

    void Awake()
    {
        // Initialize pool (small version)
        allIngredients.Add(new Ingredient("Hand-tossed", "Crust", 40, 50));
        allIngredients.Add(new Ingredient("Thin crust", "Crust", 40, 50));
        allIngredients.Add(new Ingredient("Cheese burst", "Crust", 20, 75));

        allIngredients.Add(new Ingredient("Classic tomato", "Sauce", 50, 50));
        allIngredients.Add(new Ingredient("White garlic", "Sauce", 30, 75));
        allIngredients.Add(new Ingredient("BBQ", "Sauce", 20, 100));

        allIngredients.Add(new Ingredient("Mozzarella", "Cheese", 50, 50));
        allIngredients.Add(new Ingredient("Cheddar", "Cheese", 30, 75));
        allIngredients.Add(new Ingredient("Feta", "Cheese", 20, 100));

        allIngredients.Add(new Ingredient("Onion", "Veggie", 40, 50));
        allIngredients.Add(new Ingredient("Bell pepper", "Veggie", 40, 50));
        allIngredients.Add(new Ingredient("Mushrooms", "Veggie", 20, 75));

        allIngredients.Add(new Ingredient("Pepperoni", "Meat", 40, 50));
        allIngredients.Add(new Ingredient("Sausage", "Meat", 40, 50));
        allIngredients.Add(new Ingredient("Grilled chicken", "Meat", 20, 75));

        allIngredients.Add(new Ingredient("Ramen noodles 🍜", "Fun", 10, 125));
        allIngredients.Add(new Ingredient("French fries 🍟", "Fun", 10, 125));
    }
    private Ingredient GetRandomIngredient(params string[] categories)
    {
        List<Ingredient> pool = allIngredients.FindAll(i => System.Array.Exists(categories, c => c == i.category));

        if (pool.Count == 0)
        {
            Debug.LogError("No ingredients found for categories: " + string.Join(", ", categories));
            return null;
        }

        int totalWeight = 0;
        foreach (var ing in pool) totalWeight += ing.weight;

        int roll = Random.Range(0, totalWeight);
        int current = 0;

        foreach (var ing in pool)
        {
            current += ing.weight;
            if (roll < current)
                return ing;
        }

        return pool[0]; // fallback
    }

    public PizzaOrder GenerateOrder()
    {
        PizzaOrder order = new PizzaOrder();

        order.crust = GetRandomIngredient("Crust");
        order.sauce = GetRandomIngredient("Sauce");
        order.cheese = GetRandomIngredient("Cheese");

        // 2 toppings per pizza (can be veggie, meat, or fun)
        var topping1 = GetRandomIngredient("Veggie", "Meat", "Fun");
        var topping2 = GetRandomIngredient("Veggie", "Meat", "Fun");

        if (topping1 != null) order.toppings.Add(topping1);
        if (topping2 != null) order.toppings.Add(topping2);

        string toppingNames = order.toppings.Count > 0
            ? string.Join(", ", order.toppings.ConvertAll(t => t.name))
            : "No toppings";

        Debug.Log($"Order: {order.crust?.name}, {order.sauce?.name}, {order.cheese?.name}, {toppingNames}");

        return order;
    }
}
