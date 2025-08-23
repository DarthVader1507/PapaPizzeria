using UnityEngine;

public class ScoringControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score = 0;
    public void AddPoints(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }
}
