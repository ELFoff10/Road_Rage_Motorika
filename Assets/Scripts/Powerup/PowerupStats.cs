using UnityEngine;

public class PowerupStats : Powerup
{
    public enum EffectType
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


        if (_effectType == EffectType.AddSpeed)
        {
            car.AddSpeed(_value);
        }

        if (_effectType == EffectType.SlowSpeed)
        {
            car.SlowSpeed(_value);
        }

        //if (m_EffectType == EffectType.AddIndestructible)
        //{
        //    car.AddIndestructible();
        //}
    }
}
