using System.Collections;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone Skill")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration = 2f;
    [Space]
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [SerializeField] private bool canDuplicate;
    [SerializeField] private float chanceToDuplicate = 30f;
    [SerializeField] private bool crystalInteadOfClone;
    public void CreateClone(Transform transform, Vector3 _offset)
    {
        if (transform == null)
        {
            Debug.LogError("CreateClone()에 null Transform이 전달되었습니다.");
            return;
        }

        if (crystalInteadOfClone)
        {
            SkillManager.instance.crystal.CreateCrystal();
            SkillManager.instance.crystal.CurrentCrystalChooseRandomTarget();
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().
            SetupClone(transform, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform), canDuplicate, chanceToDuplicate);
    }
    public void CreateCloneOnDashStart()
    {
        if (createCloneOnDashStart)
            CreateClone(player.transform, Vector3.zero);
    }


    public void CreateCloneOnDashOver()
    {
        if (createCloneOnDashOver)
            CreateClone(player.transform, Vector3.zero);
    }
    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
        StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(2 * player.facingDir, 0)));
    }
    private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform, _offset);
    }


}
