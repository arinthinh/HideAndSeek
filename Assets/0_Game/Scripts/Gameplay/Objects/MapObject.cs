using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public void SetPosition(float position)
    {
        transform.SetLocalPositionX(position);
    }
}