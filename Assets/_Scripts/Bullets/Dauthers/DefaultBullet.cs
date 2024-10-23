using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : Bullet
{
    protected override void Move()
    {
        transform.position += Vector3.up * (speed * Time.deltaTime);
    }
}
