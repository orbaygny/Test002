using UnityEngine;

public interface IInteractable
{
    Interactables GetType();
}

public enum Interactables
{
    Car
}