using UnityEngine;

public class BlockController : MonoBehaviour
{
    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

    }

    private void OnMouseDown()
    {
        if (scoreManager != null)
        {
            Destroy(gameObject); 
            scoreManager.IncreaseScore(1); 
            
        }
        else
        {
            Debug.LogError("ScoreManager not assigned to BlockController.");
        }
    }
}
