using UnityEngine;
using UnityEngine.UI;

public class EnviromentInteractions : MonoBehaviour
{
    [SerializeField] Button interactButton;
    IInteractable current;
    private void OnEnable()
    {
        interactButton.onClick.AddListener(OnInteract);
    }
    private void OnDisable()
    {
        interactButton.onClick.RemoveListener(OnInteract);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            current = interactable;
            var type = interactable.GetInteractableType();
            InteractionButtonControl.Enable(type);
            Debug.Log(type);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            InteractionButtonControl.Disable();
            current = null;
        }
    }

    void OnInteract()
    {
        if (current == null) return;
        InteractionButtonControl.Disable();
        current.Interact(transform);
        current = null;
    }
}

public struct CarVariables
{
    public Transform DriverPos;
    public Transform DoorHandle;
    public Transform Seat;
    public Transform LeftHandSteerPos;
    public Transform RightHandSteerPos;
    public Animator DoorAnimator;
    public bool IsDriverSide;

}