using TMPro;
using UnityEngine;

public class BlackHole_HotKey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEmemy;
    private BlackHole_Skill_Controller blackHole;

    public void SetupHotKey(KeyCode _myHotKey, Transform _myEnemy, BlackHole_Skill_Controller _myBleackHole)
    {
        myText = GetComponentInChildren<TextMeshProUGUI>();
        sr = GetComponentInChildren<SpriteRenderer>();
        myHotKey = _myHotKey;
        myText.text = myHotKey.ToString();
        myEmemy = _myEnemy;
        blackHole = _myBleackHole;
    }
    private void Update()
    {
        if(Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEmemy);
            Debug.Log("Hotkey Pressed: " + myHotKey);
            myText.color = Color.clear;
            sr.color = Color.clear;
        }
    }


}
