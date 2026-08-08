using StarterAssets;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;
public class CarInteractionControl : MonoBehaviour, IInteractable
{
    public static System.Action OnExit; 
    readonly Interactables type = Interactables.Car;
    CarVariables driverSideVariables;
    CarVariables passengerSideVariables;
    [SerializeField] Transform cameraRoot;

    [SerializeField] Transform driverPos;
    [Header("Driver Side Variables")]
    [SerializeField] Animator driverDoorAnim;
    [SerializeField] Transform driverDoorHandle;
    [SerializeField] Transform driverSeat;
    [SerializeField] Transform driverEnterence;

    [Header("Passenger Side Variables")]
    [SerializeField] Animator passengerDoorAnim;
    [SerializeField] Transform passengerDoorHandle;
    [SerializeField] Transform passengerSeat;
    [SerializeField] Transform passengerEnterence;

    [SerializeField] Transform leftHandSteerPos;
    [SerializeField] Transform rightHandSteerPos;
    private void OnEnable()
    {
        OnExit += SetExitVariables;
    }
    private void OnDisable()
    {
        OnExit -= SetExitVariables;
    }
    public Interactables GetInteractableType()
    {
        return type;
    }

    public void Interact(Transform playerPos)
    {
        JoystickVisibilty.Instance.ChangeStatus();
        var driverDis = Vector3.Distance(playerPos.position, driverEnterence.position);
        var passengerDis = Vector3.Distance(playerPos.position, passengerEnterence.position);

        if (driverDis < passengerDis)
        {
            ThirdPersonController.SetDoorEnterence(driverEnterence);
            AnimationMatcher.SetCarVariables(driverSideVariables);
            StartCoroutine(Check(playerPos, driverEnterence));
        }
        else
        {
            ThirdPersonController.SetDoorEnterence(passengerEnterence);
            AnimationMatcher.SetCarVariables(passengerSideVariables);
            StartCoroutine(Check(playerPos, passengerEnterence));
        }

    }

    void SetExitVariables()
    {
        AnimationMatcher.SetCarVariables(driverSideVariables);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        driverSideVariables = new CarVariables
        {
            DriverPos = driverPos,
            DoorAnimator = driverDoorAnim,
            DoorHandle = driverDoorHandle,
            LeftHandSteerPos = leftHandSteerPos,
            RightHandSteerPos= rightHandSteerPos,
            Seat = driverSeat,
            IsDriverSide = true,
        };

        passengerSideVariables = new CarVariables
        {
            DriverPos = driverPos,
            DoorAnimator = passengerDoorAnim,
            DoorHandle = passengerDoorHandle,
            LeftHandSteerPos = leftHandSteerPos,
            RightHandSteerPos = rightHandSteerPos,
            Seat = passengerSeat,
            IsDriverSide = false,
        };
    }

    IEnumerator Check(Transform player , Transform door)
    {
        while (Vector3.Distance(player.position, door.position) > 0.5f)
        {
            yield return new WaitForSeconds(1f);
        }
        player.position = door.position;
        CinemachineTargetChanger.Instance.Change(cameraRoot); 
        AnimationMatcher.EnterVehicle();
    }
}
