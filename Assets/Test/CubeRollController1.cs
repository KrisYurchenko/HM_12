using System;
using UnityEngine;

public class CubeRollController1 : MonoBehaviour
{
    [SerializeField] private float _cubeSize = 0.5f;
    [SerializeField] private float _rotationSpeed = 2f;
    
    private Rigidbody _rigidbody;
    private Vector3 _currentDirection;
    private bool _isRolling;
    private Vector3 _rollPoint;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(0, -1);
        }    
        else if (Input.GetKey(KeyCode.S))
        {
            Move(0, 1);
        }   
        else if (Input.GetKey(KeyCode.A))
        {
            Move(1, 0);
        }   
        else if (Input.GetKey(KeyCode.D))
        {
            Move(-1, 0);
        } 
        else
        {
            _isRolling = false;
            _currentDirection = Vector3.zero;
        }
    }

    private void Move(int x, int z)
    {
        var direction = new Vector3(x, 0, z);
        _currentDirection = direction;
        if (_isRolling)
        {
            var axis = new Vector3(z, 0, -x);
            transform.RotateAround(_rollPoint, axis, _rotationSpeed * Time.deltaTime);

            if (HasMadeFullRotation())
            {
                SnapToIntegerPosition();
                _isRolling = false;
            }
        }
        if (!_isRolling)
        {
            _rollPoint = GetRollPoint(direction);
            _isRolling = true;
        }
    }

    private void SnapToIntegerPosition()
    {
        transform.rotation = Quaternion.identity;
        var p = transform.position;
        transform.position = new Vector3((int)p.x, (int)p.y, (int)p.z);
    }

    private bool HasMadeFullRotation()
    {
        var a = transform.rotation.eulerAngles;
        return a.x >= 90 || a.x <= -90 || a.z >= 90 || a.z <= -90;
    }

    private Vector3 GetRollPoint(Vector3 direction)
    {
        var isNextToWall = CheckWallInDirection(direction);
        
        var isOnGround = CheckIsGrounded();
        if (isOnGround)
        {
            return GetBottomRollPointInDirection(direction);
        }
        return transform.position;
    }

    private Vector3 GetBottomRollPointInDirection(Vector3 direction)
    {
        var x = SnapTo01(direction.x);
        var z = SnapTo01(direction.z);
        return transform.position + new Vector3(x * _cubeSize, -_cubeSize, z * _cubeSize);
    }

    private float SnapTo01(float value)
    {
        return value == 0f ? value : value > 0f ? 1f : -1f;
    }

    private Vector3[] GetAllRollPoints()
    {
        var pos = transform.position;
        var s = _cubeSize / 2;
        return new Vector3[]
        {
            pos + new Vector3(s, s, 0),
            pos + new Vector3(-s, s, 0),
            pos + new Vector3(s, -s, 0),
            pos + new Vector3(-s, -s, 0),
            pos + new Vector3(0, s, s),
            pos + new Vector3(0, s, -s),
            pos + new Vector3(0, -s, s),
            pos + new Vector3(0, -s, -s),
            // pos + new Vector3(s, 0, s),
            // pos + new Vector3(s, 0, -s),
            // pos + new Vector3(-s, 0, s),
            // pos + new Vector3(-s, 0, -s),
        };
    }

    private bool CheckWallInDirection(Vector3 direction)
    {
        return Physics.Raycast(transform.position, direction, _cubeSize);
    }

    private bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _currentDirection);
        Gizmos.DrawSphere(_rollPoint, 0.2f);

        // var rollPoints = GetAllRollPoints();
        //
        // foreach (var rollPoint in rollPoints)
        // {
        //     Gizmos.DrawSphere(rollPoint, 0.2f);
        // }
    }
}
