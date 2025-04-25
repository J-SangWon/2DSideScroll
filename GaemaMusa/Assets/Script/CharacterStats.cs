using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Character Stats")]
    public Stat strength;
    public Stat agility;
    public Stat intelligence;
    public Stat vitaliry;

    [Header("공격 스텟")]
    public Stat damage;
    public Stat CritChance;
    public Stat critPower;

    [Header("방어 스텟")]
    public Stat maxHealth;
    [SerializeField] private int currentHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("마법 스텟")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    public bool isIgnighted;
    public bool isChilled;
    public bool isShocked;

    protected virtual void Start()
    {
        maxHealth.SetBaseValue(100);
        currentHealth = maxHealth.GetValue();

    }
    public virtual void DoDamage(CharacterStats _targetStats)
    {
        if (CanAvoidAttack(_targetStats)) return;

        int totalDamage = damage.GetValue() + strength.GetValue() - _targetStats.armor.GetValue();
        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }
        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        _targetStats.TakeDamage(totalDamage);
    }

    private bool CanAvoidAttack(CharacterStats _targetStats)
    {
        int totalEvasion = _targetStats.evasion.GetValue() + _targetStats.agility.GetValue();

        if (Random.Range(0, 100) < totalEvasion)
        {
            Debug.Log("Evasion!!");
            return true;
        }
        return false;
    }

    public virtual void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
        Debug.Log("Damage : " + _damage);
        if (currentHealth < 0)
            Die();
    }

    public virtual void DoMagicalDamage(CharacterStats _targetStats)
    {
        int _fireDamage = fireDamage.GetValue();
        int _iceDamage = iceDamage.GetValue();
        int _lightningDamage = lightningDamage.GetValue();

        int totalMagicalDamage = _fireDamage + _iceDamage + _lightningDamage + intelligence.GetValue() - _targetStats.magicResistance.GetValue();
        _targetStats.TakeDamage(totalMagicalDamage);

    }
    public void ApplyAilments(bool _isIgnighted, bool _isChilled, bool _isShocked)
    {
        if (_isIgnighted || _isChilled || _isShocked)
        {
            return;
        }

        isIgnighted = _isIgnighted;
        isChilled = _isChilled;
        isShocked = _isShocked;
    }

    protected bool CanCrit()
    {
        int totalCritChance = CritChance.GetValue() + agility.GetValue();
        if (Random.Range(0,100) <= totalCritChance)
        {
            return true;
        }
        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.01);
        float critDamage = _damage * totalCritPower;

        return Mathf.RoundToInt(critDamage);
    }

    protected virtual void Die()
    {

    }
}
