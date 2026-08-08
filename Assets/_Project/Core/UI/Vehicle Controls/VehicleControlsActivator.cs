using UnityEngine;

public class VehicleControlsActivator : MonoBehaviour
{
    public static System.Action<bool> ToggleControl;

    [SerializeField] GameObject vehicleControlsObject;

    private void OnEnable()
    {
        ToggleControl += OnToggle;
    }
    private void OnDisable()
    {
        ToggleControl -= OnToggle;
    }

    void OnToggle(bool value)
    {
        vehicleControlsObject.SetActive(value);
        if (value)
            ExitVehicleButtonControl.Toggle(true);
    }
}
