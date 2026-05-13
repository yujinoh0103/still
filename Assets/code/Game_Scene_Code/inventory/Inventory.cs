using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory INVENTORY;
    public List<Action_Item> items;
    public List<Action_Item> items_1; //1층 아이템 리스트
    public List<Action_Item> items_2; //리스트
    public List<Action_Item> items_3; //리스트
    public List<Action_Item> items_4; //리스트

    [SerializeField] //private 변수를 인스펙터에서 접근가능하게 함
    Transform slot_parent;
    [SerializeField]
    Slot[] slots; //기본 슬롯

    private void OnValidate() //OnValidate()의 기능은 게임을 실행하지 않더라도 유니티 에디터에서 바로 작동을 하는 역할
    {
       slots = slot_parent.GetComponentsInChildren<Slot>();
        
        for(int i = 0;i<slots.Length;i++)
        {
            slots[i].slot_num = i;
        }
    }

    
    public void Clear_Slot() //아이템 획득, 제거시마다 새로고침
    {
       Floor_Items();
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
        Floor_return_Item();

        
    }

    private void Awake()
    {
        INVENTORY = this;
        Clear_Slot();
    }

    public void Add_Item(Action_Item input_item)
    {
        Floor_Items();
        if (items.Count < slots.Length)
        {
            items.Add(input_item);
            Clear_Slot();
        }
        else
            Debug.Log("슬롯이 가득 참");
    }

    public void Remove_Item(int slot_num)
    {
        Floor_Items();
        if (items.Count > 0)
        {
            items.RemoveAt(slot_num);//특정 부분을 제거하는 리스트 함수 
            Clear_Slot();
        }
        else
        {
            Debug.Log("슬롯이 비어 있음");
        }
    }

    public void Floor_Items()
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num)
        {
            case 1:
                items = items_1;
                break;
            case 2:
                items = items_2;
                break;
            case 3:
                items = items_3;
                break;
            case 4:
                items = items_4;
                break;

        }

        

    }

    public void Floor_return_Item()
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num)
        {
            case 1:
                items_1 = items;
                break;
            case 2:
                items_2 = items;
                break;
            case 3:
                items_3 = items;
                break;
            case 4:
                items_4 = items;
                break;

        }

       
    }
}
