using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectsManager : MonoBehaviour
{
    [SerializeField] private Transform _objectsContainer;
    
    public void Run(float moveAmount)
    {
        _objectsContainer.Translate(Vector3.left * moveAmount);
    }
    
}
