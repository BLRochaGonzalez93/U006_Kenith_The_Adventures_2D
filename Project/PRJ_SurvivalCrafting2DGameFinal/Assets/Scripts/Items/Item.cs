using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// La clase item es la clase base que representa un objeto en el juego.
/// Esta clase hereda de ScriptableObject y puede ser creada como un objeto Scriptable en el editor de Unity.
/// El campo id almacena un identificador único del objeto.
/// El campo itemName representa el nombre del objeto.
/// El campo icon es el sprite que se utiliza como icono del objeto.
/// El campo maximumStacks indica la cantidad máxima de pilas que puede tener el objeto.
/// La variable _sb es una instancia de StringBuilder utilizada para construir cadenas de texto.
/// El método OnValidate es invocado cuando se valida el objeto en el editor y asigna un identificador único al objeto en base a su ruta de archivo.
/// El método GetCopy devuelve una copia del objeto actual.
/// El método Destroy es virtual y puede ser implementado en clases derivadas para definir la lógica de destrucción del objeto.
/// El método GetItemType es virtual y puede ser implementado en clases derivadas para obtener el tipo de objeto.
/// El método GetDescription es virtual y puede ser implementado en clases derivadas para obtener la descripción del objeto
/// </summary>
[CreateAssetMenu(menuName = "Items/item")]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string itemName; // Nombre del objeto
    public Sprite icon; // Icono del objeto
    [Range(1, 999)]
    public int maximumStacks = 1; // Cantidad máxima de pilas que puede tener el objeto

    protected static readonly StringBuilder sb = new();

#if UNITY_EDITOR
    /// <summary>
    /// Método invocado cuando se valida el objeto en el editor.
    /// Asigna un identificador único al objeto en base a su ruta de archivo.
    /// </summary>
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    /// <summary>
    /// Obtiene una copia del objeto actual.
    /// </summary>
    /// <returns>Una copia del objeto.</returns>
    public virtual Item GetCopy()
    {
        return this;
    }

    /// <summary>
    /// Destruye el objeto.
    /// </summary>
    public virtual void Destroy()
    {
        // Implementación específica de destrucción del objeto en clases derivadas
    }

    /// <summary>
    /// Obtiene el tipo de objeto.
    /// </summary>
    /// <returns>El tipo de objeto.</returns>
    public virtual string GetItemType()
    {
        return "";
    }

    /// <summary>
    /// Obtiene la descripción del objeto.
    /// </summary>
    /// <returns>La descripción del objeto.</returns>
    public virtual string GetDescription()
    {
        return "";
    }
}