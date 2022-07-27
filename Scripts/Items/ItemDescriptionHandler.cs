using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemDescriptionHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text effect;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private GameObject card;

    private void Start()
    {
        Debug.Log(gameObject.name);
    }

    public void UpdateCard(Item item)
    {
        title.text = item.GetName();
        effect.text = item.GetAmount() + " " + item.GetEffect();
        description.text = item.GetDescription();
        cost.text = item.GetCost().ToString();
    }

    public void SetCardActive(bool active)
    {
        card.SetActive(active);
    }
}
