using UnityEngine;

public class FSF_Event : MonoBehaviour
{
    private DNF_FSF FSF;
    void Start()
    {
        FSF = GetComponentInParent<DNF_FSF>();
    }

    public void Animationtrigger()
    {
        FSF.AttackOver();
    }
}
