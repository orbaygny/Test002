using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExitVehicleButtonControl : MonoBehaviour
{
    public static System.Action<bool> Toggle;
    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI tmp;

    private void OnEnable()
    {
        Toggle += OnToggle;
        button.onClick.AddListener(OnPressButton);
    }
    private void OnDisable()
    {
        Toggle -= OnToggle;
        button.onClick.RemoveListener(OnPressButton);
    }
    void OnToggle(bool value)
    {
        button.enabled = value;
        image.enabled = value;
        tmp.enabled = value;
    }
    void OnPressButton()
    {
        CarInteractionControl.OnExit();
        OnToggle(false);
        VehicleControlsActivator.ToggleControl(false);
        AnimationMatcher.ExitVehicle();
    }
}
