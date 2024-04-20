using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCustomController : MonoBehaviour
{
    public CinemachineVirtualCamera aimCamera;
    public CinemachineVirtualCamera mainCamera;
    public StarterAssets.ThirdPersonController thirdPersonController;
    public GameObject bullet;
    public Transform firePoint;
    public Animator animator;
    public ParticleSystem particleSystem;
    public GameObject crosshair;
    public AudioSource fireSound;


    public LayerMask collisionMask;
    public GameObject shootAimObject;
    public UnityEngine.Animations.Rigging.Rig aimAnimationRig, defaultAnimationRig;

    float attackCoolDownTime;
    float cameraShakeCoolDownTime = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _mouseWorldPosition = Vector3.zero;
        Vector2 _center = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray _ray = Camera.main.ScreenPointToRay(_center);
        if (Physics.Raycast(_ray, out RaycastHit raycastHit, 100f, collisionMask))
        {
            shootAimObject.transform.position = raycastHit.point;
            _mouseWorldPosition = raycastHit.point;
        }

        if (Input.GetMouseButton(1))
        {
            aimCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);
            thirdPersonController.rotationSensitivity = 0.5f;
            thirdPersonController.isMomentRotationStopped = true;
            crosshair.SetActive(true);

            Vector3 _aimAngle = _mouseWorldPosition;
            _aimAngle.y = transform.position.y;
            Vector3 _aimDirection = (_aimAngle - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, _aimDirection, 20 * Time.deltaTime);
          //  aimAnimationRig.weight = 1;
           // defaultAnimationRig.weight = 0;
           // animator.SetLayerWeight(1, 1);

        }
        else
        {
            mainCamera.gameObject.SetActive(true);
            aimCamera.gameObject.SetActive(false);
            crosshair.SetActive(false);
            thirdPersonController.rotationSensitivity = 1f;
            thirdPersonController.isMomentRotationStopped = false;
          //  aimAnimationRig.weight = 0;
            //defaultAnimationRig.weight = 1;
            //animator.SetLayerWeight(1, 0);
        }


        if (Input.GetMouseButton(0) && attackCoolDownTime <= 0)
        {
            if (!animator.GetBool("Shoot"))
                animator.SetBool("Shoot", true);

            attackCoolDownTime = 0.1f;
            Vector3 _direction = (_mouseWorldPosition - firePoint.position).normalized;
            GameObject _bullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(_direction, Vector3.up));

            fireSound.Play();

            mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.3f;
            aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.3f;
            particleSystem.Play();
           
        }
        else if (Input.GetMouseButtonUp(0))
        { 
            if (animator.GetBool("Shoot"))
                animator.SetBool("Shoot", false);
            aimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        }


        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            animator.SetLayerWeight(1, 1);
            aimAnimationRig.weight = 1;
            defaultAnimationRig.weight = 0;
        }
        else
        {
            animator.SetLayerWeight(1, 0);
            aimAnimationRig.weight = 0;
            defaultAnimationRig.weight = 1;
        }

        if (attackCoolDownTime > 0)
            attackCoolDownTime -= Time.deltaTime;

        if (cameraShakeCoolDownTime > 0)
            cameraShakeCoolDownTime -= Time.deltaTime;
    }
}
