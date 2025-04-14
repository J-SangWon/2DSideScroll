using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("FX")]
    [SerializeField] private float hitTime = 0.3f;
    [SerializeField] private Material hitMat;
    private Material defaultMat;
    

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        defaultMat = sr.material;


    }

    private IEnumerator HitEffect()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(hitTime);
        sr.material = defaultMat;

    }

    private void RedColorBlink()
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }

    private void CancelRedBlink()
    {
        CancelInvoke("RedColorBlink");
        sr.color = Color.white;
    }
}
