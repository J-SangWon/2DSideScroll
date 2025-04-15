using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    protected virtual void Update()
    {
        if (cooldownTimer > 0) cooldownTimer -= Time.deltaTime;
        
    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer <= 0)
        {
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("Skill is on cooldown");
        return false;
    }

    public virtual void UseSkill()
    {
        // Implement skill usage logic here
        Debug.Log("Using skill");
    }

}
