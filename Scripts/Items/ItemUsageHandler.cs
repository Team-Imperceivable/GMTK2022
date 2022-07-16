using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUsageHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerController playerController;
    public void OnPointerClick(PointerEventData data)
    {
        playerController.SelectItem(System.Int32.Parse(gameObject.name));
    }
}
