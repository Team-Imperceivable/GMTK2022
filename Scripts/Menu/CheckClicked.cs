using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckClicked : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string clickedMethodName;
    [SerializeField] private Color hoverColor;
    private Image text;
    private Color normalColor;

    void Start()
    {
        text = gameObject.GetComponent<Image>();
        normalColor = text.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SendMessageUpwards(clickedMethodName);
        text.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = normalColor;
    }
}
