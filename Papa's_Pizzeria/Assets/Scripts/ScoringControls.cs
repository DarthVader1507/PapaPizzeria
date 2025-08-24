using UnityEngine;
using UnityEngine.UI;
public class ScoringControls : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int score = 0;
    public Text scoreText;
    public void AddPoints(int points)
    {
        score += points;
        scoreText.text = score.ToString();
        Debug.Log("Score: " + score);
    }
}
