using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WristSocket : MonoBehaviour
{
    [SerializeField] private float offsetY;
    public Inventory parentInventory;
    private XRGrabInteractable socketedInteractable;
    private Vector3 baseScale;

	/// <summary>
	/// Return true if the socket contain an object, false otherwise
	/// </summary>
    public bool ContainObject => socketedInteractable != null;

	/// <summary>
	/// Attach the XRGrabInteractable in the center (+offset) of the socket. Also makes the Rigidbody kinematic
	/// </summary>
	/// <param name="objectToAttach">The XRGrabInteractable to attach</param>
	private void Attach(XRGrabInteractable objectToAttach)
    {
        Debug.Log($"Attach {objectToAttach}");
	    socketedInteractable = objectToAttach;
	    baseScale = socketedInteractable.transform.localScale;
	    
	    // since XRGrabInteractable modifies the rigidbodies after grab event we must modify the rigidbody after it does
		socketedInteractable.selectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Attach");
			var rb = socketedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.isKinematic = true;
			socketedInteractable.transform.parent = transform;
			socketedInteractable.transform.localRotation = Quaternion.identity; // set the socketInteractable rotation the same as the parent socket
			socketedInteractable.selectExited.RemoveAllListeners();
		});

		// force drop the holdedObject and don't allow regrab it with the same input
		socketedInteractable.selectingInteractor.allowSelect = false;
		StartCoroutine(AllowSelect(socketedInteractable.selectingInteractor));
		
		socketedInteractable.retainTransformParent = false;
		socketedInteractable.transform.position = transform.position + offsetY * transform.up;
		socketedInteractable.selectEntered.AddListener(_ => Release()); // once the object is in the inventory the grab event will release it

		parentInventory.AddObject(socketedInteractable.gameObject);
	}

    private IEnumerator AllowSelect(XRBaseInteractor interactor)
    {
		yield return new WaitForEndOfFrame();
		interactor.allowSelect = true;
    }

	/// <summary>
	/// Release XRGrabInteractable from the socket and restore its previous state
	/// </summary>
	private void Release() 
    {
        Debug.Log($"Release {socketedInteractable}");
		socketedInteractable.selectEntered.RemoveAllListeners();
		socketedInteractable.transform.localScale = baseScale;
		var detachedInteractable = socketedInteractable; // used for lambda capture
		// same as above
		socketedInteractable.selectExited.AddListener(_ =>
		{
			Debug.Log("Select exit Release");
			var rb = detachedInteractable.GetComponent<Rigidbody>();
			rb.useGravity = true;
			rb.isKinematic = false;
			detachedInteractable.transform.parent = null;
			detachedInteractable.selectExited.RemoveAllListeners();
		});
		parentInventory.RemoveObject(socketedInteractable.gameObject);
	    socketedInteractable = null;
	}

	private void OnTriggerStay(Collider other)
	{
		if (ContainObject)
			return;

	    var interactable = other.GetComponentInParent<XRGrabInteractable>();
	    if (interactable != null &&
			parentInventory.CanAddObject &&				// not in animation
	        interactable.selectingInteractor != null && // if is grabbed 
	        interactable.transform.parent == null)		// if not already attached
	    {
            Attach(interactable);
	    }
    }

}
