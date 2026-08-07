using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using DG.Tweening;
public class AnimationMatcher : MonoBehaviour
{
    [SerializeField]
    Animator door;
    [SerializeField] private Rig handRig;

    [SerializeField] Transform doorHandle;
    [SerializeField] Transform doorClosePoint;
    [SerializeField] Transform target;
    [SerializeField] Transform seat;
    [SerializeField] Transform ground;


    Animator animator;
    bool isRigWeight;
    bool positionRigTarget;
    private void OnEnable()
    {
        HandPositioner.FrameIK += InitIK;
        HandPositioner.SetGround += SetHeight;
    }
    private void OnDisable()
    {
        HandPositioner.FrameIK -= InitIK;
        HandPositioner.SetGround -= SetHeight;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        //if (Keyboard.current.spaceKey.wasPressedThisFrame) TestAnim();
    }
    private void LateUpdate()
    {
        if (positionRigTarget)
            target.position = doorHandle.position;
    }
    public void StartDoorAnim()
    {
        //animator.SetTrigger("EnterCar");
        animator.SetTrigger("EnterPassanger");
        door.Play("DoorOpen");
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
            transform.DOMoveX(0, 1f).SetDelay(1.75f);
            isRigWeight = false;
        }
    }
    void TestAnim()
    {
        animator.SetTrigger("ExitCar");
        door.Play("DoorClose");
    }
    private IEnumerator AnimateRigWeight(float targetWeight, float duration)
    {
        float startWeight = handRig.weight;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            handRig.weight = Mathf.Lerp(startWeight, targetWeight, time / duration);
            yield return null; // Bir sonraki kareye kadar bekler
        }
    }
}


public enum Door
{
    DriverSide,
    PassengerSide
}
