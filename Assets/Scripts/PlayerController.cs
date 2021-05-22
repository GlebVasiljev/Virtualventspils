using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float degreesPerSec = 360f;
    public Rigidbody2D rb;
    public Camera cam;
    public Sprite[] PlayerSprite;
    bool isMoving;
    Vector2 Target;
    Vector2 movement;
    Vector2 mousePos;

    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //Rotation
        float rotAmount = degreesPerSec * Time.deltaTime;
        float curRot = transform.localRotation.eulerAngles.z;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, curRot + rotAmount));
    }

    private void FixedUpdate()
    {
        //if (!isMoving) 
        //   return;
        // Vector2 NewPos = Vector2.MoveTowards(transform.position, Target, moveSpeed * Time.fixedDeltaTime);
        // rb.MovePosition(NewPos);
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Level1");
    }
    public void MoveToPoint(Vector2 Target)
    {
        Debug.Log("Moving");
        this.Target = Target;
        isMoving = true;
    } 

    public void OnClick(Transform Point)
    {
        MoveToPoint(Point.position);
    }
}
