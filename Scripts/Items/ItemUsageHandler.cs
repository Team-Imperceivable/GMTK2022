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

    public void OnPointerClick(PointerEventData data)
    {
        playerController.SelectItem(System.Int32.Parse(gameObject.name));
    }

    private void Start()
    {
        myRect = gameObject.GetComponent<RectTransform>();
        itemImage = gameObject.GetComponent<Image>();
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
        Color imgColor = itemImage.color;
        Item itemInSlot = playerController.SlotToItemName(System.Int32.Parse(gameObject.name));
        if (itemInSlot != null)
        {
            Texture2D itemTexture = Resources.Load<Texture2D>("Sprites/Items/" + itemInSlot.GetName());
            itemImage.sprite = Sprite.Create(itemTexture, myRect.rect, Vector2.zero, 0f, 0);
            imgColor.a = 1;
        } else
        {
            imgColor.a = 0;
        }
        itemImage.color = imgColor;
    }
}
