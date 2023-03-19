using UnityEngine;

public static class BlockChecker
{
    private const float VectorLength = 0.55f;
    
    public static bool CheckIsGrounded(Vector3 position)
    {
        return Physics.Raycast(position, Vector3.down, VectorLength);
    }

    public static bool HasWallInDirection(Vector3 position, Vector3 direction)
    {
        return Physics.Raycast(position, direction, VectorLength);
    }

    public static bool HasBlockInDirection(Vector3 position, Vector3 direction)
    {
        return Physics.Raycast(position + direction, Vector3.down, 1f);
    }

    public static void SnapPositionToInteger(Transform transform)
    {
        var pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.z = Mathf.Round(pos.z);
        
        transform.position = pos;
    }
}