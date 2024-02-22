using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private KeyCode moveLeftKeyCode = KeyCode.A;
    private KeyCode moveRightKeyCode = KeyCode.D;
    private KeyCode moveUpKeyCode = KeyCode.W;
    private KeyCode moveDownKeyCode = KeyCode.S;
    private Vector2 inputAxis;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxCameraSize;
    [SerializeField] private float minCameraSize;
    [SerializeField] private Vector2 cameraMaxBounds;

    [SerializeField] private GameObject wallPrefab;
    // Start is called before the first frame update
    void Start()
    {
        inputAxis = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();

    }
    void FixedUpdate()
    {
        Move();
    }

    private void CheckInputs()
    {
        inputAxis = new Vector2(0, 0);
        if (Input.GetKey(moveLeftKeyCode))
        {
            inputAxis += new Vector2(-1,0);
        }
        if (Input.GetKey(moveRightKeyCode))
        {
            inputAxis += new Vector2(1,0);
        }
        if (Input.GetKey(moveUpKeyCode))
        {
            inputAxis += new Vector2(0,1);
        }
        if (Input.GetKey(moveDownKeyCode))
        {
            inputAxis += new Vector2(0,-1);
        }
        if (inputAxis.x != 0)
        {
            inputAxis = new Vector2(Mathf.Sign(inputAxis.x), inputAxis.y);
        }
        if (inputAxis.y != 0)
        {
            inputAxis = new Vector2(inputAxis.x, Mathf.Sign(inputAxis.y));
        }
        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 10;
        if (Camera.main.orthographicSize < minCameraSize)
        {
            Camera.main.orthographicSize = minCameraSize;
        }
        else if (Camera.main.orthographicSize > maxCameraSize)
        {
            Camera.main.orthographicSize = maxCameraSize;
        }

        if(Input.GetMouseButtonDown(0))
        {
            MousePressed();
        }
    }
    private void Move()
    {
        transform.position += (Vector3)inputAxis * moveSpeed;
        if(Mathf.Abs(transform.position.x) > cameraMaxBounds.x)
        {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * cameraMaxBounds.x,transform.position.y,transform.position.z);
        }
        if (Mathf.Abs(transform.position.y) > cameraMaxBounds.y)
        {
            transform.position = new Vector3(transform.position.x,Mathf.Sign(transform.position.y) * cameraMaxBounds.y, transform.position.z);
        }
    }
    private void MousePressed()
    {
        PlaceObject();
    }
    private void PlaceObject()
    {

    }
    private void PlaceWall()
    {
        GameObject wall = Instantiate(wallPrefab);
        
    }
}
