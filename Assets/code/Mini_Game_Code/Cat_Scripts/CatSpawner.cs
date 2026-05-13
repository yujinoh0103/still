// CatSpawner.cs
using System.Collections;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    public GameObject[] catPrefabs; 
    public float spawnIntervalMin = 0.7f;
    public float spawnIntervalMax = 3f;
    private ScoreManager scoreManager;
    private bool gameEnded = false;
    private int catIndex = 0;

    public void minigame_Button()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        StartCoroutine(SpawnCats());
        StartCoroutine(GameTimer());
    }
    IEnumerator SpawnCats()
    {
        while (!gameEnded)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            if (!gameEnded)
            {
                SpawnCat();
            }
        }
    }

    void SpawnCat()
    {
        Vector3 spawnPosition = new Vector3(0,0, 0);

        GameObject newCat = Instantiate(catPrefabs[catIndex], spawnPosition, Quaternion.identity);
        catIndex = (catIndex + 1) % 6;
        scoreManager.IncreaseSpawnedCatCount(); 
        Destroy(newCat, 1f); // 0.57초 후에 고양이가 사라짐
    }

    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(2f); // 30초 타이머
        if (!gameEnded)
        {
            scoreManager.CheckGameResult(); 
            gameEnded = true;
        }
    }
}
