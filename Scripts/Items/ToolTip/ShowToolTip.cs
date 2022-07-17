using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShowToolTip : PlayerController, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private int itemSlot;
    private Item currentItem; 
    private GameObject item;

    private RectTransform rectTransform;
    private Image image;

    [SerializeField] private GameObject toolTip;
    [SerializeField] private TextMeshProUGUI headerField;
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private GameObject Image;
    [SerializeField] private Sprite action;
    [SerializeField] private Sprite heart;

    [SerializeField] private Camera mainCamera;

    void Start()
    {
        rectTransform = toolTip.GetComponent<RectTransform>();
        item = GetComponent<GameObject>();
        image = Image.GetComponent<Image>();

        toolTip.SetActive(false);
    }

    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        toolTip.transform.position = mouseWorldPosition;

        float pivotX = mouseWorldPosition.x / Screen.width;
        float pivotY = mouseWorldPosition.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CreateToolTip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.SetActive(false);
    }

    private void CreateToolTip()
    {
        currentItem = SlotToItemName(itemSlot);
        if(currentItem == null)
            return;

        headerField.text = "      " + currentItem.GetName();
        contentField.text = currentItem.GetDescription();

        if(currentItem.GetCost() == 0)
        {
            cost.text = null;
        }
        else
        {
            cost.text = currentItem.GetCost().ToString();
        }

        if (currentItem.GetEffect().Equals("Pact"))
        {
            image.sprite = heart;
        }
        else
        {
            image.sprite = action;
        }

        toolTip.SetActive(true);
    }
}
