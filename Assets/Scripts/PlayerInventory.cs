using UnityEngine;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    [Inject] private AudioManager audioManager;
    public Transform pickupSlot;
    public float throwForce = 5f;

    internal GameObject heldItem;
    private Rigidbody heldItemRb;

    public void TryInteract(RaycastHit hit)
    {
        Debug.Log($"Trying to interact with: {hit.collider.name}");

        if (heldItem == null)
        {
            Debug.Log("No item held, attempting to pickup.");
            PickupItem(hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Item already held, dropping current item.");
            DropItem();
        
            if (hit.collider.CompareTag("Pickup"))
            {
                Debug.Log("Picking up new item after drop.");
                PickupItem(hit.collider.gameObject);
            }
        }
    }

    internal void DropItem()
    {
        if (heldItem == null)
        {
            Debug.LogWarning("No item to drop.");
            return;
        }

        heldItem.transform.SetParent(null);
        heldItemRb.isKinematic = false;
        heldItemRb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        Debug.Log($"Dropped: {heldItem.name}");
        audioManager.PlaySound(audioManager.impact);
        heldItem = null;
    }

    private void PickupItem(GameObject item)
    {
        if (item == null)
        {
            Debug.LogError("Trying to pickup a null item!");
            return;
        }

        heldItem = item;
        heldItemRb = item.GetComponent<Rigidbody>();

        if (heldItemRb == null)
        {
            Debug.LogError("Pickup item is missing a Rigidbody!");
            return;
        }

        heldItem.transform.SetParent(pickupSlot);
        heldItem.transform.localPosition = Vector3.zero;
        //heldItem.transform.localRotation = Quaternion.identity;
        heldItemRb.isKinematic = true;

        Debug.Log($"Picked up: {item.name}");
        audioManager.PlaySound(audioManager.pickup);
    }
}
