using UnityEngine;

public class ToppingSpawner : MonoBehaviour
{
    private GameObject currentTopping;

    void Update()
    {
        if (currentTopping != null)
        {
            // Follow mouse in world space
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            currentTopping.transform.position = mousePos;

            if (Input.GetMouseButtonUp(0))
            {
                Collider2D hit = Physics2D.OverlapPoint(mousePos);
                if (hit != null && hit.CompareTag("PizzaBase"))
                {
                    // Snap inside pizza circle
                    CircleCollider2D pizzaCollider = hit.GetComponent<CircleCollider2D>();
                    Vector3 pizzaCenter = hit.transform.position;
                    float pizzaRadius = pizzaCollider.radius * hit.transform.localScale.x;

                    Vector3 offset = mousePos - pizzaCenter;

                    // If dropped outside radius → clamp it inside
                    if (offset.magnitude > pizzaRadius)
                    {
                        offset = offset.normalized * pizzaRadius;
                    }

                    currentTopping.transform.position = pizzaCenter + offset;
                    currentTopping.transform.SetParent(hit.transform);
                }
                else
                {
                    Destroy(currentTopping);
                }

                currentTopping = null;
            }
        }
    }

    public void StartDragTopping(GameObject prefab)
    {
        if (currentTopping == null)
        {
            currentTopping = Instantiate(prefab);
        }
    }
    public bool IsCorrectTopping(GameObject toppingPrefab,  Ingredient requiredToppingName)
    {
        // Compare the prefab's name to the required topping name
        return toppingPrefab.name == requiredToppingName.ToString();
    }
}
