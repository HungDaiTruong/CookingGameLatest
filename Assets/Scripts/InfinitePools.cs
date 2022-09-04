using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InfinitePools : MonoBehaviour
{
    // This script is responsible for all the infinite item pools in the playable area

    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject myPrefab = null;
    public GameObject spawner;

    bool isQuitting = false;

    // This method will allow the OnApplicationQuit to be called before SpawnPrefab
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    // This method will instantiate the Prefab when the game needs it.
    public void SpawnPrefab()
    {
        // Instantiate at position of spawner and with zero rotation.
        if (this.enabled && !isQuitting) 
        {
            Instantiate(myPrefab, new Vector3((float)spawner.transform.position.x, (float)spawner.transform.position.y, (float)spawner.transform.position.z), Quaternion.identity);

            //Destroy(myPrefab, 40.0f);
        }
    }
}
