using UnityEngine;

public class PlayerAniEvents : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void Animationtrigger()
    {
        player.AttackOver();
    }
}
