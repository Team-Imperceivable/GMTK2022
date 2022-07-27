using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float slideSpeed;
    [SerializeField] private List<GameObject> encounterPool;
    [SerializeField] private List<GameObject> exclusiveEncounters;
    [SerializeField] private int maxEnemyNumbers, minEnemyNumbers;
    [SerializeField] private GameObject enemyUI;

    public int enemiesAlive;

    private Transform[] enemies;
    private bool sliding;
    private GameObject player;
    private List<UIFollowTarget> followers;
    private PlayerController pc;

    private int scaling;
    private SFXHandler sfx;

    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        player = GameObject.FindWithTag("Player");
        GenerateEncounter();
        gameObject.SetActive(false);
        followers = new List<UIFollowTarget>();
        pc = player.GetComponent<PlayerController>();
        sfx = player.GetComponentInChildren<SFXHandler>();
    }

    void Update()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sliding && transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slideSpeed * Time.deltaTime);
        }
        if(transform.position == targetPosition)
        {
            sliding = false;
        }
    }

    public void SetEncounterActive(bool active)
    {
        gameObject.SetActive(active);
        foreach(Transform transform in enemies)
        {
            transform.gameObject.SetActive(active);
        }
        foreach(UIFollowTarget follower in followers)
        {
            Destroy(follower.gameObject);
        }
    }

    public void ApplyScaling(int amount)
    {
        scaling = amount;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }

    public void SlideToTarget()
    {
        sliding = true;
    }

    public void GenerateEncounter()
    {
        enemiesAlive = Random.Range(minEnemyNumbers, maxEnemyNumbers + 1);
        for(int i = 0; i < enemiesAlive; i++)
        {
            GameObject selected = encounterPool[Random.Range(0, encounterPool.Count)];
            Vector3 startPos = transform.position;
            startPos.x += 10 * i;
            GameObject instantiatedObject = Instantiate(selected, startPos, transform.rotation);
            instantiatedObject.transform.parent = transform;
            EnemyCombatHandler enemyCombatHandler = instantiatedObject.GetComponent<EnemyCombatHandler>();
            
            if (enemyCombatHandler != null)
            {
                enemyCombatHandler.TargetPlayer(player);
            }
            if (i == 0)
            {
                if (exclusiveEncounters.Contains(selected))
                {
                    enemiesAlive = 1;
                    if (selected.CompareTag("Shop"))
                        targetPosition.x += 16;
                    break;
                }
                foreach(GameObject exclusive in exclusiveEncounters)
                {
                    while(encounterPool.Contains(exclusive))
                    {
                        encounterPool.Remove(exclusive);
                    }
                }
            }
        }
    }

    public void GenerateUI()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform enemy in enemies)
        {
            if(enemy != transform && enemy.CompareTag("Enemy"))
            {
                BuffTargetHealth(enemy);
                BuffTargetDamage(enemy);
                BuffTargetMoney(enemy);
                GameObject instantiated = Instantiate(enemyUI, enemy.position, enemy.rotation);
                instantiated.transform.SetParent(GameObject.Find("Enemy UIs").transform);
                UIFollowTarget follower = instantiated.GetComponent<UIFollowTarget>();
                follower.SetTarget(enemy);
                follower.SetHandler(enemy.GetComponent<EnemyCombatHandler>());
                follower.UpdateOffset(enemy.GetComponent<Collider2D>().bounds);
                followers.Add(follower);
            }
        }
    }

    private void BuffTargetHealth(Transform target)
    {
        EnemyCombatHandler enemyCombatHandler = target.gameObject.GetComponent<EnemyCombatHandler>();
        if(enemyCombatHandler != null)
        {
            enemyCombatHandler.BuffHealth(scaling);
        }
    }

    private void BuffTargetDamage(Transform target)
    {
        EnemyCombatHandler enemyCombatHandler = target.gameObject.GetComponent<EnemyCombatHandler>();
        if (enemyCombatHandler != null)
        {
            enemyCombatHandler.BuffDamage(scaling);
        }
    }

    private void BuffTargetMoney(Transform target)
    {
        EnemyCombatHandler enemyCombatHandler = target.gameObject.GetComponent<EnemyCombatHandler>();
        if (enemyCombatHandler != null)
        {
            enemyCombatHandler.BuffMoney(scaling);
        }
    }

    public void EnemyDeath(int moneyDrop)
    {
        sfx.PlayEnemyDeath();
        enemiesAlive--;
        pc.GiveMoney(moneyDrop);
        if (enemiesAlive == 0)
        {
            SendMessageUpwards("NextEncounter");
        }
    }

    private void UpdateUI()
    {
        foreach(UIFollowTarget follower in followers)
        {
            follower.UpdatePosition();
        }
    }
}
