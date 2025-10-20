using UnityEngine;

public class EnemyController : MonoBehaviour
{

    enum State
    {
        Spawning,
        Moving,
        Dying
	}

	public float speed = 2;

    GameObject target;
    State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player");   
    } 

    // Update is called once per frame

    private void FixedUpdate()
    {
        Vector2 direction = target.transform.position - transform.position;

        transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);

        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
		}
		if (direction.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}
	} 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float d = collision.gameObject.GetComponent<Bullet>().damage;

            if (GetComponent<Character>().Hit(d))
            {
                // 살아있음
            }
            else
            {
                // 죽어있음
                Die();
            }
		}
	}

    void Die()
    {
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.4f);
	}

    void AfterDying()
    {
        Destroy(gameObject);
    }
}
