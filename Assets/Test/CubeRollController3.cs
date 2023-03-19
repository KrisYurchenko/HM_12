using System;
using UnityEngine;

public class CubeRollController3 : MonoBehaviour
{
    [SerializeField] private float _cubeSize = 0.5f;
    [SerializeField] private float _rotationSpeed = 2f;
    
    private Rigidbody _rigidbody;
    private bool _isRolling;
    [SerializeField] private double _snapTolerance = 0.01f;
    private Vector3 _currentPivot;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(0, 1);
        }   
        else if (Input.GetKey(KeyCode.S))
        {
            Move(0, -1);
        }  
        else if (Input.GetKey(KeyCode.A))
        {
            Move(-1, 0);
        }    
        else if (Input.GetKey(KeyCode.D))
        {
            Move(1, 0);
        }  
    }

    private void Move(int x, int z)
    {
        var axis = new Vector3(z, 0, -x);
        var force = axis * _rotationSpeed;
        _rigidbody.AddTorque(force);
    }
}
