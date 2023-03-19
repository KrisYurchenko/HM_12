using System;
using UnityEngine;

public class CubeRollController2 : MonoBehaviour
{
    [SerializeField] private float _cubeSize = 0.5f;
    [SerializeField] private float _rotationSpeed = 2f;
    
    private Rigidbody _rigidbody;
    private Vector3 _currentDirection;
    private bool _isRolling;
    [SerializeField] private double _snapTolerance = 0.01f;
    private Vector3 _currentPivot;
    private HingeJoint _hinge;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hinge = GetComponent<HingeJoint>();
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
        var axis = new Vector3(z, 0, x);
        var force = axis * _rotationSpeed;

        _rigidbody.AddTorque(force);

        if (PositionIsNearToInteger())
        {
            var integerPosition = GetIntegerPosition();
            var pivot = GetNearestPivotTo(integerPosition, direction);
            if (_currentPivot != pivot)
            {
                _currentPivot = pivot;
                transform.position = integerPosition;
                transform.rotation = Quaternion.identity;
                _hinge.anchor = pivot;

            }
        }
    }

    private Vector3 GetNearestPivotTo(Vector3 integerPosition, Vector3 direction)
    {
        var isNextToWall = CheckWallInDirection(direction);
        
        var isOnGround = CheckIsGrounded();
        if (isOnGround)
        {
            return GetBottomPivotInDirection(integerPosition, direction);
        }
        return transform.position;
    }

    private Vector3 GetBottomPivotInDirection(Vector3 pos, Vector3 direction)
    {
        return new Vector3(direction.x * _cubeSize, -_cubeSize, direction.z * _cubeSize);
    }

    private Vector3 GetIntegerPosition()
    {
        var p = transform.position;
        return new Vector3(Mathf.Round(p.x), Mathf.Round(p.y), Mathf.Round(p.z));
    }

    private bool PositionIsNearToInteger()
    {
        var p = transform.position;
        var xFrac = Math.Abs(p.x - Mathf.Round(p.x));
        var yFrac = Math.Abs(p.y - Mathf.Round(p.y));
        var zFrac = Math.Abs(p.z - Mathf.Round(p.z));

        return xFrac + yFrac + zFrac < _snapTolerance;
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
        Gizmos.DrawSphere(_currentPivot, 0.2f);

        // var rollPoints = GetAllRollPoints();
        //
        // foreach (var rollPoint in rollPoints)
        // {
        //     Gizmos.DrawSphere(rollPoint, 0.2f);
        // }
    }
}
