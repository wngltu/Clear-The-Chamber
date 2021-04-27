﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    public GameObject wepGameObject;

    public static weaponSwitch Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                wepGameObject = weapon.gameObject;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        Invoke("SelectWeapon", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        int previousWep = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && gunClass.Instance.isReloading == false) //scroll down next wep.
        {
            if (selectedWeapon >= transform.childCount -1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") >  0f && gunClass.Instance.isReloading == false) //scroll up prev. wep
        {
            if (selectedWeapon < 1)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (previousWep != selectedWeapon) //when wep selected is different, call method
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                wepGameObject = weapon.gameObject;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
        if (selectedWeapon == 0)
        {
            gunClass.Instance.damage = 10f;
            gunClass.Instance.range = 100f;
            gunClass.Instance.magCap = 12;
            gunClass.Instance.magCapText.text = "/12";
            gunClass.Instance.reloadTimer = 2.5f;
        }

        if (selectedWeapon == 1)
        {
            gunClass.Instance.damage = 5f;
            gunClass.Instance.range = 50f;
            gunClass.Instance.magCap = 25;
            gunClass.Instance.magCapText.text = "/25";
            gunClass.Instance.reloadTimer = 3f;
        }

        if (selectedWeapon == 2)
        {
            gunClass.Instance.damage = 20f;
            gunClass.Instance.range = 150f;
            gunClass.Instance.magCap = 30;
            gunClass.Instance.magCapText.text = "/30";
            gunClass.Instance.reloadTimer = 4.5f;
        }
    }
}
