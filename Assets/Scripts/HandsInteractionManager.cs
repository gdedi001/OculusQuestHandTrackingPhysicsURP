using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInteractionManager : MonoBehaviour
{
    private OVRHand ovrHand;
    private OVRCustomSkeleton ovrSkeleton;
    [SerializeField]
    private GameObject boneToTrack;

    [SerializeField]
    private float minFingerPinchStrength = 0.7f;

    // Gameobject used as dart in current hand
    public GameObject Dart;
    private bool dartLoaded;

    void Awake()
    {
        ovrHand = gameObject.GetComponent<OVRHand>();
        ovrSkeleton = gameObject.GetComponent<OVRCustomSkeleton>();
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(boneToTrack);  
    }

    void FixedUpdate()
    {
        // if hand tracking is reliable and index finger is pinched. Display dart
        if (isHandTrackingReliable())
        {
            if (isPinching())
                loadHandDart();
            else
                removeHandDart();
        }
        // upon releasing pinch, add gravity and 
    }

    private bool isHandTrackingReliable()
    {
        OVRHand.TrackingConfidence indexConfidence = ovrHand.GetFingerConfidence(OVRHand.HandFinger.Index);
        OVRHand.TrackingConfidence thumbConfidence = ovrHand.GetFingerConfidence(OVRHand.HandFinger.Thumb);

        if (indexConfidence == OVRHand.TrackingConfidence.High && thumbConfidence == OVRHand.TrackingConfidence.High)
            return true;
        else
            return false;
    }

    private bool isPinching()
    {
        bool isIndexFingerPinching = ovrHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float ringFingerPinchStrength = ovrHand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (isIndexFingerPinching
            && ringFingerPinchStrength >= minFingerPinchStrength)
            return true;
        else
            return false;
    }

    private void loadHandDart()
    {
        // place dart at bone we are tracking (thumb)
        Dart.transform.position = boneToTrack.transform.position;
        Dart.SetActive(true);
        dartLoaded = true;
        Debug.Log("!!! DART LOADED");
    }

    private void removeHandDart()
    {
        Dart.SetActive(false);
        dartLoaded = false;
        Debug.Log("??? DART REMOVED");
    }
}
