using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Hand: MonoBehaviour
{
    public GameObject hand;

    private void OnMouseDown()
    {
        hand.SetActive(false); // "hand" 게임 오브젝트를 비활성화하여 숨김
        Timing.instance.isMoving = !Timing.instance.isMoving;
    }
}
