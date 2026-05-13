using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cg_Off : MonoBehaviour, IPointerClickHandler
{
    public GameObject Story_Canvas;
    //CG 이미지 끔
    public void OnPointerClick(PointerEventData eventData)
    {
        Story_Canvas.GetComponent<GameManager_Talk>().is_no_click = false;
        gameObject.SetActive(false);
    }
}
