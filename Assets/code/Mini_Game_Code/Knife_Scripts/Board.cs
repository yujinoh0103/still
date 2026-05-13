using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;
    // Start is called before the first frame update


    [SerializeField]

    private Sprite[] cardSprites;

    private List<int> cardIDList = new List<int>();
    private List<Card> cardList = new List<Card>();


    void Start()
    {
        GenerateCardID();
        ShuffleCardID();
        InitBoard();
    }

    void GenerateCardID()
    {
        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardIDList.Add(i);
        }
    }

    void ShuffleCardID()
    {
        int cardCount = cardIDList.Count;

        for (int i = 0; i < cardCount / 2; i++)
        {
            int randomIndex = Random.Range(i, cardCount);
            int temp = cardIDList[randomIndex];
            cardIDList[randomIndex] = cardIDList[i];
            cardIDList[i] = temp;
        }
    }

    void InitBoard()
    {

        int rowCount = 2;//세로
        int colCount = 6;//가로
        int cardIndex = 0;

        for (int row = 0; row < rowCount; row++)
        {

            for (int col = 0; col < colCount; col++)
            {
                float posY = (row - (int)(rowCount / 2)) * 3.0f+3.0f;
                float posX = (col - (int)(colCount / 2)) * 3.0f + 1.5f;
                Vector3 pos = new Vector3(posX, posY, 0f);
                GameObject cardObject = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>();
                int cardID = cardIDList[cardIndex++];

                card.SetCardID(cardID);
                card.SetAnimalSprite(cardSprites[cardID]);
                cardList.Add(card);
            }
        }

    }




    public List<Card> GetCards()
    {
        return cardList;
    }

}
