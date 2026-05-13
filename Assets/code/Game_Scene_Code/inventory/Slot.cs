using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerDownHandler //OnPointerDown 사용을 위함
{
    //inventory  - bag - slot - slot_item 삽입

    Action_Item all_item_information;

    public Action_Item items_1; //1층 아이템
    public Action_Item items_2; //2층 아이템
    public Action_Item items_3; //3층 아이템
    public Action_Item items_4; //4층 아이템


    UnityEngine.UI.Image item_image; //UIElements에도 이미지란 정의가 있기에 UI라고 확실하게 정의
    string item_name;
    string item_information;
    int item_id;
    int item_act;
    public UnityEngine.UI.Image slot_default_image;
    public Transform item_information_text;

    public int slot_num; //리스트 제거 시 클릭한 위치 제거용 변수(inventory 스크립트에서 자동 입력)

    public int invrntory_floor = 1;
    //버튼 클릭 시 인벤토리 이동용 변수. 일괄변경때문에 Game_GameMananger에서 편집


    private void OnValidate()
    {
        
    }

    private void Start()
    {
        item_image = gameObject.GetComponent<UnityEngine.UI.Image>();
        item_information_text = GameObject.Find("Canvas").transform.Find("Menu_Panel").transform.Find("Item_slot").transform.Find("Inventory_Panel").transform.Find("item_text");
    }

    public void Update()
    {
        Show_items_Image();
        
    }

   
    public Action_Item item
    {
        get {
            all_item_information = Floor_Items();
            return all_item_information; } //슬롯 안의 아이템 정보 넘김
        set
        {
            all_item_information = value; //들어오는 아이템 정보 저장

            Floor_return_Item();
        }
    }

    public void Show_items_Image() //아이템을 층마다 띄움
    {
        invrntory_floor = GameManager_Game.Game_GameMananger.invrntory_floor;

        if (Floor_Items_Button() != null) //만약 현재 층 아이템이 존재한다면
        {
           
            item_name = Floor_Items_Button().name;
            item_information = Floor_Items_Button().item_information;
            item_id = Floor_Items_Button().item_id;
            item_act = Floor_Items_Button().item_act;
            item_image.sprite = Floor_Items_Button().item_image;
            item_image.color = new Color(1, 1, 1, 1);
        }
        else //들어오는 아이템 정보가 없다면
        {
            item_image = slot_default_image;
            item_image.color = new Color(1, 1, 1, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData) //슬롯을 눌렀다면
    {
        if(Floor_Items_Button() != null)
        {
            
            item_information_text.GetComponent<Text>().text = Floor_Items_Button().item_name + "\n\n" + Floor_Items_Button().item_information;
            //Inventory.INVENTORY.Remove_Item(slot_num); //아이템 제거

        }
    }

   
    public Action_Item Floor_Items_Button() //버튼 이동용
    {
        switch (invrntory_floor)
        {
            case 1:
                return items_1;
            case 2:
                return items_2;
            case 3:
                return items_3;
            case 4:
                return items_4;
            default:
                return null;

        }

    }

    public Action_Item Floor_Items() //층에 따른 아이템 저장용
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num)
        {
            case 1:
                return items_1;
            case 2:
                return items_2;
            case 3:
                return items_3;
            case 4:
                return items_4;
            default:
                return null;

        }

    }

    public void Floor_return_Item()
    {
        switch (DontDestroy_Gamemanager.Dont_Destroy_Instance.player_now_floor_num)
        {
            case 1:
                items_1 = all_item_information;
                break;
            case 2:
                items_2 = all_item_information;
                break;
            case 3:
                items_3 = all_item_information;
                break;
            case 4:
                items_4 = all_item_information;
                break;

        }

    }
}
