using UnityEngine;

public class Character : MonoBehaviour
{

    public float MaxHP = 3;
    float HP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HP = MaxHP;
    }

    public void Initialize()
    {
        HP = MaxHP;
    }

    /*
     * 살아있으면 True를 리턴한다.
     */
    public bool Hit(float damage)
    {
        HP -= damage;

        if (HP <= 0) 
        {
            HP = 0;
        }

        return HP > 0;
    }
}
