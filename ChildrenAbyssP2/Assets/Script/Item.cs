using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public string description;
    public Sprite icon;

    [HideInInspector]
    public bool pickedUp;
    [HideInInspector]
    public bool equiped;

    [HideInInspector]
    public GameObject weaponManager;
    [HideInInspector]
    public GameObject weapon;

    public bool playersWeapon;

    public void Start()
    {
        weaponManager = GameObject.FindWithTag("Weaponmanager");
        if (!playersWeapon)
        {
            int allweapons = weaponManager.transform.childCount;
            for (int i = 0; i < allweapons; i++)
            {
                if (weaponManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    weapon = weaponManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }

    private void Update()
    {
        if (equiped)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                equiped = false;
            }
            if (equiped == false)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void ItemUsage()
    {
        if (type == "Weapon")
        {
            weapon.SetActive(true);
            weapon.GetComponent<Item>().equiped = true;
        }
    }
}
