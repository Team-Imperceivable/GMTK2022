using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float slideSpeed;
    [SerializeField] private List<GameObject> encounterPool;
    [SerializeField] private List<GameObject> exclusiveEncounters;
    [SerializeField] private int maxEnemyNumbers;
    [SerializeField] private GameObject enemyUI;

    private Transform[] enemies;
    private bool sliding;
    private GameObject player;
    private int enemiesAlive;
    private List<UIFollowTarget> followers;

    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        player = GameObject.FindWithTag("Player");
        GenerateEncounter();
        gameObject.SetActive(false);
        followers = new List<UIFollowTarget>();
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
        enemiesAlive = Random.Range(1, maxEnemyNumbers + 1);
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
                        targetPosition.x += 20;
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

    public void EnemyDeath()
    {
        enemiesAlive--;
        if(enemiesAlive == 0)
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
