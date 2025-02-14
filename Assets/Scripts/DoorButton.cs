using System.Collections;
using UnityEngine;
using Zenject;

public class DoorButton : MonoBehaviour
{
   [Inject] private AudioManager audioManager;
   [Inject] private DoorController doorController;
   private bool isPressed = false;

   private void OnMouseDown()
   {
      PressButton();
   }

   public void PressButton()
   {
      if (!isPressed)
      {
         isPressed = true;
         StartCoroutine(ButtonMove());
         audioManager.PlaySound(audioManager.buttonClick);
         doorController.ToggleDoor();
         Invoke(nameof(ResetButton), 0.5f);
      }
   }

   private IEnumerator ButtonMove()
   {
      float duration = 0.5f;
      float elapsedTime = 0f;
      
      Vector3 startPosition = gameObject.transform.position;
      Vector3 endPosition = startPosition + new Vector3(0, -0.2f, 0);

      while (elapsedTime < duration)
      {
         gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
         elapsedTime += Time.deltaTime;
         yield return null;
      }
      gameObject.transform.position = startPosition;
   }

   private void ResetButton()
   {
      isPressed = false;
   }
}
