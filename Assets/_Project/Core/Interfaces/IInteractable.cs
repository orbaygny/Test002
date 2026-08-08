using UnityEngine;

public interface IInteractable
{
    Interactables GetInteractableType();
    void Interact(Transform playerPos);
}

public enum Interactables
{
    Car
}