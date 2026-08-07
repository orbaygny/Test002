using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class InteractionButtonControl : MonoBehaviour
{
    public static Action<Interactables> Enable;
    public static Action Disable;

    [SerializeField] Button button;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI tmp;

    private void OnEnable()
    {
        Enable += OnButtonEnable;
        Disable += OnButtonDisable;
    }
    private void OnDisable()
    {
        Enable -= OnButtonEnable;   
        Disable -= OnButtonDisable;
    }

    void OnButtonEnable(Interactables type)
    {
        tmp.text = GetText(type);
        button.enabled = true;
        image.enabled= true;
        tmp.enabled = true;
    }
    void OnButtonDisable()
    {
        button.enabled = false;
        image.enabled = false;  
        tmp.enabled = false;    
    }

    string GetText(Interactables type) => type switch
    {
        Interactables.Car => "Enter Car",
        _=> "ERROR INTERACTABLE TYPE MISMATCH"
    };
}
