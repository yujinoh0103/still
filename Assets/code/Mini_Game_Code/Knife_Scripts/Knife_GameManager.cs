using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Knife_GameManager : MonoBehaviour
{
    public static Knife_GameManager instance;
    private List<Card> allCards;

    private Card flippedCard;

    private bool isFlipping = false;
    private bool isMatched = false;
    private int matchesFound = 0;
    private int totalMatches = 6;
    private bool isGameOver=false;

    [SerializeField]
    private Sprite[] matchedCards;



    [SerializeField]
    private GameObject gameOverPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Board board = FindObjectOfType<Board>();
        allCards = board.GetCards();
        StartCoroutine(FlipAllCardsRoutine());
        gameOverPanel.SetActive(false);
    }

    IEnumerator FlipAllCardsRoutine()
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3.0f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);

        isFlipping = false;
    }

    void FlipAllCards()
    {
        foreach (Card card in allCards)
        {
            card.FlipCard();
        }
    }


    public void CardClicked(Card card)
    {
        if (isFlipping)
        {
            return;
        }

        card.FlipCard();
        if (flippedCard == null) flippedCard = card;
        else
        {
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        isFlipping = true;

        if ((card1.cardID % 2 == 0 && card1.cardID == card2.cardID - 1) || (card1.cardID % 2 == 1 && card1.cardID == card2.cardID + 1))
        {
            card1.SetMatched();
            card2.SetMatched();

            matchesFound++;

            if (matchesFound <=totalMatches)
            {
                yield return new WaitForSeconds(0.7f);
                // 비활성화 또는 제거
                card1.gameObject.SetActive(false);
                card2.gameObject.SetActive(false);

                // "RawImage" 오브젝트 활성화
                string rawImageName = "RawImage (" + (matchesFound - 1) + ")";
                GameObject rawImageObject = GameObject.Find(rawImageName);
                if (rawImageObject != null)
                {
                    rawImageObject.SetActive(true);

                    // 스프라이트 선택 및 설정
                    int spriteIndex = card1.cardID/2; 
                    rawImageObject.GetComponent<RawImage>().texture = matchedCards[spriteIndex].texture;
                }
                else
                {
                    Debug.LogError("RawImage object not found: " + rawImageName);
                }

                if (matchesFound == totalMatches)
                {
                    GameOver(true);
                }
            }

            else
            {
                GameOver(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            card1.FlipCard();
            card2.FlipCard();
            yield return new WaitForSeconds(0.4f);
        }

        isFlipping = false;
        flippedCard = null;
    }




    void GameOver(bool success)
    {
        if (!isGameOver)
        {
            isGameOver = true;
            if (success)
            {

                Invoke("ShowGameOverPanel", 1f);

            }
        }


       
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}