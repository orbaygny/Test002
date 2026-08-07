using UnityEngine;

public class EnviromentInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out IInteractable interactable))
        {
            var type = interactable.GetType();
            InteractionButtonControl.Enable(type);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (TryGetComponent(out IInteractable interactable))
            InteractionButtonControl.Disable();
    }
}
