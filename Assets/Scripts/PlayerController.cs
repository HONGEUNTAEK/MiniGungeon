    using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
	public GameObject bulletPrefab;
	Vector3 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

	// Update is called once per frame
	void Update()
	{

		move = Vector3.zero;

		if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
		{
			move += new Vector3(-1, 0, 0);
		}

		if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
		{
			move += new Vector3(1, 0, 0);
		}

		if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
		{
			move += new Vector3(0, 1, 0);
		}

		if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
		{
			move += new Vector3(0, -1, 0);
		}

		move = move.normalized;
		if (move.x < 0)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}

		if (move.x > 0)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}

		if (move.magnitude > 0)
		{
			GetComponent<Animator>().SetTrigger("Move");
		}
		else {
			GetComponent<Animator>().SetTrigger("Stop");
		}

		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			Shoot();
		}

		}

	void Shoot()
	{
		Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();

		Vector3 worldPosition = Camera.main.ScreenToWorldPoint(
			new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane)
		);
		worldPosition.z = 0;
		worldPosition -= (transform.position + new Vector3(0, -0.5f, 0));

		Debug.Log(worldPosition);

		GameObject newBullet = GetComponent<ObjectPool>().Get();
		if (newBullet != null)
		{
			newBullet.transform.position = transform.position + new Vector3(0, -0, -0.5f);
			newBullet.GetComponent<Bullet>().Direction = worldPosition;
		}
		
	}



	private void FixedUpdate()
    {
		transform.Translate(move * speed * Time.fixedDeltaTime);
    }
}
