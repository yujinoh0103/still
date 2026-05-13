using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag_mini10 : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector2 pos;
    bool is_over, is_clear; // 실패했는가, 성공했는가
    [Header("모든 체력 오브젝트")]
    public GameObject[] hp;
    [Header("클리어 텍스트")]
    public Text clearText; // 추가: 클리어 텍스트

    int i = 0;

    void Start()
    {
        if (!is_clear)
        {
            if (clearText != null)
            {
                clearText.gameObject.SetActive(false);
            }
        }
    }


    void Update()
    {
        if (is_over&&!is_clear)
        {
            clearText.gameObject.SetActive(true);
            clearText.text="Game Over";
        }
    }


    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.pos = transform.position;
        if (is_over)
        {
            foreach (GameObject j in hp)
            {
                j.SetActive(true);
                i = 0;
            }
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 current_pos = eventData.position;
        transform.position = current_pos;
        if (is_over)
            transform.position = this.pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (!is_clear)
            transform.position = this.pos;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Obstacle")&&is_over==false)
        {
            hp[i].SetActive(false);
            i++;

            if (i >= hp.Length)
            {
                is_over = true;
            }
        }

        if (other.transform.CompareTag("Clear")&&is_over==false)
        {
            clearText.gameObject.SetActive(true);
            is_clear = true;
            clearText.text = "Clear!"; // 추가: 클리어 텍스트 변경
            is_over = true;
        }
    }


}

