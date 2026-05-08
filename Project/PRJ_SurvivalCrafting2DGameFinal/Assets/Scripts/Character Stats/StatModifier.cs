namespace kenith.CharacterStats
{
    /// <summary>
    /// El espacio de nombres kenith.CharacterStats contiene la definición de la clase StatModifier y la enumeración StatModType. 
    /// La enumeración StatModType enumera los diferentes tipos de modificadores de estadísticas, como plano, porcentaje aditivo y porcentaje multiplicativo. 
    /// La clase StatModifier representa un modificador de estadística y tiene propiedades para el valor del modificador, el tipo de modificador, el orden del modificador y la fuente del modificador. 
    /// Proporciona diferentes constructores para crear instancias de StatModifier con diferentes combinaciones de parámetros.
    /// </summary>
    public enum StatModType
    {
        Flat = 100,           // Modificador de estadística plano
        PercentAdd = 200,     // Modificador de porcentaje aditivo
        PercentMult = 300,    // Modificador de porcentaje multiplicativo
    }

    public class StatModifier
    {
        public readonly float value;         // Valor del modificador
        public readonly StatModType type;    // Tipo de modificador
        public readonly int order;           // Orden del modificador
        public readonly object source;       // Fuente del modificador

        public StatModifier(float value, StatModType type, int order, object source)
        {
            this.value = value;
            this.type = type;
            this.order = order;
            this.source = source;
        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }   // Constructor abreviado sin orden ni fuente

        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }   // Constructor abreviado sin fuente

        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }   // Constructor abreviado sin orden
    }
}