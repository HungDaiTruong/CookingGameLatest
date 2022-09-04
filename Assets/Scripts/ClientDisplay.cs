using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientDisplay : MonoBehaviour
{
    // This script instantiate the client orders using assets 

    public GameObject cheeseBurger, deluxeBurger, doubleCheeseBurger, doubleDeluxeBurger;
    public GameObject ketchupBottle, mustardBottle, cookedBacon, emmentalCheese;
    public GameObject leftClientPosition, middleClientPosition, rightClientPosition;
    public GameObject leftExtraPosition, middleExtraPosition, rightExtraPosition;

    void Start()
    {
        Instantiate(cheeseBurger, new Vector3((float)leftClientPosition.transform.position.x, (float)leftClientPosition.transform.position.y, (float)leftClientPosition.transform.position.z), Quaternion.identity);
        Instantiate(deluxeBurger, new Vector3((float)middleClientPosition.transform.position.x, (float)middleClientPosition.transform.position.y, (float)middleClientPosition.transform.position.z), Quaternion.identity);
        Instantiate(doubleDeluxeBurger, new Vector3((float)rightClientPosition.transform.position.x, (float)rightClientPosition.transform.position.y, (float)rightClientPosition.transform.position.z), Quaternion.identity);

        Instantiate(ketchupBottle, new Vector3((float)leftExtraPosition.transform.position.x, (float)leftExtraPosition.transform.position.y, (float)leftExtraPosition.transform.position.z), Quaternion.identity);
        Instantiate(mustardBottle, new Vector3((float)middleExtraPosition.transform.position.x, (float)middleExtraPosition.transform.position.y, (float)middleExtraPosition.transform.position.z), Quaternion.identity);
        Instantiate(cookedBacon, new Vector3((float)rightExtraPosition.transform.position.x, (float)rightExtraPosition.transform.position.y, (float)rightExtraPosition.transform.position.z), Quaternion.identity);
    }
}
