using UnityEngine;
using Zenject;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 4f;
    public LayerMask interactLayer;

    [Inject] private PlayerInventory inventory;


    public void OnInteractButtonPressed()
    {
        if (Camera.main == null) return;

        Vector2 screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            if (hit.collider.CompareTag("Pickup") || hit.collider.CompareTag("Button"))
            {
                if (inventory != null && inventory.heldItem != null)
                {
                    if (hit.collider.gameObject == inventory.heldItem)
                    {
                        Debug.Log("Tapped on held item. Dropping it.");
                        inventory.DropItem();
                    }
                    else
                    {
                        Debug.Log("Tapped on a different item. Swapping.");
                        inventory.DropItem();
                        inventory.TryInteract(hit);
                    }
                }
                else
                {
                   inventory?.TryInteract(hit);
                }
            }
            else
            {
                if (inventory != null && inventory.heldItem != null)
                {
                    Debug.Log("Non-interactive object hit. Dropping held item.");
                    inventory.DropItem();
                }
            }
        }
        else
        {
            if (inventory != null && inventory.heldItem != null)
            {
                Debug.Log("No interactive object hit. Dropping held item.");
                inventory.DropItem();
            }
        }
    }
}