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

    private Transform[] enemies;
    private bool sliding;
    private GameObject player;
    private int enemiesAlive;

    // Start is called before the first frame update
    void Start()
    {
        enemies = gameObject.GetComponentsInChildren<Transform>(true);
        player = GameObject.FindWithTag("Player");
        GenerateEncounter();
        gameObject.SetActive(false);
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
                enemyCombatHandler.TargetPlayer(player);
            if (i == 0)
            {
                if (exclusiveEncounters.Contains(selected))
                {
                    enemiesAlive = 1;
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

    public void EnemyDeath()
    {
        enemiesAlive--;
        if(enemiesAlive == 0)
        {
            SendMessageUpwards("NextEncounter");
        }
    }
}
