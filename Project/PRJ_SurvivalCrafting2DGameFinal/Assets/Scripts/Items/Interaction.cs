using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que controla la interacción del jugador con los objetos del entorno.
/// </summary>
public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject _forgeInventory, _campfireInventory, _wellInventory, _workbenchInventory;
    public ItemContainer itemContainer;
    public Item item;
    public Item secondRandopDrop;
    public InteractableItem interactableItem;
    public int lifes, maxLifes;
    [SerializeField] GameObject _equippedItem;

    [SerializeField] FloatingHealthBar _healthBar;

    public AudioSource gathering;

    private void Awake()
    {
        _healthBar = GetComponentInChildren<FloatingHealthBar>();
        maxLifes = lifes;
    }

    /// <summary>
    /// Interactúa con un objeto interactuable.
    /// </summary>
    /// <param name="interactableItem">El transform del objeto interactuable.</param>
    public void Interact(Transform interactableItem)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (interactableItem.gameObject.layer)
            {
                case 6: // Capa de objeto recolectable
                    // Recoger recurso de un objeto (como un árbol)
                    if (_equippedItem.GetComponent<Image>().sprite != null)
                    {
                        switch (item.name)
                        {
                            case "Fibber":
                                if (_equippedItem.GetComponent<Image>().sprite.name.Contains(" Knife"))
                                {
                                    gathering.Play();
                                    lifes -= (int)GameObject.Find("Player").GetComponent<Character>().strength.Value;
                                    _healthBar.UpdateHealthBar(lifes, maxLifes);
                                }
                                break;
                            case "Wood":
                                if (_equippedItem.GetComponent<Image>().sprite.name.Contains(" Axe"))
                                {
                                    gathering.Play();
                                    lifes -= (int)GameObject.Find("Player").GetComponent<Character>().strength.Value;
                                    _healthBar.UpdateHealthBar(lifes, maxLifes);
                                }
                                break;
                            case "Stone":
                            case "Copper Ore":
                            case "Iron Ore":
                            case "Golden Ore":
                                if (_equippedItem.GetComponent<Image>().sprite.name.Contains(" Pickaxe"))
                                {
                                    gathering.Play();
                                    lifes -= (int)GameObject.Find("Player").GetComponent<Character>().strength.Value;
                                    _healthBar.UpdateHealthBar(lifes, maxLifes);
                                }
                                break;
                            default:
                                break;
                        }

                        if (lifes <= 0)
                        {
                            itemContainer.AddRandomQuantityItem(item, secondRandopDrop);
                            gathering.Play();
                            Destroy(this.gameObject);
                        }
                    }
                    break;
                case 7: // Capa de la forja
                    _forgeInventory.SetActive(!_forgeInventory.activeSelf);
                    _campfireInventory.SetActive(false);
                    _wellInventory.SetActive(false);
                    _workbenchInventory.SetActive(false);
                    break;
                case 8: // Capa de la fogata
                    _forgeInventory.SetActive(false);
                    _campfireInventory.SetActive(!_campfireInventory.activeSelf);
                    _wellInventory.SetActive(false);
                    _workbenchInventory.SetActive(false);
                    break;
                case 9: // Capa del pozo
                    _forgeInventory.SetActive(false);
                    _campfireInventory.SetActive(false);
                    _wellInventory.SetActive(!_wellInventory.activeSelf);
                    _workbenchInventory.SetActive(false);
                    break;
                case 10: // Capa del banco de trabajo
                    _forgeInventory.SetActive(false);
                    _campfireInventory.SetActive(false);
                    _wellInventory.SetActive(false);
                    _workbenchInventory.SetActive(!_workbenchInventory.activeSelf);
                    break;
                case 11: // Capa de interacción especial
                    Debug.Log("Has interacted with: " + interactableItem.gameObject.layer);
                    GameObject.Find("Player").GetComponent<Character>().currentHealth = GameObject.Find("Player").GetComponent<Character>().startHealth;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _forgeInventory.SetActive(false);
            _campfireInventory.SetActive(false);
            _wellInventory.SetActive(false);
            _workbenchInventory.SetActive(false);
        }
    }
}