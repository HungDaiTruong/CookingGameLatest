using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetAnimation : MonoBehaviour
{
    // This script rotates the client assets

    public int rotationSpeed = 45;

    // Update is called once per frame
    void Update()
    {
        // Rotates 45 degrees per second around Y axis
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
