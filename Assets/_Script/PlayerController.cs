using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float maxLaunchSpeed = 10f;

    [Header("Debug")]
    public Vector3 touchDownPos;
    public Vector3 touchUpPos;
    public Vector3 lastLaunchVelocity;
    public Rigidbody2D rb;

    public enum InputState { None = 0, ButtonDown = 1}
    public InputState inputState;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            touchDownPos = GetMousePosition();
            inputState = InputState.ButtonDown;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchUpPos = GetMousePosition();
            Launch(GetLaunchVelocity(touchDownPos, touchUpPos));
            inputState = InputState.None;
        }
	}

    private Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    private Vector3 GetLaunchVelocity(Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from).normalized;
        lastLaunchVelocity = dir * maxLaunchSpeed;
        return lastLaunchVelocity;
    }

    private void Launch(Vector3 launchVelocity)
    {
        rb.velocity = launchVelocity;
        Debug.Log("launch: " + launchVelocity);
    }

    private void OnDrawGizmos()
    {
        if(inputState == InputState.ButtonDown)
        {
            Vector3 fromScreen = touchDownPos;
            Vector3 toScreen = Input.mousePosition;
            
            Vector3 from = Camera.main.ScreenToWorldPoint(fromScreen);
            Vector3 to = Camera.main.ScreenToWorldPoint(toScreen);

            Vector3 dir = to - from;
            dir.z = 0;
            dir.Normalize();
            Gizmos.DrawRay(transform.position, dir);
        }
    }
}
