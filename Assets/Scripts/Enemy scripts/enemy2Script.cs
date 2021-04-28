using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class enemy2Script : MonoBehaviour
{
    public Text enemyHealthText;
    public Slider enemyHealthBar;

    NavMeshAgent agent;
    public GameObject temp;
    public GameObject deathEffects;
    public Transform playercoord;


    public float currentHealth = 15;
    public float health = 150f;

    bool isDying = false;

    private void Start()
    {
        currentHealth = health;
        enemyHealthBar.maxValue = health;
        enemyHealthBar.value = health;
        enemyHealthText.text = currentHealth.ToString();

        playercoord = GameObject.Find("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (isDying == false && playercoord.position != null)
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
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        enemyHealthBar.value = currentHealth;
        enemyHealthText.text = currentHealth.ToString();

        if (currentHealth <= 0f)
        {
            agent.isStopped = true;
            agent.speed = 0;
            Death();
            playerController.Instance.GainEnergy(Random.Range(45,60));
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
            playerController.Instance.GainEnergy(Random.Range(55,85));
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
