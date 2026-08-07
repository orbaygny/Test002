using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class JoystickVisibilty : MonoBehaviour
{
    public static JoystickVisibilty Instance;

    [SerializeField] private UIDocument uiDocument;
    private VisualElement mobileInputContainer;

    void OnEnable()
    {
        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            mobileInputContainer = root.Q<VisualElement>("MobileInput");
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeStatus()
    {
        if (mobileInputContainer != null)
        {
            bool isVisible = mobileInputContainer.style.display != DisplayStyle.None;
            mobileInputContainer.style.display = isVisible ? DisplayStyle.None : DisplayStyle.Flex;
        }
    }
}
