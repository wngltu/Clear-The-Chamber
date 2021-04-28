using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    public Text enemyHealthText;
    public Slider enemyHealthBar;

    NavMeshAgent agent;
    public GameObject temp;
    public GameObject deathEffects;
    public Transform playercoord;


    public float currentHealth = 15;
    public float health = 100f;

    bool isDying = false;

    float spawnTime;
    float timer = 0f;

    private void Start()
    {
        spawnTime = Random.Range(1,2);
        currentHealth = health;
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value = health;
        enemyHealthText.text = currentHealth.ToString();

        playercoord = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if (isDying == false && playercoord.position != null && timer >= spawnTime)
        {
            agent.destination = playercoord.position;
        }
        else if (playercoord == null)
        {
            agent.destination = transform.position;
        }
        if (isDying == true)
        {
            agent.updatePosition = false;
        }
    }
    public void TakeDamage (float damage)
    {
        currentHealth -= damage;
        enemyHealthBar.value = currentHealth;
        enemyHealthText.text = currentHealth.ToString();

        if (currentHealth <= 0f)
        {
            agent.isStopped = true;
            agent.speed = 0;
            Death();
            playerController.Instance.GainEnergy(Random.Range(20,45));
        }
    }
    public void TakeCritDamage(float damage)
    {
        currentHealth -= damage * 2;
        enemyHealthBar.value = currentHealth;
        enemyHealthText.text = (currentHealth).ToString();

        if (currentHealth <= 0f)
        {
            agent.isStopped = true;
            agent.speed = 0;
            Death();
            playerController.Instance.GainEnergy(Random.Range(30,60));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(deathEffects, transform.position, transform.rotation);
            waveManager.Instance.enemiesLeft--;
            waveManager.Instance.UpdateEnemyCount();
            Destroy(gameObject);
        }
    }
    public void Death()
    {
        Instantiate(deathEffects, transform.position, transform.rotation);
        waveManager.Instance.enemiesLeft--;
        waveManager.Instance.UpdateEnemyCount();
        Destroy(gameObject);
    }
}
