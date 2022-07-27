using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHoverHandler : MonoBehaviour
{
    [SerializeField] private ShopHandler shop;
    private ItemDescriptionHandler itemCard;

    private void Start()
    {
        itemCard = GameObject.Find("Item Card").GetComponent<ItemDescriptionHandler>();
    }

    private void OnMouseEnter()
    {
        itemCard.SetCardActive(true);
        try
        {
            //Is an item
            int mySlot = Int32.Parse(gameObject.name.Substring(gameObject.name.Length - 1));
            ItemDescriptionHandler itemDescription = itemCard.GetComponent<ItemDescriptionHandler>();
            itemDescription.UpdateCard(shop.GetItems()[mySlot].item);
        } catch(FormatException e)
        {
            //Is Dice
        }
    }

    private void OnMouseExit()
    {
        itemCard.SetCardActive(false);
    }
}
