using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool doneWithTurn;

    [SerializeField] private CombatHandler combatHandler;
    private FrameInputs inputs;

    private int selectedItemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GatherInputs();
        CheckSelect();  
    }

    #region Selecting
    [SerializeField] private Bounds selectableBounds;

    private void CheckSelect()
    {
        if(inputs.select && selectableBounds.Contains(inputs.mousePos))
        {
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(inputs.mousePos);
            
            if(selectedItemSlot != 0 && !doneWithTurn)
            {
                if (hitColliders.Length > 0)
                    combatHandler.UseItem(selectedItemSlot, hitColliders[0].gameObject);
                else
                    combatHandler.UseItem(selectedItemSlot, null);
            }
        }
    }

    public void SelectItem(int selectedSlot)
    {
        selectedItemSlot = selectedSlot;
    }
    #endregion

    public void TakeTurn()
    {
        combatHandler.TakeTurn();
    }

    private void GatherInputs()
    {
        inputs = new FrameInputs
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition),
            select = Input.GetButtonDown("Fire1"),
            endTurn = Input.GetButtonDown("Jump")
        };
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(selectableBounds.center, selectableBounds.size);
    }
}
