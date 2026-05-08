using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// _recipeUIPrefab: Prefabricado de la interfaz de receta del banco de trabajo.
/// _recipeUIParent: Contenedor de las interfaces de receta del banco de trabajo.
/// _benchRecipeUIs: Lista de interfaces de receta del banco de trabajo.
/// itemContainer: Contenedor de objetos del banco de trabajo.
/// craftingRecipes: Lista de recetas de creación.
/// OnPointerEnterEvent: Evento que se activa cuando el puntero entra en un espacio de objeto.
/// OnPointerExitEvent: Evento que se activa cuando el puntero sale de un espacio de objeto.
/// Init(): Método para inicializar y configurar el banco de trabajo.
/// Start(): Método que se ejecuta al inicio del juego para configurar las interfaces de receta y asignar eventos.
/// UpdateCraftingRecipes(): Método para actualizar las interfaces de receta del banco de trabajo con las recetas de creación disponibles.
/// En el método Init(), se obtienen las interfaces de receta existentes en el contenedor _recipeUIParent mediante el uso de GetComponentsInChildren. Estas interfaces se almacenan en la lista _benchRecipeUIs.
/// En el método Start(), se asignan los eventos OnPointerEnterEvent y OnPointerExitEvent a cada interfaz de receta en la lista _benchRecipeUIs.
/// El método UpdateCraftingRecipes() se encarga de actualizar las interfaces de receta con las recetas de creación. Se itera sobre la lista de recetas craftingRecipes y se verifica si hay suficientes interfaces de receta en _benchRecipeUIs. 
/// Si no hay suficientes, se instancia una nueva interfaz utilizando Instantiate y se agrega al contenedor _recipeUIParent. Luego, se asigna el contenedor de objetos itemContainer y la receta de creación correspondiente a cada interfaz de receta.
/// Finalmente, se limpian las interfaces de receta sobrantes en caso de que la lista de recetas craftingRecipes sea más corta que la lista de interfaces _benchRecipeUIs. 
/// Esto se hace asignando null a la propiedad CraftingRecipe de las interfaces de receta sobrantes.
/// En resumen, este script se encarga de administrar y actualizar las interfaces de receta en un banco de trabajo, en base a las recetas de creación disponibles y el contenedor de objetos asociado. 
/// También proporciona eventos para el puntero al interactuar con las interfaces de receta.
///summary>
public class CraftingBenchWindows : MonoBehaviour
{
    [Header("References")]
    [SerializeField] BenchRecipeUI _recipeUIPrefab; // Prefab para la interfaz de receta del banco de trabajo
    [SerializeField] RectTransform _recipeUIParent; // Contenedor de las interfaces de receta del banco de trabajo
    [SerializeField] List<BenchRecipeUI> _benchRecipeUIs; // Lista de interfaces de receta del banco de trabajo

    [Header("Public Variables")]
    public ItemContainer itemContainer; // Contenedor de objetos del banco de trabajo
    public List<CraftingRecipe> craftingRecipes; // Lista de recetas de creación

    public event Action<BaseItemSlot> OnPointerEnterEvent; // Evento para cuando el puntero entra en un espacio de objeto
    public event Action<BaseItemSlot> OnPointerExitEvent; // Evento para cuando el puntero sale de un espacio de objeto

    private void OnValidate()
    {
        Init();
    }

    private void Start()
    {
        Init();

        // Asignar eventos a las interfaces de receta
        foreach (BenchRecipeUI benchRecipeUI in _benchRecipeUIs)
        {
            benchRecipeUI.OnPointerEnterEvent += slot => OnPointerEnterEvent(slot);
            benchRecipeUI.OnPointerExitEvent += slot => OnPointerExitEvent(slot);
        }
    }

    private void Init()
    {
        // Obtener las interfaces de receta del banco de trabajo existentes
        _recipeUIParent.GetComponentsInChildren<BenchRecipeUI>(includeInactive: true, result: _benchRecipeUIs);

        // Actualizar las recetas de creación
        UpdateCraftingRecipes();
    }

    public void UpdateCraftingRecipes()
    {
        // Actualizar las interfaces de receta con las recetas de creación
        for (int i = 0; i < craftingRecipes.Count; i++)
        {
            if (_benchRecipeUIs.Count == i)
            {
                _benchRecipeUIs.Add(Instantiate(_recipeUIPrefab, _recipeUIParent, false));
            }
            else if (_benchRecipeUIs[i] == null)
            {
                _benchRecipeUIs[i] = Instantiate(_recipeUIPrefab, _recipeUIParent, false);
            }

            _benchRecipeUIs[i].itemContainer = itemContainer;
            _benchRecipeUIs[i].CraftingRecipe = craftingRecipes[i];
        }

        // Limpiar las interfaces de receta sobrantes
        for (int i = craftingRecipes.Count; i < _benchRecipeUIs.Count; i++)
        {
            _benchRecipeUIs[i].CraftingRecipe = null;
        }
    }
}