using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Vector3 leftOpenPosition;
    public Vector3 rightOpenPosition;
    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;
    private bool isOpen = false;

    private void Start()
    {
        leftClosedPosition = leftDoor.localPosition;
        rightClosedPosition = rightDoor.localPosition;
    }

    public void ToggleDoor()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            leftDoor.DOLocalMove(leftOpenPosition, 1f);
            rightDoor.DOLocalMove(rightOpenPosition, 1f);
        }
        else
        {
            leftDoor.DOLocalMove(leftClosedPosition, 1f);
            rightDoor.DOLocalMove(rightClosedPosition, 1f);
        }
    }
}