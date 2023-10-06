using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollBackground : MonoBehaviour
{
    [SerializeField] private float _x,_y;
    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(_x, _y) * Time.deltaTime,
            _rawImage.uvRect.size);
    }
}
