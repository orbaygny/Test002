using UnityEngine;
using Unity.Cinemachine;
public class CinemachineTargetChanger : MonoBehaviour
{
    public static CinemachineTargetChanger Instance;
    [SerializeField] CinemachineVirtualCamera cam;
    private void Awake()
    {
        Instance = this;
    }


    public void Change(Transform target)
    {
        cam.Follow = target;
    }
}
