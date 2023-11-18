using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    private Camera _mainCamera;
    
    public void Init()
    {
        _mainCamera = Camera.main;
        Normalize();
    }

    public void ZoomOut()
    {
        //_mainCamera.DOOrthoSize(5, 1f);
        _mainCamera.transform.DOMove(new Vector3(1, 0, -10), 1f);
    }

    public void Normalize()
    {
        _mainCamera.orthographicSize = 4;
        _mainCamera.transform.position = new Vector3(0, 0, -10);
    }
}
