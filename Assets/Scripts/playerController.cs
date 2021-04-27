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

    public float currentHealth = 15f;
    public float maxHealth = 100f;

    public Slider playerenergyBar;
    public Text playerenergyText;

    public float currentEnergy = 0f;
    public float maxEnergy = 1000f;

    public float speed = 12f;
    public float gravityforce = -25f;

    public ParticleSystem gainParticles;
    public ParticleSystem loseParticles;

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
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("door1") && currentEnergy >= 100)
        {
            Destroy(hit.gameObject);
            LoseEnergy(100);
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
        gainParticles.Play();
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
            Destroy(gameObject);
        }
    }
}
