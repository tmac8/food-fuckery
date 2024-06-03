using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    public LayerMask floorMask;
    public Transform floorChecker;
    public Transform playerCamera;
    public Rigidbody playerBody;
    [Space]
    public float speed;
    public float sensitivity;
    public float jumpforce;

    public Transform player;
    public CapsuleCollider playerCol;
    public float normalHeight, crouchHeight;
    public Vector3 offset;


    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(floorChecker.position, 0.1f, floorMask))
            {
                playerBody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            playerCol.height = crouchHeight;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            playerCol.height = normalHeight;
            player.position = player.position + offset;
        }
    }

    private void MoveCamera()
    {
        xRot -= PlayerMouseInput.y * sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}
