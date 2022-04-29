using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Camera : MonoBehaviour
{
    // transform que la camera va suivre
    public Transform playerTarget;
    // les coordonnées de la camera
    public float camHeight = 10f;
    public float camDistance = 20f;
    public float camAngle = 45f;




    // Start is called before the first frame update
    void Start()
    {
        HandleCamera();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
    }

    protected virtual void HandleCamera()
    {
        if (!playerTarget)
        {
            return;
        }

        // le vecteur en World position
        Vector3 worldPosition = (Vector3.forward * camDistance) + (Vector3.up * camHeight);

        // le vecteur de rotation
        Vector3 rotateVector = Quaternion.AngleAxis(camAngle, Vector3.up) * worldPosition;

        // Fait bouger la position de la camera
        Vector3 flatTargetPosition = playerTarget.position;
        flatTargetPosition.y = 0f;
        Vector3 finalPosition = flatTargetPosition + rotateVector;

        transform.position = finalPosition;

    }
}
