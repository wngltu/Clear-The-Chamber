using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController: MonoBehaviour
{
    public static playerController Instance;

    public CharacterController controller;

    public Transform groundChecker;
    public float groundDistance = .001f;
    public LayerMask groundMask;
    bool isGrounded;

    public Slider playerhealthBar;
    public Text playerhealthText;
    public Text playermaxhealthText;

    public float currentHealth = 15f;
    public float maxHealth = 100f;

    public Slider playerenergyBar;
    public Text playerenergyText;

    public Text chamberText;

    public float currentEnergy = 0f;
    public float maxEnergy = 1000f;

    public float speed = 12f;
    public float gravityforce = -25f;

    public ParticleSystem loseParticles;
    public ParticleSystem deathParticles;

    public GameObject deathCam;
    public GameObject gameoverPanel;

    public bool won = false;

    Vector3 vel;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        currentHealth = maxHealth;
        playerhealthBar.maxValue = maxHealth;
        playerhealthBar.value = maxHealth;
        playerhealthText.text = currentHealth.ToString();

        currentEnergy = 0;
        playerenergyBar.maxValue = maxEnergy;
        playerenergyBar.value = currentEnergy;
        playerenergyText.text = currentEnergy.ToString();

        FindObjectOfType<soundManager>().Play("Chamber1");
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && vel.y < 0) //smoother gravity on collision (to prevent floating)
        {
            vel.y = -2;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vel.y = 8f;
            FindObjectOfType<soundManager>().Play("jump");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (shopManager.Instance.open == true)
            {
                shopManager.Instance.CloseShop();
            }
            if (doorManager.Instance.door1IsOpen == true)
            {
                doorManager.Instance.CloseDoor1Menu();
            }
            if (doorManager.Instance.door2IsOpen == true)
            {
                doorManager.Instance.CloseDoor2Menu();
            }
        }

        if (won == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        float x = Input.GetAxis("Horizontal"); //ad
        float z = Input.GetAxis("Vertical"); //ws

        Vector3 move = transform.right * x + transform.forward * z; //movement wasd

        controller.Move(move * speed * Time.deltaTime);

        vel.y += gravityforce * Time.deltaTime; //gravity

        controller.Move(vel * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            TakeDamage(15);
        }
        if (other.gameObject.CompareTag("enemy2"))
        {
            TakeDamage(25);
        }
        if (other.gameObject.CompareTag("killtrigger"))
        {
            TakeDamage(playerController.Instance.currentHealth);
        }
        if (other.gameObject.CompareTag("chamber2"))
        {
            waveManager.Instance.wave = 2;
            waveManager.Instance.enemiesLeft = 15;
            waveManager.Instance.spawnsLeft = 15;
            waveManager.Instance.UpdateEnemyCount();
            chamberText.text = "Chamber 2";
            FindObjectOfType<soundManager>().Play("Chamber2");
            FindObjectOfType<soundManager>().Pause("shop");
        }
        if (other.gameObject.CompareTag("chamber3"))
        {
            waveManager.Instance.wave = 3;
            waveManager.Instance.enemiesLeft = 25;
            waveManager.Instance.spawnsLeft = 25;
            waveManager.Instance.UpdateEnemyCount();
            chamberText.text = "Chamber 3";
            FindObjectOfType<soundManager>().Play("Chamber3");
            FindObjectOfType<soundManager>().Pause("shop");
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("door1") && waveManager.Instance.enemiesLeft <= 0)
        {
            doorManager.Instance.Door1Open();
        }
        if (hit.gameObject.CompareTag("door2") && waveManager.Instance.enemiesLeft <= 0)
        {
            doorManager.Instance.Door2Open();
        }
        if (hit.gameObject.CompareTag("shop"))
        {
            shopManager.Instance.OpenShop();
        }
    }

    public void GainEnergy(float energy)
    {
        currentEnergy = currentEnergy + energy;
        playerenergyBar.value = currentEnergy;
        playerenergyText.text = currentEnergy.ToString();
    }
    public void LoseEnergy(float energy)
    {
        currentEnergy = currentEnergy - energy;
        playerenergyBar.value = currentEnergy;
        playerenergyText.text = currentEnergy.ToString();
        loseParticles.Play();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        playerhealthBar.value = currentHealth;
        playerhealthText.text = currentHealth.ToString();

        if (currentHealth <= 0f)
        {
            currentHealth = 0;
            playerhealthBar.value = currentHealth;
            playerhealthText.text = currentHealth.ToString();
            Instantiate(deathCam, transform.position, transform.rotation);
            Instantiate(deathParticles, transform.position, transform.rotation);
            waveManager.Instance.disableSpawn = true;
            gameoverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateEnergy()
    {
        playerenergyBar.value = currentEnergy;
        playerenergyText.text = currentEnergy.ToString();
    }
    public void UpdateHealth()
    {
        playerhealthBar.value = currentHealth;
        playerhealthText.text = currentHealth.ToString();
    }
    public void UpdateHealthMax()
    {
        playerhealthBar.maxValue = maxHealth;
        playermaxhealthText.text = "/" + maxHealth.ToString();
    }
}
