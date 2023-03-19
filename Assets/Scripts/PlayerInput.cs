using UnityEngine;

[RequireComponent(typeof(CubeController))]
public class PlayerInput : MonoBehaviour
{
    private CubeController _cubeController;

    private void Start()
    {
        _cubeController = GetComponent<CubeController>();
    }

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _cubeController.Move(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _cubeController.Move(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            _cubeController.Move(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _cubeController.Move(Vector3.back);
        }
    }
}