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

    public Material flashMaterial;
    public Material deafaultMaterial;

    public AudioClip hitSound;
    public AudioClip deadSound;

    GameObject target;
    State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Spawn(GameObject target)
    {
        this.target = target;
        state = State.Spawning;
        GetComponent<Character>().Initialize();
        GetComponent<Animator>().SetTrigger("Spawn");
        Invoke("StartMoving", 1);
        GetComponent<Collider2D>().enabled = false;
    }

    void StartMoving()
    {
        GetComponent<Collider2D>().enabled = true;
        state = State.Moving;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        if (state == State.Moving)
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
	} 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float d = collision.gameObject.GetComponent<Bullet>().damage;

            if (GetComponent<Character>().Hit(d))
            {
                // 살아있음
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                Flash();
            }
            else
            {
                // 죽어있음
                GetComponent<AudioSource>().PlayOneShot(deadSound);
                Die();
            }
		}
	}

    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.3f);
    }

    void AfterFlash()
    {
		GetComponent<SpriteRenderer>().material = deafaultMaterial;
	}

    void Die()
    {
        state = State.Dying;
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.4f);
	}

    void AfterDying()
    {
	    gameObject.SetActive(false); 
	}
}
