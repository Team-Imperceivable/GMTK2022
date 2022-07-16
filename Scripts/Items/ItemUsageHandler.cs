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
    private bool firstImageLoad;

    public void OnPointerClick(PointerEventData data)
    {
        playerController.SelectItem(System.Int32.Parse(gameObject.name));
    }

    private void Start()
    {
        firstImageLoad = false;
        myRect = gameObject.GetComponent<RectTransform>();
        itemImage = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        itemInSlot = playerController.SlotToItemName(System.Int32.Parse(gameObject.name));
        if (!firstImageLoad)
        {
            firstImageLoad = true;
            UpdateColor();
            UpdateSprite();
        }
        if(playerController.updateHotbar)
        {
            UpdateColor();
            UpdateSprite();
        }
        if (gameObject.name.Equals(playerController.selectedItemSlot.ToString()))
        {
            backgroundImage.sprite = backgroundSelectedSprite;
        } else
        {
            backgroundImage.sprite = backgroundSprite;
        }
    }

    private void UpdateSprite()
    {
        itemInSlot = playerController.SlotToItemName(System.Int32.Parse(gameObject.name));
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
