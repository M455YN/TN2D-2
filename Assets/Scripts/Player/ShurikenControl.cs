using UnityEngine;
using UnityEngine.UI;
public class ShurikenControl : MonoBehaviour
{
    public int shurikenCount = 10;
    public PlayerAttack PlayerAttackScript;
    public GameObject[] shurikeny;

    void Update()
    {
        shurikenCount = PlayerAttackScript.GetComponent<PlayerAttack>().shurikenCount;
        if (shurikenCount >= 10)
        {
            shurikenCount = 10;
            PlayerAttackScript.GetComponent<PlayerAttack>().shurikenCount = 10;
        }

        for (int i = 0; i < shurikeny.Length; i++)
        {
            if (i < shurikenCount)
            {
                shurikeny[i].SetActive(true);
            }
            else
            {
                shurikeny[i].SetActive(false);
            }
        }
    }
}
