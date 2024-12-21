using System.Collections;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] float spawnGracePeriod;
    [SerializeField] float delayCheck;
    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        StartCoroutine(EnemyGracePeriod());
    }

    private void Update()
    {
        ManageStates();
    }

    void ManageStates()
    {
        //CheckCircles();
    }



    IEnumerator EnemyGracePeriod()
    {
        enemy.enemyState = EnemyState.Spawning;
        yield return new WaitForSeconds(spawnGracePeriod);
        EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
        enemyHealth.canBeDamaged = true;
        enemy.enemyState = EnemyState.Idle;

        //TODO: probably should add a fade in effect, that the enemy is spawning, playing with its transparency
    }
}
