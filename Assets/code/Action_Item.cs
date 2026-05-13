using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu] //에셋 메뉴에서 바로 생성 가능해짐
public class Action_Item : ScriptableObject  //스크립트를 모듈로 쓰기 위한 변경
{
    // Start is called before the first frame update

    [Header("아이템 이름")]
    public string item_name;

    [Header("아이템 설명")]
    public string item_information;

    [Header("아이템 id")]
    public int item_id;

    [Header("아이템 이미지")]
    public Sprite item_image;

    [Header("아이템 공격력/방어력")]
    public int item_act;

    [Header("선공 시 추가 공격력 / 후공 시 추가 방어력 (랜덤 수치)")]
    public int min;
    public int max;

    public static Action_Item ITEM_MODULE;

    private void Awake()
    {
        ITEM_MODULE = this;

    }
}
