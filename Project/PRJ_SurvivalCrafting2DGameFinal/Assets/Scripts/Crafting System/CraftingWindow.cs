using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// En este código, la clase CraftingWindow maneja la interfaz de usuario de la ventana de crafteo. Permite mostrar las recetas de crafteo disponibles y manejar eventos relacionados con los espacios de ítems.
/// _recipeUIPrefab es el prefabricado de la interfaz de usuario de la receta de crafteo.
/// _recipeUIParent es el objeto transform del padre que contiene las interfaces de usuario de las recetas de crafteo.
/// _craftingRecipeUIs es una lista de interfaces de usuario de las recetas de crafteo.
/// El método OnValidate() se ejecuta cuando se valida el script, y llama al método Init() para inicializar la ventana de crafteo.
/// El método Start() se ejecuta al iniciar el script y también llama a Init(), luego se suscribe a los eventos de las interfaces de usuario de las recetas de crafteo para los eventos de entrada y salida del cursor.
/// El método Init() obtiene todas las interfaces de usuario de las recetas de crafteo dentro del padre _recipeUIParent, incluso las que están inactivas. 
/// Luego llama a UpdateCraftingRecipes() para actualizar las recetas de crafteo mostradas en la ventana.
/// El método UpdateCraftingRecipes() actualiza las interfaces de usuario de las recetas de crafteo según la lista de craftingRecipes. 
/// Compara la cantidad de recetas de crafteo con la cantidad de interfaces de usuario disponibles. Si no hay suficientes interfaces de usuario, se instancian nuevas utilizando el prefabricado _recipeUIPrefab. 
/// Si hay interfaces de usuario nulas, se reemplazan por nuevas instancias. Luego se asignan el itemContainer correspondiente y la receta de crafteo a cada interfaz de usuario. 
/// Finalmente, se limpian las recetas de crafteo de las interfaces de usuario que no tienen una receta asociada.
/// En resumen, este código administra la ventana de crafteo y se encarga de mostrar y actualizar las recetas de crafteo en la interfaz de usuario
/// </summary>
public class CraftingWindow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CraftingRecipeUI _recipeUIPrefab; // Prefabricado de la interfaz de usuario de la receta de crafteo
    [SerializeField] RectTransform _recipeUIParent; // Padre de las interfaces de usuario de las recetas de crafteo
    [SerializeField] List<CraftingRecipeUI> _craftingRecipeUIs; // Lista de interfaces de usuario de las recetas de crafteo

    [Header("Public Variables")]
    public ItemContainer itemContainer; // Contenedor de los ítems
    public List<CraftingRecipe> craftingRecipes; // Lista de recetas de crafteo

    public event Action<BaseItemSlot> OnPointerEnterEvent; // Evento que se dispara cuando el cursor entra en un espacio de ítem
    public event Action<BaseItemSlot> OnPointerExitEvent; // Evento que se dispara cuando el cursor sale de un espacio de ítem

    private void OnValidate()
    {
        Init();
    }

    private void Start()
    {
        Init();

        foreach (CraftingRecipeUI craftingRecipeUI in _craftingRecipeUIs)
        {
            craftingRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            craftingRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
        }
    }

    private void Init()
    {
        _recipeUIParent.GetComponentsInChildren<CraftingRecipeUI>(includeInactive: true, result: _craftingRecipeUIs); // Obtener las interfaces de usuario de las recetas de crafteo dentro del padre, incluso las inactivas
        UpdateCraftingRecipes();
    }

    public void UpdateCraftingRecipes()
    {
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            if (_craftingRecipeUIs.Count == i)
            {
                _craftingRecipeUIs.Add(Instantiate(_recipeUIPrefab, _recipeUIParent, false)); // Instanciar una nueva interfaz de usuario de la receta de crafteo si no hay suficientes
            }
            else if (_craftingRecipeUIs[i] == null)
            {
                _craftingRecipeUIs[i] = Instantiate(_recipeUIPrefab, _recipeUIParent, false); // Reemplazar una interfaz de usuario de receta de crafteo nula con una nueva instancia
            }

            _craftingRecipeUIs[i].itemContainer = itemContainer;
            _craftingRecipeUIs[i].CraftingRecipe = craftingRecipes[i];
        }

        for (int i = craftingRecipes.Count; i < _craftingRecipeUIs.Count; i++)
        {
            _craftingRecipeUIs[i].CraftingRecipe = null; // Limpiar las recetas de crafteo de las interfaces de usuario que no tienen una receta asociada
        }
    }
}