using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    PlayerPlacementManager myPlacementManager;

    private KeyCode toggleTowerPlacingKeyCode = KeyCode.Alpha0;
    private KeyCode cycleTowerKeyCode = KeyCode.Tab;
    private KeyCode basicTowerKeyCode = KeyCode.Q;
    private KeyCode wallTowerKeyCode = KeyCode.E;

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
        myPlacementManager = GetComponent<PlayerPlacementManager>();
        LoadControls();
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
    private void LoadControls()
    {
        toggleTowerPlacingKeyCode = SaveDataScript.GetKeyCode("toggleTowerPlacingKeyCode");
        cycleTowerKeyCode = SaveDataScript.GetKeyCode("cycleTowerKeyCode");
        basicTowerKeyCode = SaveDataScript.GetKeyCode("basicTowerKeyCode");
        wallTowerKeyCode = SaveDataScript.GetKeyCode("wallTowerKeyCode");
        moveLeftKeyCode = SaveDataScript.GetKeyCode("moveLeftKeyCode");
        moveRightKeyCode = SaveDataScript.GetKeyCode("moveRightKeyCode");
        moveUpKeyCode = SaveDataScript.GetKeyCode("moveUpKeyCode");
        moveDownKeyCode = SaveDataScript.GetKeyCode("moveDownKeyCode");
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
            myPlacementManager.PlaceObject();
        }
        if (Input.GetMouseButtonDown(1))
        {
            //myPlacementManager.PlaceObject();
            Vector2 mousePos = (Vector2)GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

            WaveController.instance.CreateEnemy((int) mousePos.x, (int) mousePos.y);
        }

        if (Input.GetKeyDown(toggleTowerPlacingKeyCode))
        {
            myPlacementManager.TogglePlacingTowers();
        }
        if (Input.GetKeyDown(basicTowerKeyCode))
        {
            myPlacementManager.SelectTower("basicTower");
        }
        if (Input.GetKeyDown(wallTowerKeyCode))
        {
            myPlacementManager.SelectTower("wallTower");
        }
        if (Input.GetKeyDown(cycleTowerKeyCode))
        {
            myPlacementManager.CycleTowers();
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
}
