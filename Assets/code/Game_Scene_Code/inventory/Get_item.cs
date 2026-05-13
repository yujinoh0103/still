using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get_item : MonoBehaviour
{
    public Action_Item item;

    private void OnValidate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.item_image;
    }

    private void OnMouseDown()
    {
        Inventory.INVENTORY.Add_Item(item);
        //GameManager_Game.Game_GameMananger.is_sound = true;
        Destroy(gameObject);
    }
}
