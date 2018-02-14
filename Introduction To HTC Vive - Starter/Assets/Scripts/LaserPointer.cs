using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {

    // Use this for initialization
    private SteamVR_TrackedObject trackedObj;

    public GameObject laserPrefab;

    private GameObject laser;

    private Transform laserTransform;

    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    
    public GameObject teleportReticlePrefab;
    
    private GameObject reticle;
    
    private Transform teleportReticleTransform;
    
    public Transform headTransform;
    
    public Vector3 teleportReticleOffset;
    
    public LayerMask teleportMask;
    
    private bool shouldTeleport;

    void Start()
    { 
        // 1
        laser = Instantiate(laserPrefab);
        // 2
        laserTransform = laser.transform;
        // 1
        reticle = Instantiate(teleportReticlePrefab);
        // 2
        teleportReticleTransform = reticle.transform;
    }
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private void ShowLaser(RaycastHit hit)
    {
        // 1
        laser.SetActive(true);
        // 2
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);
        // 3
        laserTransform.LookAt(hitPoint);
        // 4
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
    // Update is called once per frame
    void Update()
    {


        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;


            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                // 1
                reticle.SetActive(true);
                // 2
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                // 3
                shouldTeleport = true;
            }
        }
        else
        {
            laser.SetActive(false);
            reticle.SetActive(false);
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
        {
            Teleport();
        }
    }
        private void Teleport()
        {
            // 1
            shouldTeleport = false;
            // 2
            reticle.SetActive(false);
            // 3
            Vector3 difference = cameraRigTransform.position - headTransform.position;
            // 4
            difference.y = 0;
            // 5
            cameraRigTransform.position = hitPoint + difference;
        } 
}
