using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waveManager : MonoBehaviour
{
    public static waveManager Instance;

    public int wave = 1;
    public GameObject[] enemyArray;
    public GameObject[] spawnerArray;
    public GameObject winPanel;

    public Text enemyCountText;

    bool bossSpawned = false;
    float bosscooldown = 10f;
    float cooldown = 6f;
    float timer = 0f;

    int lastSpawn = -1;

    public int spawnsLeft = 7;
    public int enemiesLeft = 11;

    public bool disableSpawn = false;
    private void Start()
    {
        Instance = this;
        enemyCountText.text = enemiesLeft.ToString();
    }

    private void Update()
    {
        if (spawnsLeft > 0 && disableSpawn == false && wave == 1)
        {
            timer += Time.deltaTime;
            if (timer > cooldown)
            {  
                timer = 0f;
                spawnEnemy1();
            }
        }
        if (spawnsLeft > 0 && disableSpawn == false && wave == 2)
        {
            cooldown = 4f;
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                timer = 0f;
                spawnEnemy2();
            }
        }
        if (spawnsLeft > 0 && disableSpawn == false && wave == 3)
        {
            cooldown = 3f;
            timer += Time.deltaTime;
            if (timer > cooldown)
            {
                timer = 0f;
                spawnEnemy3();
            }
        }
        if (enemiesLeft <= 0 && disableSpawn == false && wave == 3)
        {
            winPanel.SetActive(true);
            playerController.Instance.won = true;
        }
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = enemiesLeft.ToString();
    }
    void spawnEnemy1()
    {
        int i = Random.Range(0, 3); //random enemy (from enemy array)
        int n = Random.Range(0, 3); //random spawn point (from spawn array)
        int p = Random.Range(-5, 5); //spawn point random range
        if (n != lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n].transform.position - new Vector3(p, 0, p), spawnerArray[n].transform.rotation);
            lastSpawn = n;
        }
        else if (n == lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n + 1].transform.position - new Vector3(p, 0, p), spawnerArray[n+1].transform.rotation);
            lastSpawn = n + 1;
        }
        spawnsLeft--;
    }
    void spawnEnemy2()
    {
        int i = Random.Range(0, 5); //random enemy (from enemy array)
        int n = Random.Range(4, 7); //random spawn point (from spawn array)
        int p = Random.Range(-5, 5); //spawn point random range
        if (n != lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n].transform.position - new Vector3(p, 0, p), spawnerArray[n].transform.rotation);
            lastSpawn = n;
        }
        else if (n == lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n + 1].transform.position - new Vector3(p, 0, p), spawnerArray[n + 1].transform.rotation);
            lastSpawn = n + 1;
        }
        spawnsLeft--;
    }
    void spawnEnemy3()
    {
        int i = Random.Range(0, 6); //random enemy (from enemy array)
        int n = Random.Range(8, 11); //random spawn point (from spawn array)
        int p = Random.Range(-5, 5); //spawn point random range
        if (n != lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n].transform.position - new Vector3(p, 0, p), spawnerArray[n].transform.rotation);
            lastSpawn = n;
        }
        else if (n == lastSpawn)
        {
            Instantiate(enemyArray[i], spawnerArray[n].transform.position - new Vector3(p, 0, p), spawnerArray[n].transform.rotation);
            lastSpawn = n + 1;
        }
        spawnsLeft--;
    }
}
