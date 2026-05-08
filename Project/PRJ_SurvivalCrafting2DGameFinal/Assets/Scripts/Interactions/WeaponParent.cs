using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponParent : MonoBehaviour
{
    public int selectedWeapon = 0;

    // public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public Vector2 direction;
    public Weapon[] weapons;
    public Image wImage;

    private void Start()
    {
        SelectWeapon();
    }

    private void Update()
    {
        // Obtiene la dirección hacia la posición del puntero
        direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        // Actualiza la escala del personaje y del arma según la dirección
        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
        transform.parent.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector2(scale.y, scale.x);
        Debug.Log(transform.parent.GetComponentInChildren<SpriteRenderer>().transform.localScale);

        SelectWeapon();
    }
    
    private void SelectWeapon()
    {
        if (wImage.GetComponent<Image>().sprite != null)
        {
            string iconName = wImage.GetComponent<Image>().sprite.name.Substring(0, wImage.GetComponent<Image>().sprite.name.Length-1);
            foreach (Weapon weapon in weapons)
            {
                
                string wName = weapon.weaponRenderer.sprite.name.Substring(0, weapon.weaponRenderer.sprite.name.Length - 1);
                if (wName == iconName)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public void Attack()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].gameObject.activeSelf)
            {
                weapons[i].Attack();
            }
        }
    }
}