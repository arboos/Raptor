using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoving : MonoBehaviour
{
    public float speedY;
    
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += new Vector3(0f, speedY * Time.deltaTime, 0f);
    }
}
