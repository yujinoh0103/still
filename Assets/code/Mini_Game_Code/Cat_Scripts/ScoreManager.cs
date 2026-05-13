using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    private int spawnedCatCount = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverImage; //게임클리어,오버이미지는 추후 삭제(구분용)
    public GameObject gameClearImage;
    private bool gameEnded = false;

    void Start()
    {
        gameOverImage.SetActive(false);
        gameClearImage.SetActive(false);
    }

    public void IncreaseScore(int amount)
    {
        if (!gameEnded)
        {
            score += amount;
            UpdateScoreText();
        }
    }

    public void IncreaseSpawnedCatCount()
    {
        spawnedCatCount++;
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void CheckGameResult()
    {
        Debug.Log("게임 종료");
        if (score >= spawnedCatCount * 0.75f) //75% 이상 고양이를 잡으면 클리어
        {
            GameClear();
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        // 게임오버 처리
        gameEnded = true;
        gameOverImage.SetActive(true);
        Invoke("Scene_Move", 1);
    }

    void GameClear()
    {
        // 게임클리어 처리
        gameEnded = true;
        gameClearImage.SetActive(true);
        DontDestroy_Gamemanager.Dont_Destroy_Instance.clear_mini[5] = true;
        Invoke("Scene_Move", 1); //딜레이

    }

    void Scene_Move()
    {
        DontDestroy_Gamemanager.Dont_Destroy_Instance.is_talk_start_sign = true;
        DontDestroy_Gamemanager.Dont_Destroy_Instance.Talk_Num++;
        SceneManager.LoadScene("Game_Scene");
    }
}
