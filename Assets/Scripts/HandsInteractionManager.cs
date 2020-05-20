using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsInteractionManager : MonoBehaviour
{
    private OVRHand hand;
    private string currentHand;

    // Gameobject used as dart in current hand
    public GameObject Dart;

    [SerializeField]
    private float minFingerPinchStrength = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        hand = gameObject.GetComponent<OVRHand>();
    }

    // Update is called once per frame
    void Update()
    {
        LoadHandDart();
    }

    void FixedUpdate()
    {
        // if hand tracking is reliable and index finger is pinched. Display dart
        if (isHandTrackingReliable())
        {

        }
        // upon releasing pinch, add gravity and 
    }

    private bool isHandTrackingReliable()
    {
        OVRHand.TrackingConfidence confidence = hand.GetFingerConfidence(OVRHand.HandFinger.Index);

        if (confidence == OVRHand.TrackingConfidence.High)
            return true;
        else
            return false;
    }

    private void LoadHandDart()
    {
        bool isIndexFingerPinching = hand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        float ringFingerPinchStrength = hand.GetFingerPinchStrength(OVRHand.HandFinger.Index);

        if (isIndexFingerPinching
            && ringFingerPinchStrength > 0.7f)
        {
            Debug.Log("!!! Dart Active");
            Dart.SetActive(true);
        }
        else
        {
            Dart.SetActive(false);
            Debug.Log("??? Dart De-Activated");
        }
    }
}
