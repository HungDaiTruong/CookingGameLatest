using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Cooking : MonoBehaviour
{
    // This script is responsible for the cooking process of the meat objects

    public GameObject cookedPatty;
    public GameObject cookedBacon;
    public GameObject meatSpawner;

    // Variables for detection of meats on the pans
    bool patty = false;
    bool bacon = false;

    public float pattyCookingTime = 10f;
    public float baconCookingTime = 3f;

    private float _timer = 0f;

    private void Update()
    {
        // Cooks the patty or bacon for their duration, when cooked the raw meats are destroyed and the cooked ones are instantiated
        if (patty)
        {
            _timer += Time.deltaTime;
            if (_timer >= pattyCookingTime)
            {
                _timer = 0f;
                Destroy(GameObject.FindWithTag("RawPattyCooking"));
                Instantiate(cookedPatty, new Vector3((float)meatSpawner.transform.position.x, (float)meatSpawner.transform.position.y, (float)meatSpawner.transform.position.z), Quaternion.identity);
                patty = false;
            }
        }
        else if (bacon)
        {
            _timer += Time.deltaTime;
            if (_timer >= baconCookingTime)
            {
                _timer = 0f;
                Destroy(GameObject.FindWithTag("RawBaconCooking"));
                Instantiate(cookedBacon, new Vector3((float)meatSpawner.transform.position.x, (float)meatSpawner.transform.position.y, (float)meatSpawner.transform.position.z), Quaternion.identity);
                bacon = false;
            }
        }
        else _timer = 0f;
    }

    // This method detects the type of meat according to tag and set it to a cooking state
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RawPatty")
        {
            patty = true;
            other.gameObject.tag = "RawPattyCooking";
        }
        else if (other.gameObject.tag == "RawBacon")
        {
            bacon = true;
            other.gameObject.tag = "RawBaconCooking";
        }
    }

    // This method detects the type of cooking meat according to tag and set it to non cooking state
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "RawPattyCooking")
        {
            patty = false;
            other.gameObject.tag = "RawPatty";
        }
        else if (other.gameObject.tag == "RawBaconCooking")
        {
            bacon = false;
            other.gameObject.tag = "RawBacon";
        }
    }
}
