using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.VisualScripting;
using System;
public class AnimationMatcher : MonoBehaviour
{
    public static Action<CarVariables> SetCarVariables;
    public static Action EnterVehicle;
    public static Action ExitVehicle;

    [SerializeField] Rig leftHandRig;
    [SerializeField] Transform leftTarget;

    [SerializeField] Rig rightHandRig;
    [SerializeField] Transform rightTarget;

    Vector3 rightTargetInitial;
    Vector3 leftTargetInitial;

    private Rig handRig;
    Transform target;

    Animator animator;
    bool isRigWeight;
    bool positionRigTarget;

    // Door Variables 
    Transform drivePos;
    Transform doorHandle;
    Transform seat;
    Transform leftHandSteerPos;
    Transform rightHandSteerPos;
    Animator doorAnim;
    bool isDriverSide;
    bool isEnterVehicle;
    bool handsSetted;
    private void OnEnable()
    {
        HandPositioner.FrameIK += InitIK;
        HandPositioner.SetGround += SetHeight;
        SetCarVariables += OnSetCarVariables;
        EnterVehicle += StartDoorAnim;
        ExitVehicle += OnExitVehicle;
    }
    private void OnDisable()
    {
        HandPositioner.FrameIK -= InitIK;
        HandPositioner.SetGround -= SetHeight;
        SetCarVariables -= OnSetCarVariables;
        EnterVehicle -= StartDoorAnim;
        ExitVehicle -= OnExitVehicle;
    }
    void OnSetCarVariables(CarVariables variables)
    {
        doorHandle = variables.DoorHandle;
        seat = variables.Seat;
        doorAnim = variables.DoorAnimator;
        isDriverSide = variables.IsDriverSide;
        drivePos = variables.DriverPos;
        leftHandSteerPos = variables.LeftHandSteerPos;
        rightHandSteerPos = variables.RightHandSteerPos;
        if (isDriverSide)
        {
            target = leftTarget;
            handRig = leftHandRig;
        }
        else
        {
            target = rightTarget;
            handRig = rightHandRig;
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        target = leftTarget;
    }
    private void Start()
    {
        leftTargetInitial = leftTarget.localPosition;
        rightTargetInitial = rightTarget.localPosition;
    }
    private void LateUpdate()
    {
        if (positionRigTarget)
            target.position = doorHandle.position;

        if (handsSetted)
        {
            leftTarget.position = leftHandSteerPos.position;
            rightTarget.position = rightHandSteerPos.position;
        }
    }
    public void StartDoorAnim()
    {
        isEnterVehicle = true;
        ToggleCharacterController.Toggle(false);
        transform.parent = doorHandle.root;
        positionRigTarget = true;
        Vector3 look = doorHandle.position;
        look.y = transform.position.y;
        transform.LookAt(look);
        if (isDriverSide)
            animator.SetTrigger("EnterCar");
        else
            animator.SetTrigger("EnterPassanger");

        doorAnim.Play("DoorOpen");
    }
    public void PlayerSeated()
    {
        VehicleControlsActivator.ToggleControl(true);
    }
    void SetHeight()
    {
        transform.DOMoveY(0, 1f);
    }
    void InitIK()
    {
        if (!isRigWeight)
        {
            animator.MatchTarget(
       seat.position,
      transform.rotation,
       AvatarTarget.Body,
       new MatchTargetWeightMask(Vector3.one, 0f),
       0.27f,
       0.52f
   );
            StartCoroutine(AnimateRigWeight(1, 0.5f));
            isRigWeight = true;
        }
        else
        {
            StartCoroutine(AnimateRigWeight(0, 0.5f));
            if (isEnterVehicle)
            {
                transform.DOLocalMoveX(drivePos.localPosition.x, 1f).SetDelay(2f);
                transform.DOLocalRotate(Vector3.zero, 1f).SetDelay(2f);
            }
            isRigWeight = false;
        }
    }
    public void SetHands()
    {
        handsSetted = true;
        leftTarget.DOLocalRotate(Vector3.forward * 95f, 0.1f);
        rightTarget.DOLocalRotate(Vector3.forward * 95f, 0.1f);
        StartCoroutine(AnimateRigWeight(leftHandRig, 1, 0.5f));
        StartCoroutine(AnimateRigWeight(rightHandRig, 1, 0.5f));
    }
    void OnExitVehicle()
    {
        handsSetted = false;
        leftTarget.localRotation = Quaternion.Euler(Vector3.zero);
        rightTarget.localRotation = Quaternion.Euler(Vector3.zero);
        rightHandRig.weight = 0;
        leftHandRig.weight = 0;
        target.position = doorHandle.position;
        isEnterVehicle = false;
        transform.parent = null;
        animator.SetTrigger("ExitCar");
        doorAnim.Play("DoorClose");
    }
    public void ChangeCamTarget()
    {
        positionRigTarget = false;
        CinemachineTargetChanger.Instance.Change(transform);
        JoystickVisibilty.Instance.ChangeStatus();
        ToggleCharacterController.Toggle(true);
        leftTarget.localPosition = leftTargetInitial;
        rightTarget.localPosition = rightTargetInitial;
    }
    private IEnumerator AnimateRigWeight(float targetWeight, float duration)
    {
        float startWeight = handRig.weight;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            handRig.weight = Mathf.Lerp(startWeight, targetWeight, time / duration);
            yield return null;
        }
    }
    private IEnumerator AnimateRigWeight(Rig rig, float targetWeight, float duration)
    {
        float startWeight = rig.weight;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            rig.weight = Mathf.Lerp(startWeight, targetWeight, time / duration);
            yield return null;
        }
    }
}


public enum Door
{
    DriverSide,
    PassengerSide
}
