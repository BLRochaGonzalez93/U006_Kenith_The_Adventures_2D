using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// El espacio de nombres kenith.CharacterStats contiene la definición de la clase CharacterStat. Esta clase representa una estadística de personaje y tiene propiedades como:
/// baseValue: Valor base de la estadística.
/// isDirty: Indica si la estadística está sucia y necesita actualizarse.
/// lastBaseValue: Valor base anterior de la estadística.
/// _value: Valor actual de la estadística.
/// value: Propiedad que devuelve el valor actual de la estadística, calculándolo si está sucio o si el valor base ha cambiado.
/// statModifiers: Lista de modificadores de estadística.
/// StatModifiers: Lista de modificadores de estadística de solo lectura.
/// AddModifier(mod): Agrega un modificador de estadística a la lista y marca la estadística como sucia.
/// RemoveModifier(mod): Elimina un modificador de estadística de la lista y marca la estadística como sucia.
/// RemoveAllModifiersFromSource(source): Elimina todos los modificadores de estadística asociados a una fuente específica y marca la estadística como sucia.
/// CompareModifierOrder(a, b): Método de comparación utilizado para ordenar los modificadores de estadística por orden.
/// CalculateFinalValue(): Calcula el valor final de la estadística teniendo en cuenta los modificadores aplicados, como los modificadores planos, los aditivos de porcentaje y los multiplicativos de porcentaje.
/// La clase CharacterStat proporciona métodos para administrar modificadores de estadísticas, calcular el valor final de la estadística y mantener un registro de los modificadores aplicados.
/// </summary>
namespace kenith.CharacterStats
{
    [Serializable]
    public class CharacterStat
    {
        public float baseValue;   // Valor base de la estadística

        protected bool isDirty = true;    // Indica si la estadística está sucia y necesita actualizarse
        protected float lastBaseValue;    // Valor base anterior

        protected float _value;   // Valor actual de la estadística
        public virtual float Value
        {
            get
            {
                if (isDirty || lastBaseValue != baseValue)
                {
                    lastBaseValue = baseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        protected readonly List<StatModifier> statModifiers;   // Lista de modificadores de estadística
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;   // Lista de modificadores de estadística de solo lectura

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
        }

        public CharacterStat(float baseValue) : this()
        {
            this.baseValue = baseValue;
        }

        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        public virtual bool RemoveModifier(StatModifier mod)
        {
            if (statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }
            return didRemove;
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.order < b.order)
                return -1;
            else if (a.order > b.order)
                return 1;
            return 0; //if (a.order == b.order)
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.type == StatModType.Flat)   // Modificador de estadística plano
                {
                    finalValue += mod.value;
                }
                else if (mod.type == StatModType.PercentAdd)   // Modificador de porcentaje aditivo
                {
                    sumPercentAdd += mod.value;

                    if (i + 1 >= statModifiers.Count || statModifiers[i + 1].type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.type == StatModType.PercentMult)   // Modificador de porcentaje multiplicativo
                {
                    finalValue *= 1 + mod.value;
                }
            }

            return (float)Math.Round(finalValue, 4);   // Redondear el valor final de la estadística
        }
    }
}