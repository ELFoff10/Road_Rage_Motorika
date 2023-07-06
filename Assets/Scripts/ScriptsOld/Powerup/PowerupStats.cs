using System;
using UnityEngine;

public class PowerupStats : Powerup
{
    private enum EffectType
    {
        /*AddAmmo, AddEnergy, */AddSpeed, SlowSpeed/*, AddIndestructible*/
    }

    [SerializeField] private EffectType _effectType;

    [SerializeField] private float _value;

    protected override void OnPickedUp(CarController car)
    {
        //if (m_EffectType == EffectType.AddEnergy)
        //{
        //    car.AddEnergy((int)m_Value);
        //}

        //if (m_EffectType == EffectType.AddAmmo)
        //{
        //    car.AddAmmo((int)m_Value);
        //}

        switch (_effectType)
        {
            case EffectType.AddSpeed:
                car.AddSpeed(_value);
                break;
            case EffectType.SlowSpeed:
                car.SlowSpeed(_value);
                break;
        }

        //if (m_EffectType == EffectType.AddIndestructible)
        //{
        //    car.AddIndestructible();
        //}
    }
}
