using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationMatcher : MonoBehaviour
{
    [SerializeField]
    Animator door;
    [SerializeField] private Rig handRig;

    [SerializeField] Transform doorHandle;
    [SerializeField] Transform target;
    Animator animator;
    bool a;
    private void OnEnable()
    {
        HandPositioner.FrameIK += InitIK;
    }
    private void OnDisable()
    {
        HandPositioner.FrameIK -= InitIK;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        Invoke(nameof(StartDoorAnim), 1f);
    }
    private void LateUpdate()
    {
        target.position = doorHandle.position;
    }
    public void StartDoorAnim()
    {
        animator.SetTrigger("EnterCar");
        door.Play("DoorOpen");
    }

    void InitIK()
    {
        if (!a)
        {
            StartCoroutine(AnimateRigWeight(1, 0.5f));
            a = true;
        }
        else
        {
            StartCoroutine(AnimateRigWeight(0, 0.5f));
            a = false;
        }
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
        // handRig.weight = targetWeight; // Deðeri tam eþitleyip bitirir
    }
}
