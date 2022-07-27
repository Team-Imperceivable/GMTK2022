using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUsageHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite backgroundSelectedSprite;
    [SerializeField] private ItemDescriptionHandler itemDescription;

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
        itemDescription.SetCardActive(false);
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

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (itemInSlot != null)
        {
            itemDescription.SetCardActive(true);

            ItemDescriptionHandler description = itemDescription.GetComponent<ItemDescriptionHandler>();
            description.UpdateCard(itemInSlot);
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        itemDescription.SetCardActive(false);
    }
}
