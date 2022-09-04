using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BurgerOrders : MonoBehaviour
{
    // This script is responsible for writing, erasing and comparing the stacking composition of burgers on each station via tag, this script also is responsible for the detection of completed orders

    // Premade orders, disable or enable as fit

    protected static string[] leftClientOrder = new string[12] { "KB", "PL", "BB", "CP", "AC", "TB", "OO", "OO", "OO", "OO", "OO", "OO" }; // Cheese burger and ketchup
    protected static string[] middleClientOrder = new string[12] { "MB", "PL", "BB", "LS", "CP", "AC", "TS", "TB", "OO", "OO", "OO", "OO" }; // Deluxe burger and mustard
    protected static string[] rightClientOrder = new string[12] { "CB", "PL", "BB", "LS", "CP", "AC", "TS", "BB", "LS", "CP", "AC", "TB" }; // Double deluxe burger and cooked bacon

/*    protected static string[] leftClientOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };
    protected static string[] middleClientOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };
    protected static string[] rightClientOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };*/

    protected static string[] leftBurgerOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };
    protected static string[] middleBurgerOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };
    protected static string[] rightBurgerOrder = new string[12] { "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO", "OO" };

    // Variable to store the collider objects for future uses
    private Collider storedGameObject = null;

    // Variable to detect if socket should be used, 0 = not elligible for Order Writing nor Erasing, 1 = elligible for Order Writing, 2 = Elligible for Order Erasing, 3 = TransitionState
    int free = 0;

    // Variables to detect which station the player is currently interacting with
    public static bool leftStation = false;
    public static bool middleStation = false;
    public static bool rightStation = false;

    // Variables to detect if bags are ready on each stations
    public static bool leftBagReady = false;
    public static bool middleBagReady = false;
    public static bool rightBagReady = false;

    public static bool leftOrderCorrect = false;
    public static bool middleOrderCorrect = false;
    public static bool rightOrderCorrect = false;

    // Variables to keep track of the stacking for the arrays
    public static int lStack = 1;
    public static int mStack = 1;
    public static int rStack = 1;

    private void OnTriggerEnter(Collider other)
    {

        // Change station when hand is in tagged collider
        if(gameObject.CompareTag("Left") && other.CompareTag("Hand"))
        {
            OnLeftStation();
        }
        else if(gameObject.CompareTag("Middle") && other.CompareTag("Hand"))
        {
            OnMiddleStation();
        }
        else if(gameObject.CompareTag("Right") && other.CompareTag("Hand"))
        {
            OnRightStation();
        }

        // Store collider only if it has a matching tag
        if (other.CompareTag("Plate"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Bottom"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Lettuce"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("CookedPatty"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("American"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Tomato"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Top"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Ketchup"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Mustard"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("Emmental"))
        {
                storedGameObject = other;
        }
        else if (other.CompareTag("CookedBacon"))
        {
                storedGameObject = other;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // If collider is stored, allows the stacking function to only interact with a single item at a time
        if (storedGameObject == other)
        {
            // Check station
            if (leftStation && lStack <= 11)
            {
                // If free makes objects elligible for socketing, only allows correctly tagged object that are grabbed/socketed to be written in the arrays, then sets free to a transition state
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "PL";
                    lStack++;
                    Debug.Log("Plate");
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "BB";
                    lStack++;
                    Debug.Log("BottomBun");
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "LS";
                    lStack++;
                    Debug.Log("Lettuce");
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "CP";
                    lStack++;
                    Debug.Log("CookedPatty");
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "AC";
                    lStack++;
                    Debug.Log("AmericanCheese");
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "TS";
                    lStack++;
                    Debug.Log("Tomato");
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[lStack] = "TB";
                    lStack++;
                    Debug.Log("TopBun");
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[0] = "KB";
                    Debug.Log("Ketchup");
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[0] = "MB";
                    Debug.Log("Mustard");
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[0] = "EC";
                    Debug.Log("EmmentalCheese");
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    leftBurgerOrder[0] = "CB";
                    Debug.Log("CookedBacon");
                }
            }
            
            // Check station
            else if (middleStation && mStack <= 11)
            {
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "PL";
                    mStack++;
                    Debug.Log("Plate");
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "BB";
                    mStack++;
                    Debug.Log("BottomBun");
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "LS";
                    mStack++;
                    Debug.Log("Lettuce");
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "CP";
                    mStack++;
                    Debug.Log("CookedPatty");
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "AC";
                    mStack++;
                    Debug.Log("AmericanCheese");
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "TS";
                    mStack++;
                    Debug.Log("Tomato");
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[mStack] = "TB";
                    mStack++;
                    Debug.Log("TopBun");
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[0] = "KB";
                    Debug.Log("Ketchup");
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[0] = "MB";
                    Debug.Log("Mustard");
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[0] = "EC";
                    Debug.Log("EmmentalCheese");
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    middleBurgerOrder[0] = "CB";
                    Debug.Log("CookedBacon");
                }
            }

            // Check station
            else if (rightStation && rStack <= 11)
            {
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "PL";
                    rStack++;
                    Debug.Log("Plate");
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "BB";
                    rStack++;
                    Debug.Log("BottomBun");
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "LS";
                    rStack++;
                    Debug.Log("Lettuce");
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "CP";
                    rStack++;
                    Debug.Log("CookedPatty");
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "AC";
                    rStack++;
                    Debug.Log("AmericanCheese");
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "TS";
                    rStack++;
                    Debug.Log("Tomato");
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[rStack] = "TB";
                    rStack++;
                    Debug.Log("TopBun");
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[0] = "KB";
                    Debug.Log("Ketchup");
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[0] = "MB";
                    Debug.Log("Mustard");
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[0] = "EC";
                    Debug.Log("EmmentalCheese");
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 1)
                {
                    free = 3;
                    rightBurgerOrder[0] = "CB";
                    Debug.Log("CookedBacon");
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Check if the item is stored, only allow one object to interact
        if (storedGameObject == other)
        {
            // Check station
            if (leftStation && lStack <= 11)
            {
                // Similarly to the stacking of objects for array writing, this method is responsible for destacking and erasing array value when a correctly tagged object is grabbed away
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusPlate");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusBottomBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusLettuce");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusCookedPatty");
                    storedGameObject = null;
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusAmericanCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusTomato");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[lStack] = "OO";
                    lStack--;
                    Debug.Log("MinusTopBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[0] = "OO";
                    Debug.Log("MinusKetchup");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[0] = "OO";
                    Debug.Log("MinusMustard");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[0] = "OO";
                    Debug.Log("MinusEmmentalCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    leftBurgerOrder[0] = "OO";
                    Debug.Log("MinusCookedBacon");
                    storedGameObject = null;
                }
            }

            // Check station
            else if (middleStation && mStack <= 11)
            {
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusPlate");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusBottomBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusLettuce");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusCookedPatty");
                    storedGameObject = null;
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusAmericanCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusTomato");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[mStack] = "OO";
                    mStack--;
                    Debug.Log("MinusTopBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[0] = "OO";
                    Debug.Log("MinusKetchup");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[0] = "OO";
                    Debug.Log("MinusMustard");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[0] = "OO";
                    Debug.Log("MinusEmmentalCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    middleBurgerOrder[0] = "OO";
                    Debug.Log("MinusCookedBacon");
                    storedGameObject = null;
                }
            }

            // Check station
            if (rightStation && rStack <= 11)
            {
                if (other.CompareTag("Plate") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusPlate");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Bottom") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusBottomBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Lettuce") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusLettuce");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedPatty") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusCookedPatty");
                    storedGameObject = null;
                }
                else if (other.CompareTag("American") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusAmericanCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Tomato") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusTomato");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Top") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[rStack] = "OO";
                    rStack--;
                    Debug.Log("MinusTopBun");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Ketchup") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[0] = "OO";
                    Debug.Log("MinusKetchup");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Mustard") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[0] = "OO";
                    Debug.Log("MinusMustard");
                    storedGameObject = null;
                }
                else if (other.CompareTag("Emmental") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[0] = "OO";
                    Debug.Log("MinusEmmentalCheese");
                    storedGameObject = null;
                }
                else if (other.CompareTag("CookedBacon") && other.GetComponent<Rigidbody>().isKinematic == true && free == 2)
                {
                    free = 0;
                    rightBurgerOrder[0] = "OO";
                    Debug.Log("MinusCookedBacon");
                    storedGameObject = null;
                }
            }
        }
    }

    // Method to compare orders for each stations with the arrays when the bag is socketed
    public int CompareOrders()
    {
        if (leftBagReady && leftStation)
        {
            if (leftClientOrder.Length != leftBurgerOrder.Length) return 0;

            for (int i = 0; i < leftClientOrder.Length; i++)
            {
                if (!leftClientOrder[i].Equals(leftBurgerOrder[i])) return 0;
            }

            return 1;
        }

        if (middleBagReady && middleStation)
        {
            if (middleClientOrder.Length != middleBurgerOrder.Length) return 0;

            for (int i = 0; i < middleClientOrder.Length; i++)
            {
                if (!middleClientOrder[i].Equals(middleBurgerOrder[i])) return 0;
            }

            return 2;
        }

        if (rightBagReady && rightStation)
        {
            if (rightClientOrder.Length != rightBurgerOrder.Length) return 0;

            for (int i = 0; i < rightClientOrder.Length; i++)
            {
                if (!rightClientOrder[i].Equals(rightBurgerOrder[i])) return 0;
            }

            return 3;
        }
        else return 0;
    }

    // Methods used to modify free when object is socketed
    public void IsFree()
    {
        free = 1;
    }

    public void IsNotFree()
    {
        free = 2;
    }

    // Methods to check if the bags were socketed on the stations, display array contents and order completion/failure in debug log
    public void LeftBagIsReady()
    {
        leftBagReady = true;

        foreach (string data in leftBurgerOrder)
        {
            Debug.Log(data);
        }

        if (CompareOrders() == 1)
        {
            Debug.Log("Congratulations, the left order is correct !");
            leftOrderCorrect = true;
        }
        else Debug.Log("You have failed the left order");
    }

    public void LeftBagIsNotReady()
    {
        leftBagReady = false;
    }

    public void MiddleBagIsReady()
    {
        middleBagReady = true;

        foreach (string data in middleBurgerOrder)
        {
            Debug.Log(data);
        }

        if (CompareOrders() == 2)
        {
            Debug.Log("Congratulations, the middle order is correct !");
            middleOrderCorrect = true;
        }
        else Debug.Log("You have failed the middle order");
    }

    public void MiddleBagIsNotReady()
    {
        middleBagReady = false;
    }

    public void RightBagIsReady()
    {
        rightBagReady = true;

        foreach (string data in rightBurgerOrder)
        {
            Debug.Log(data);
        }

        if (CompareOrders() == 3)
        {
            Debug.Log("Congratulations, the right order is correct !");
            rightOrderCorrect = true;
        }
        else Debug.Log("You have failed the right order");

    }

    public void RightBagIsNotReady()
    {
        rightBagReady = false;
    }

    // Methods to modify the station values
    public void OnLeftStation()
    {
        leftStation = true;
        middleStation = false;
        rightStation = false;
    }

    public void OnMiddleStation()
    {
        leftStation = false;
        middleStation = true;
        rightStation = false;
    }

    public void OnRightStation()
    {
        leftStation = false;
        middleStation = false;
        rightStation = true;
    }
}

