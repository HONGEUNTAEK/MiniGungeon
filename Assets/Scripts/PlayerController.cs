    using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
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

		}


    private void FixedUpdate()
    {
		transform.Translate(move * speed * Time.fixedDeltaTime);
    }
}
