using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase abstracta que representa un contenedor de objetos.
/// </summary>
public abstract class ItemContainer : MonoBehaviour, IItemContainer
{
    /// <summary>
    /// Lista de espacios de objetos.
    /// </summary>
    public List<ItemSlot> itemSlots;

    public event Action<BaseItemSlot> OnPointerEnterEvent;
    public event Action<BaseItemSlot> OnPointerExitEvent;
    public event Action<BaseItemSlot> OnRightClickEvent;
    public event Action<BaseItemSlot> OnBeginDragEvent;
    public event Action<BaseItemSlot> OnEndDragEvent;
    public event Action<BaseItemSlot> OnDragEvent;
    public event Action<BaseItemSlot> OnDropEvent;

    /// <summary>
    /// Método que se llama cuando se valida el componente.
    /// Obtiene los componentes hijos de tipo ItemSlot.
    /// </summary>
    protected virtual void OnValidate()
    {
        GetComponentsInChildren(includeInactive: true, result: itemSlots);
    }

    /// <summary>
    /// Método que se llama al despertar el componente.
    /// Asigna los eventos de los slots de objetos.
    /// </summary>
    protected virtual void Awake()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].OnPointerEnterEvent += slot => EventHelper(slot, OnPointerEnterEvent);
            itemSlots[i].OnPointerExitEvent += slot => EventHelper(slot, OnPointerExitEvent);
            itemSlots[i].OnRightClickEvent += slot => EventHelper(slot, OnRightClickEvent);
            itemSlots[i].OnBeginDragEvent += slot => EventHelper(slot, OnBeginDragEvent);
            itemSlots[i].OnEndDragEvent += slot => EventHelper(slot, OnEndDragEvent);
            itemSlots[i].OnDragEvent += slot => EventHelper(slot, OnDragEvent);
            itemSlots[i].OnDropEvent += slot => EventHelper(slot, OnDropEvent);
        }
    }

    /// <summary>
    /// Método auxiliar para invocar eventos.
    /// </summary>
    /// <param name="itemSlot">El slot de objeto afectado.</param>
    /// <param name="action">La acción a invocar.</param>
    private void EventHelper(BaseItemSlot itemSlot, Action<BaseItemSlot> action)
    {
        action?.Invoke(itemSlot);
    }

    /// <summary>
    /// Verifica si se puede agregar un objeto al contenedor.
    /// </summary>
    /// <param name="item">El objeto a agregar.</param>
    /// <param name="amount">La cantidad a agregar.</param>
    /// <returns><c>true</c> si se puede agregar, <c>false</c> de lo contrario.</returns>
    public virtual bool CanAddItem(Item item, int amount = 1)
    {
        int freeSpaces = 0;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.Item == null || itemSlot.Item.ID == item.ID)
            {
                freeSpaces += item.maximumStacks - itemSlot.Amount;
            }
        }
        return freeSpaces >= amount;
    }

    /// <summary>
    /// Agrega un objeto al contenedor.
    /// </summary>
    /// <param name="item">El objeto a agregar.</param>
    /// <returns><c>true</c> si se pudo agregar, <c>false</c> de lo contrario.</returns>
    public virtual bool AddItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Agrega una cantidad aleatoria de un objeto al contenedor.
    /// </summary>
    /// <param name="item">El objeto a agregar.</param>
    /// <param name="secondRandomDrop">La segunda gota aleatoria de objeto.</param>
    public virtual void AddRandomQuantityItem(Item item, Item secondRandomDrop)
    {
        int rndItem = UnityEngine.Random.Range(1, 6) * ((int)GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue + 1);
        Debug.Log(rndItem);
        for (int i = 0; i < rndItem; i++)
        {
            AddItem(item);
        }
        if (secondRandomDrop != null)
        {
            int rndSecond = UnityEngine.Random.Range(0, 4) * ((int)GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue + 1);
            Debug.Log(rndSecond);
            for (int r = 0; r < rndSecond; r++)
            {
                AddItem(secondRandomDrop);
            }
            GameObject.Find("Player").GetComponent<XP>().GetXP((int)Mathf.Round((rndItem + rndSecond) * (2.5f + (GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue / 10))));
        }
        else
        {
            GameObject.Find("Player").GetComponent<XP>().GetXP((int)Mathf.Round((rndItem) * (2f + (GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue / 15))));
        }

    }

    /// <summary>
    /// Agrega una cantidad aleatoria de objetos al contenedor obtenidos de enemigos.
    /// </summary>
    /// <param name="item">El objeto a agregar.</param>
    /// <param name="secondRandomDrop">La segunda gota aleatoria de objeto.</param>
    public virtual void AddRandomDropFromEnemies(Item item, Item secondRandomDrop)
    {
        int rndItem = UnityEngine.Random.Range(0, 3) * ((int)GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue + 1);
        Debug.Log(rndItem);
        for (int i = 0; i < rndItem; i++)
        {
            AddItem(item);
        }
        int rndSecond = UnityEngine.Random.Range(0, 2) * ((int)GameObject.Find("Player").GetComponent<Character>().intelligence.baseValue + 1);
        if (secondRandomDrop != null)
        {
            Debug.Log(rndSecond);
            for (int r = 0; r < rndSecond; r++)
            {
                AddItem(secondRandomDrop);
            }
        }
        GameObject.Find("Player").GetComponent<XP>().GetXP((rndItem + rndSecond) * 4);
    }

    /// <summary>
    /// Remueve un objeto del contenedor.
    /// </summary>
    /// <param name="item">El objeto a remover.</param>
    /// <returns><c>true</c> si se pudo remover, <c>false</c> de lo contrario.
    public virtual bool RemoveItem(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Remueve un objeto del contenedor mediante su ID.
    /// </summary>
    /// <param name="itemID">El ID del objeto a remover.</param>
    /// <returns>El objeto removido, o <c>null</c> si no se encontró.</returns>
    public virtual Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Amount--;
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Obtiene la cantidad de un objeto en el contenedor mediante su ID.
    /// </summary>
    /// <param name="itemID">El ID del objeto.</param>
    /// <returns>La cantidad del objeto en el contenedor.</returns>
    public virtual int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                number += itemSlots[i].Amount;
            }
        }
        return number;
    }

    /// <summary>
    /// Limpia el contenedor, removiendo todos los objetos.
    /// </summary>
    public void Clear()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].Item != null && Application.isPlaying)
            {
                itemSlots[i].Item.Destroy();
            }
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }
}