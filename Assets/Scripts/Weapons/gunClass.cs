using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gunClass : MonoBehaviour
{
    public static gunClass Instance;

    public float damage = 10f;
    public float range = 100f;

    public Camera playerCam;

    public ParticleSystem muzzleflash;
    public ParticleSystem bulletTrail;

    float cooldownTimer = .25f;
    float shootTimer;
    public float reloadTimer = 2.5f;

    public float magCap = 12;
    float currentMag = 12;

    public Text currentMagText;
    public Text magCapText;

    public bool isReloading = false;

    public Animation anim;

    public ParticleSystem[] muzzleFlash;

    private void Start()
    {
        Instance = this;
    }
    private void Update()
    {
        anim = weaponSwitch.Instance.wepGameObject.GetComponent<Animation>();
        muzzleFlash = weaponSwitch.Instance.wepGameObject.GetComponentsInChildren<ParticleSystem>();
        shootTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && shootTimer >= cooldownTimer && currentMag > 0)
        {
            Shoot();
            shootTimer = 0;
        }
        else if (Input.GetButtonDown("Fire1") && currentMag <= 0 && !isReloading)
        {
            anim.Play();
            isReloading = true;
            currentMag = 0;
            currentMagText.text = currentMag.ToString();
            Debug.Log("left click reloading");
            shootTimer = reloadTimer;
            Invoke("Reload", reloadTimer);
            FindObjectOfType<soundManager>().Play("pistolEmpty");
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            anim.Play();
            isReloading = true;
            currentMag = 0;
            currentMagText.text = currentMag.ToString();
            Debug.Log("reloading");
            shootTimer = reloadTimer;
            Invoke("Reload", reloadTimer);
        }
    }

    void Shoot()
    {
        muzzleFlash[0].Play();
        muzzleFlash[1].Play();
        FindObjectOfType<soundManager>().Play("pistolShoot");
        currentMag = currentMag-1;
        currentMagText.text = currentMag.ToString();

        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.CompareTag("enemy"))
            {
                enemyScript enemy = hit.transform.GetComponent<enemyScript>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
            if (hit.transform.CompareTag("critenemy"))
            {
                enemyScript enemy = hit.transform.GetComponentInParent<enemyScript>();
                if (enemy != null)
                {
                    enemy.TakeCritDamage(damage);
                }
            }
        }
    }
    void Reload()
    {
        Debug.Log("reloading function");
        currentMag = magCap;
        currentMagText.text = currentMag.ToString();
        isReloading = false;
    }
    public void IncreaseMag()
    {
        magCap += 4;
        magCapText.text = "/" + magCap.ToString();
    }
}
