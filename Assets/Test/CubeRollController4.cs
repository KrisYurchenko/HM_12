using System;
using System.Collections;
using UnityEngine;
 
public class CubeRollController4 : MonoBehaviour {
    [SerializeField] private float _rollSpeed = 5;
    private bool _isMoving;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (_isMoving) return;
 
        if (Input.GetKey(KeyCode.A)) Assemble(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) Assemble(Vector3.right);
        else if (Input.GetKey(KeyCode.W)) Assemble(Vector3.forward);
        else if (Input.GetKey(KeyCode.S)) Assemble(Vector3.back);
    }
 
    private void Assemble(Vector3 dir)
    {
        Vector3 verticalDir = default;
        if (HasWallInDirection(dir))
        {
            verticalDir = Vector3.up;
        }
        else
        {
            verticalDir = Vector3.down;
        }
        
        var anchor = transform.position + (verticalDir + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(Roll(anchor, axis));
    }

    private bool HasWallInDirection(Vector3 dir)
    {
        return Physics.Raycast(transform.position, dir, 0.51f);
    }

    private IEnumerator Roll(Vector3 anchor, Vector3 axis) {
        _isMoving = true;
        _rigidbody.isKinematic = true;
        
        for (var i = 0; i < 90 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
        _rigidbody.isKinematic = false;
        _isMoving = false;
    }
}