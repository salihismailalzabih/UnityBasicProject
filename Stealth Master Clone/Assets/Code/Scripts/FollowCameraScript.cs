

using System;
using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.EventSystems;

public class FollowCameraScript : MonoBehaviour
{
	public Transform target;

    public float distance = 5.0f;
    private float lerpDistance = 0;

  
    public float distanceMin = 3f;
    public float distanceMax = 15f;

    private float storedLimit = 0;

    [Tooltip("The amount of smooth-out-effect.")]
    //add the smoothing variable
    public float smoothTime = 2f;

    //define the rotation axes
   public float rotationYAxis = 0.0f;
   public float rotationXAxis = 0.0f;
   public float rotationZAxis = 0.0f;
    [HideInInspector]
    public Quaternion rotation;

    //define the main velocity
    [HideInInspector]
    public float velocityX = 0.0f;
    [HideInInspector]
    public float velocityY = 0.0f;

    [Space(20)]
    [Header("Set Camera Values")]

    public float xOffset;
	public float yOffset;
	public float zOffSet;


    public bool Animation = false;


    private bool useTouch = false;

    
	[HideInInspector]
	public float velocityPanX;
	[HideInInspector]
	public float velocityPanY;

    private bool doOrbit = false;

   
    private EventSystem eventSystem;

	void Start()
	{
		
		Vector3 angles = transform.eulerAngles;
		rotationYAxis = angles.y;
		rotationXAxis = angles.x;

	    lerpDistance = distance;

	    rotation = Quaternion.Euler(rotationXAxis, rotationYAxis, rotationZAxis);
	}

	void LateUpdate()
	{
		if (target)
		{
			rotationYAxis += velocityX;
			rotationXAxis -= velocityY;

			if (Animation)
            {
                RaycastHit hit;
                if (Physics.Linecast(target.position, transform.position, out hit))
                {
                    float tempDistance = distance;
                    tempDistance -= hit.distance;

                    if (tempDistance < distanceMin)
                    {
                        tempDistance = distanceMin;
                    }
                    distance = Mathf.Lerp(distance, tempDistance, Time.deltaTime * smoothTime * 0.5f);
                }
            }

            if (target != null)
            {
                
                lerpDistance = Mathf.Lerp(lerpDistance, distance, smoothTime * Time.deltaTime);

         
                Vector3 negDistance = new Vector3(0.0f, 0.0f, -lerpDistance);

                Vector3 position = rotation * negDistance + target.position;
                Vector3 offsetPosition = new Vector3(position.x + xOffset, position.y + yOffset, position.z + zOffSet);

                transform.rotation = rotation;
                transform.position = offsetPosition;
            }

            velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
            velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);

		}

	}
}
