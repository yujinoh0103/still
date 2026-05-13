using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//3스테이지 - GameManager 오브젝트. 클리어 판정용
public class puzzle_score : MonoBehaviour
{
    public GameObject clear;

    [Header("퍼즐의 개수")]
    public int puzzle_max;

    [Header("퍼즐의 맞는 개수")]
    public int puzzle_right_num;

    private void Update()
    {
        if (puzzle_right_num == puzzle_max)
        {
            clear.SetActive(true);
        }
    }
   
}
