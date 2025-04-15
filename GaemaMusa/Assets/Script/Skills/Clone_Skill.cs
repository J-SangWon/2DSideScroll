using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone Skill")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration = 2f;
    [Space]
    [SerializeField] private bool canAttack;
    public void CreateClone(Transform transform)
    {
        if (transform == null)
        {
            Debug.LogError("CreateClone()에 null Transform이 전달되었습니다.");
            return;
        }

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(transform, cloneDuration, canAttack);
    }
}
