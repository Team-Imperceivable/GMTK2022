using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool doneWithTurn;

    [SerializeField] private CombatHandler combatHandler;
    public int money;
    public bool updateHotbar = false;
    private FrameInputs inputs;

    public int selectedItemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(updateHotbar)
        {
            updateHotbar = false;
        }
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
            
            if((hitColliders.Length > 0) && hitColliders[0] != null)
            {
                if (hitColliders[0].tag.Equals("Shop"))
                {
                    char[] hitObjName = hitColliders[0].name.ToCharArray();
                    ShopHandler itemShop = hitColliders[0].GetComponentInParent<ShopHandler>();
                    if (hitObjName[0] == 'D')
                    {
                        if (itemShop.GetItemInSlot(0).cost <= money)
                        {
                            Die boughtDie = itemShop.BuyDie();
                            combatHandler.AddDie(boughtDie);
                            money -= boughtDie.MaxRoll();
                        }
                    } else if (hitObjName[0] == 'I')
                    {
                        int shopItemSlot = System.Int32.Parse(hitObjName[hitObjName.Length - 1].ToString());
                        ShopItem itemInShop = itemShop.GetItemInSlot(shopItemSlot);
                        if (itemInShop.cost <= money)
                        {
                            money -= itemInShop.cost;
                            Item boughtItem = itemShop.BuyItem(shopItemSlot);
                            if (!combatHandler.AddItemToInventory(boughtItem))
                            {
                                combatHandler.ReplaceItem(selectedItemSlot - 1, boughtItem);
                            }
                            updateHotbar = true;
                        }
                    } else if (hitObjName[0] == 'N')
                    {
                        itemShop.ExitShop();
                    }
                }
                else if (selectedItemSlot != 0 && !doneWithTurn)
                {
                    combatHandler.UseItem(selectedItemSlot, hitColliders[0].gameObject);
                } 
            }
            else if(selectedItemSlot != 0 && !doneWithTurn)
                combatHandler.UseItem(selectedItemSlot, null);
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

    public void AddItem(Item item)
    {
        combatHandler.AddItemToInventory(item);
    }

    public Item SlotToItemName(int slot)
    {
        return combatHandler.GetInventory().items[slot - 1];
    }

    public void ResetItems()
    {
        combatHandler.Reset();
    }
    public void Reset()
    {
        combatHandler.FullReset();
    }

    public void GiveMoney(int amount)
    {
        money += amount;
    }
    public void TakeMoney(int amount)
    {
        money -= amount;
    }
}
