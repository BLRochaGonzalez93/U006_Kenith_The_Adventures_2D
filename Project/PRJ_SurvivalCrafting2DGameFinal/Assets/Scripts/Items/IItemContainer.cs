/// <summary>
/// La interfaz IItemContainer define las operaciones básicas que deben ser implementadas por un contenedor de elementos. 
/// Esto incluye la capacidad de verificar si es posible agregar un elemento en una cantidad determinada, agregar y eliminar elementos, 
/// vaciar el contenedor y contar la cantidad de elementos con un identificador específico.
/// </summary>
public interface IItemContainer
{
    /// <summary>
    /// Verifica si es posible agregar un elemento al contenedor en una determinada cantidad.
    /// </summary>
    /// <param name="item">El elemento a agregar.</param>
    /// <param name="amount">La cantidad de elementos a agregar (opcional, valor predeterminado: 1).</param>
    /// <returns><c>true</c> si es posible agregar el elemento en la cantidad especificada, <c>false</c> en caso contrario.</returns>
    bool CanAddItem(Item item, int amount = 1);

    /// <summary>
    /// Agrega un elemento al contenedor.
    /// </summary>
    /// <param name="item">El elemento a agregar.</param>
    /// <returns><c>true</c> si se pudo agregar el elemento, <c>false</c> en caso contrario.</returns>
    bool AddItem(Item item);

    /// <summary>
    /// Elimina un elemento del contenedor utilizando su identificador.
    /// </summary>
    /// <param name="itemID">El identificador del elemento a eliminar.</param>
    /// <returns>El elemento eliminado o <c>null</c> si el elemento no fue encontrado.</returns>
    Item RemoveItem(string itemID);

    /// <summary>
    /// Elimina un elemento del contenedor.
    /// </summary>
    /// <param name="item">El elemento a eliminar.</param>
    /// <returns><c>true</c> si se pudo eliminar el elemento, <c>false</c> en caso contrario.</returns>
    bool RemoveItem(Item item);

    /// <summary>
    /// Elimina todos los elementos del contenedor.
    /// </summary>
    void Clear();

    /// <summary>
    /// Obtiene la cantidad de elementos con un identificador específico en el contenedor.
    /// </summary>
    /// <param name="itemID">El identificador del elemento.</param>
    /// <returns>La cantidad de elementos con el identificador especificado.</returns>
    int ItemCount(string itemID);
}