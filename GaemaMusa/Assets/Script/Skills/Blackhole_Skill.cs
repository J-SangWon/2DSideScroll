using UnityEngine;

public class Blackhole_Skill : Skill
{

    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneCooldown;
    [SerializeField] private float blackholeDuration;
    BlackHole_Skill_Controller currentBalckhole;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
        
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBlackhole = Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);
        BlackHole_Skill_Controller newBlackholeScript = newBlackhole.GetComponent<BlackHole_Skill_Controller>();
        newBlackholeScript.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, amountOfAttacks, cloneCooldown, blackholeDuration);

    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool BlackHoleFinished()
    {
        if (!currentBalckhole)
            return false;

        if (currentBalckhole.playerCanExitState)
        {
            currentBalckhole = null;
            return true;
        }
        return false;
    }

    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }
}
