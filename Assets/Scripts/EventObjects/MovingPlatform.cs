using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public void Move()
    {
        gameObject.transform.position = gameObject.transform.position + new Vector3(0, 0, Time.deltaTime * -0.5f);
    }
}
