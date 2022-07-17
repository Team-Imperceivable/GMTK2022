using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceVisuals : MonoBehaviour
{
    
    [SerializeField] private List<Sprite> d6Sprites;
    [SerializeField] private List<Sprite> d8Sprites;
    [SerializeField] private List<Sprite> d10Sprites;
    [SerializeField] private List<Sprite> d12Sprites;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private List<Sprite> selected;

    private void Start()
    {
        //spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetType(int diceType)
    {
        if(diceType == 6)
        {
            selected = d6Sprites;
        } else if(diceType == 8)
        {
            selected = d6Sprites;
        } else if(diceType == 10)
        {
            selected = d6Sprites;
        } else if (diceType == 12)
        {
            selected = d6Sprites;
        }
    }
    public void SetRoll(int roll)
    {
        Debug.Log(selected[roll - 1].name);
        spriteRenderer.sprite = selected[roll - 1];
    }
}
