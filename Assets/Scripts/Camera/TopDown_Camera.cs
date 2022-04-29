using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Camera : MonoBehaviour
{
    /// <summary>
    /// Le code me provient de https://www.youtube.com/c/IndiePixel3D/search?query=unity%20camera
    /// </summary>

    // transform que la camera va suivre
    public Transform playerTarget;
    // les coordonnées de la camera
    [SerializeField]
    private float camHeight = 10f;

    [SerializeField]
    private float camDistance = 20f;

    [SerializeField]
    private float camAngle = 45f;

    [SerializeField]
    private float camSmoothSpeed = 0.5f;


    private Vector3 refVelocity;



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

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, camSmoothSpeed);
        transform.LookAt(flatTargetPosition);

    }
}
