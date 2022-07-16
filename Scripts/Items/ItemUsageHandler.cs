using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUsageHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite backgroundSelectedSprite;

    private RectTransform myRect;
    private Image itemImage;

    private List<Sprite> itemSprites;
    private Item itemInSlot;

    public void OnPointerClick(PointerEventData data)
    {
        playerController.SelectItem(System.Int32.Parse(gameObject.name));
        Item itemCopy = itemInSlot;
        itemInSlot = playerController.SlotToItemName(System.Int32.Parse(gameObject.name));
        if (itemCopy != itemInSlot)
        {
            UpdateSprite();
        }
        UpdateColor();
    }

    private void Start()
    {
        myRect = gameObject.GetComponent<RectTransform>();
        itemImage = gameObject.GetComponent<Image>();
        UpdateColor();
        UpdateSprite();
    }

    private void Update()
    {
        if(gameObject.name.Equals(playerController.selectedItemSlot.ToString()))
        {
            backgroundImage.sprite = backgroundSelectedSprite;
        } else
        {
            backgroundImage.sprite = backgroundSprite;
        }
    }

    private void UpdateSprite()
    {
        if(itemInSlot != null)
            itemImage.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemInSlot.GetName());
    }
    private void UpdateColor()
    {
        Color imgColor = itemImage.color;
        if (itemInSlot != null)
        {
            imgColor.a = 1;
        }
        else
        {
            imgColor.a = 0;
        }
        itemImage.color = imgColor;
    }
}
