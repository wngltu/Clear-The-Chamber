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
    public Transform playercoord;

    public ParticleSystem deathParticles;

    public float currentHealth = 15;
    public float health = 100f;

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
        if (isDying == false)
        {
            agent.destination = playercoord.position;
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
            deathParticles.Play();
            agent.isStopped = true;
            agent.speed = 0;
            Death();
            playerController.Instance.GainEnergy(50);
        }
    }
    public void TakeCritDamage(float damage)
    {
        currentHealth -= damage * 2;
        enemyHealthBar.value = currentHealth;
        enemyHealthText.text = (currentHealth).ToString();

        if (currentHealth <= 0f)
        {
            deathParticles.Play();
            agent.isStopped = true;
            agent.speed = 0;
            Death();
            playerController.Instance.GainEnergy(50);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
