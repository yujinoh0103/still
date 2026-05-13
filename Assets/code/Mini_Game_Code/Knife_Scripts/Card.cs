using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer cardRender;

    [SerializeField]
    private Sprite animalSprite;

    [SerializeField]
    private Sprite backSprite;

    private bool isFlipped = false;
    public int cardID;

    private bool isMatched = false;
    private bool isFlipping = false;


    public void SetCardID(int id)
    {
        cardID = id;
    }

    public void SetMatched()
    {
        isMatched = true;
    }

    public void SetAnimalSprite(Sprite sprite)
    {
        animalSprite = sprite;
    }


    public void FlipCard()
    {
        isFlipping = true;
        
        isFlipped = !isFlipped;
        if (isFlipped)
        {
            Vector3 originalScale = transform.localScale * 0.2f;
            Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z);

            transform.DOScale(targetScale, 0.3f).OnComplete(() =>
            {
                cardRender.sprite = animalSprite;
                transform.DOScale(originalScale, 0.3f).OnComplete(() =>
                {
                    isFlipping = false;
                });
            });
        }
        else
        {
            Vector3 originalScale = transform.localScale*5;
            Vector3 targetScale = new Vector3(0f, originalScale.y, originalScale.z);

            transform.DOScale(targetScale, 0.2f).OnComplete(() =>
            {
                cardRender.sprite = backSprite;
                transform.DOScale(originalScale, 0.2f).OnComplete(() =>
                {
                    isFlipping = false;
                });
            });
        }


    }


    void OnMouseDown()
    {
        if(!isFlipping&&!isMatched&&!isFlipped)
        { Knife_GameManager.instance.CardClicked(this); }
    }
}