using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private int minCost;
    [SerializeField] private int randomCost;
    [SerializeField] private SpriteRenderer dice;
    [SerializeField] private SpriteRenderer slot1;
    [SerializeField] private SpriteRenderer slot2;
    [SerializeField] private SpriteRenderer slot3;
    [SerializeField] private SpriteRenderer slot4;
    [SerializeField] private SpriteRenderer slot5;

    private Dictionary<int, Item> possibleItems;
    private List<Die> possibleDice;

    private ShopItem[] itemsInShop;

    // Start is called before the first frame update
    void Start()
    {
        possibleItems = new Dictionary<int, Item>();
        possibleDice = new List<Die>();
        itemsInShop = new ShopItem[6];

        possibleItems.Add(0, new BetterShield());
        possibleItems.Add(1,new BetterSword());
        possibleItems.Add(2, new Blunt());
        possibleItems.Add(3, new DiceBag());
        possibleItems.Add(4, new GamblingAddict());
        possibleItems.Add(5, new GreaterShield());
        possibleItems.Add(6, new GreaterSword());
        possibleItems.Add(7,new HealingGem());
        possibleItems.Add(8, new MadDice());
        possibleItems.Add(9, new Mulligan());
        possibleItems.Add(10, new PepperSpray());
        possibleItems.Add(11, new ReallyBigShield()); ;
        possibleItems.Add(12, new SacrificialPact());
        possibleItems.Add(13, new StarterShield());
        possibleItems.Add(14, new StarterSword());
        possibleItems.Add(15, new SugarRush());
        possibleItems.Add(16, new TheChalice());
        possibleItems.Add(17, new Whip());

        possibleItems.Add(18, new IntimidatingDrip());
        possibleItems.Add(19, new MagicDice());
        possibleItems.Add(20, new LoadedDice());

        Die d6 = Die.CreateDice(6);
        Die d8 = Die.CreateDice(8);
        Die d10 = Die.CreateDice(10);
        Die d12 = Die.CreateDice(12);
        possibleDice.Add(d6);
        possibleDice.Add(d8);
        possibleDice.Add(d10);
        possibleDice.Add(d12);

        
        UpdateShop();
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateSprites()
    {
        dice.sprite =  Resources.Load<Sprite>("Sprites/d" + itemsInShop[0].die.MaxRoll() + "/" + itemsInShop[0].die.MaxRoll() + "d" + itemsInShop[0].die.MaxRoll());
        slot1.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemsInShop[1].item.GetName());
        slot2.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemsInShop[2].item.GetName());
        slot3.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemsInShop[3].item.GetName());
        slot4.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemsInShop[4].item.GetName());
        slot5.sprite = Resources.Load<Sprite>("Sprites/Items/" + itemsInShop[5].item.GetName());
    }

    private void GenerateItems()
    {
        itemsInShop[0] = new ShopItem(possibleDice[Random.Range(0, possibleDice.Count)]);

        for(int i = 1; i < itemsInShop.Length; i++)
        {
            itemsInShop[i] = new ShopItem(possibleItems[Random.Range(0, possibleItems.Count)], minCost);
        }
    }

    public void UpdateShop()
    {
        GenerateItems();
        UpdateSprites();
    }

    public ShopItem GetItemInSlot(int slot)
    {
        return itemsInShop[slot];
    }
    public ShopItem[] GetItems()
    {
        return itemsInShop;
    }
    public Item BuyItem(int slot)
    {
        ShopItem toBeReplaced = itemsInShop[slot];
        itemsInShop[slot] = null;
        switch (slot)
        {
            case 1:
                slot1.gameObject.SetActive(false);
                break;
            case 2:
                slot2.gameObject.SetActive(false);
                break;
            case 3:
                slot3.gameObject.SetActive(false);
                break;
            case 4:
                slot4.gameObject.SetActive(false);
                break;
            case 5:
                slot5.gameObject.SetActive(false);
                break;
        }
        return toBeReplaced.item;
    }
    public Die BuyDie()
    {
        ShopItem toBeReplaced = itemsInShop[0];
        itemsInShop[0] = null;
        dice.gameObject.SetActive(false);
        return toBeReplaced.die;
    }
}

public class ShopItem
{
    public Item item;
    public int cost;
    public Die die;
    public ShopItem(Item shopItem, int itemCost)
    {
        item = shopItem;
        cost = itemCost;
    }
    public ShopItem(Die shopDice)
    {
        die = shopDice;
        cost = die.MaxRoll();
    }
}
