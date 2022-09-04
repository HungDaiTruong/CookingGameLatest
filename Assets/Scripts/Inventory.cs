using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Inventory : MonoBehaviour
{
	private enum AskState
	{
		None,
		Open,
		Close,
	}

	[Header("References")]
    [SerializeField] private GameObject socketPrefab;

    [Header("Settings")]
    [SerializeField] private int maxSockets = 12; 
    [SerializeField] private float radius = 0.7f; // radius of the circle on which sockets will be placed
    [SerializeField] private float animDuration = 0.5f; // duration of the open / close animation

	private List<GameObject> objectList = new List<GameObject>();
    private List<WristSocket> sockets = new List<WristSocket>();

    private AskState askState;
	private bool isInAnimation;

	/// <summary>
	/// Check this to ensure that an object can be safely added to the inventory
	/// </summary>
	public bool CanAddObject => !isInAnimation;

	private void Awake()
	{
		GenerateSocketObjects();
	}

	/// <summary>
	/// Add an object to the inventory
	/// </summary>
	/// <param name="newObject">object to add</param>
	public void AddObject(GameObject newObject)
    {
        objectList.Add(newObject);
    }

	/// <summary>
	/// Remove the specified gameobject from inventory
	/// </summary>
	/// <param name="removedObject">object to remove</param>
    public void RemoveObject(GameObject removedObject)
    {
        objectList.Remove(removedObject);
	}

	/// <summary>
	/// Get the wrist local position relative to this.transform
	/// </summary>
	private void GetWristPosAndUp(int index, out Vector3 pos, out Vector3 up)
	{
		float step = 2 * Mathf.PI / maxSockets;
		float x = Mathf.Cos(index * step);
		float y = Mathf.Sin(index * step);
		up = new Vector3(x, y, 0);
		pos = up * radius;
	}

	/// <summary>
	/// Generate all wristSockets of inventory in circle around the hand
	/// </summary>
	private void GenerateSocketObjects()
    {
	    for (int i = 0; i < maxSockets; i++)
	    {
		    GameObject socketObject = Instantiate(socketPrefab, transform.position, Quaternion.identity, transform);
		    var socket = socketObject.GetComponent<WristSocket>();
		    socket.parentInventory = this;
		    socketObject.SetActive(false);
		    sockets.Add(socket);
	    }
	}

	private void Update()
    {
		if (isInAnimation || askState == AskState.None)
			return;

		if (askState == AskState.Open)
			OpenInventory();
		else
			CloseInventory();

	    askState = AskState.None;
    }

	/// <summary>
	/// Set the socket gameobject active and start the display animation (DOScale)
	/// </summary>
	/// <param name="socket">socket to display</param>
	/// <param name="posIndex">positionnal index (used to get socket position from socketsPos array)</param>
	private void DisplaySocket(WristSocket socket, int posIndex)
	{
		GameObject socketObject = socket.gameObject;
		socketObject.SetActive(true);
		socketObject.transform.DOScale(1.0f, animDuration).OnComplete(() =>
		{
			isInAnimation = false;
		});
		GetWristPosAndUp(posIndex, out var pos, out var up);
		socketObject.transform.up = up;
		socketObject.transform.DOLocalMove(pos, animDuration);
	}

	/// <summary>
	/// Open the inventory and set isInAnimation to true until the end of the open animation
	/// </summary>
	[ContextMenu("Open Inventory")]
    private void OpenInventory()
    {
	    Debug.Log("Open inventory");
	    isInAnimation = true;
	    int displayIndex = 0; // index of the wrist to display

	    foreach (var socket in sockets.Where(s => s.ContainObject))
	    {
		    DisplaySocket(socket, displayIndex++);
	    }

		// add empty socket if enough space
		if (objectList.Count < maxSockets)
			DisplaySocket(sockets[objectList.Count], displayIndex);
    }

	/// <summary>
	/// Close the inventory and set isInAnimation to true until the end of the close animation
	/// </summary>
	[ContextMenu("Close Inventory")]
    private void CloseInventory()
    {
	    isInAnimation = true;
		Debug.Log("Close inventory");
		foreach (var socket in sockets)
		{
			socket.transform.DOScale(0, animDuration);
			var socketObject = socket.gameObject; // used for lambda capture
			socket.transform.DOLocalMove(Vector3.zero, animDuration).OnComplete(() =>
			{
				socketObject.SetActive(false);
				isInAnimation = false;
			});
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// when the user hand trigger the inventory collider, ask for open it
		// we do not want break current animation by calling directly OpenInventory or CloseInventory
		if (other.CompareTag("Hand"))
			askState = AskState.Open;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Hand"))
			askState = AskState.Close;
	}

}
