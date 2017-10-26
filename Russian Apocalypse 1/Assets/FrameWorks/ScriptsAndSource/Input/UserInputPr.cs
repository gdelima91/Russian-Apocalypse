using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(InputClientManager))]
public class UserInputPr : MonoBehaviour {

    InputClientManager inputClientManager;

    public string fireButton;

    public KeyCode Key_A;
    public KeyCode Key_B;

    public Vector2 inputVH;
    public Vector2 currentVH;
    public Vector2 currentVelocity;
    public float smoothTime;

    public float mouseScroll;
    public float mouseVertical;
    public float mouseHorizontal;

    public bool Shift;

    public Vector2 InputVH { get { return inputVH; } }

    int statu = 0;

    private void Start()
    {
        inputClientManager = GetComponent<InputClientManager>();
        if (inputClientManager == null)
        {
            Debug.LogError("inputActionManager is Null");
        }
    }

    public void UpdateInput()
    {
#if UNITY_IOS || UNITY_ANDROID
		Vector2 input = TouchLib.GetSwipe2D();
		InputVH.x = input.x;
		InputVH.y = input.y;
		CrossPlatformButtonInput();
#else
        inputVH.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        inputVH.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        //Movement Information for Animation
        currentVH = Vector2.SmoothDamp(currentVH, inputVH, ref currentVelocity, smoothTime, float.MaxValue, Time.deltaTime);
        Shift = Input.GetKey(KeyCode.LeftShift);

        mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            mouseVertical = -Input.GetAxis("Mouse Y");
            mouseHorizontal = Input.GetAxis("Mouse X");
        }

        StandaredKeyInput();
#endif
        //TestForSwitchPlayModel();
    }
   
    void StandaredKeyInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
            inputClientManager.GetKey_A_Down();       
        }

        if (Input.GetMouseButton(0))
        {
            inputClientManager.GetKey_A();
        }

        if (Input.GetMouseButtonUp(0))
        {
            inputClientManager.GetKey_A_Up();
        }

        if (Input.GetKeyDown(Key_B))
        {
            inputClientManager.GetKey_B_Down();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            GameUIPr.Instance.InventoryPanelEvent();
        }
    }

    void CrossPlatformButtonInput()
    {
        if (CrossPlatformInputManager.GetButton(fireButton))
        {
            inputClientManager.GetKey_A();
        }

        if (CrossPlatformInputManager.GetButtonUp(fireButton))
        {
            inputClientManager.GetKey_A_Up();
        }
    }
}

public static class TouchLib
{
	static float startTime;
	static float endTime;

	static Vector2 startPos;
	static Vector2 endPos;
	static Vector2 move;

	static float minSwipDist = 20.0f;
	static float maxSwipDist = 100.0f;
	static float swipeDistance;

	static float maxTime = 1.0f;

	public static Vector2 GetSwipe2D()
	{
		if(Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Began)
			{
				startTime = Time.time;
				startPos = touch.position;
				return Vector2.zero;
			}else if(touch.phase == TouchPhase.Ended)
			{
				endTime = Time.time;
				endPos = touch.position;
				move = endPos - startPos;
				swipeDistance = move.magnitude;
				float swipeTime = endTime - startTime;
				if(swipeDistance > minSwipDist && swipeTime < maxTime)
				{
					return new Vector2(Mathf.Clamp(move.x/maxSwipDist, -1.0f,1.0f),
										Mathf.Clamp(move.y/maxSwipDist,-1.0f,1.0f));
				}
				return Vector2.zero;
			}
			return Vector2.zero;
		}
		return Vector2.zero;
	}

    static float perspectiveZoomSpeed = 0.5f;
    static float orthZoomSpeed = 0.5f;

    public static void ZoomInOut_ChangeFieldOfView()
    {
        if (Input.touchCount == 2)
        {
            float deltaMagnitudeDiff = GetDeltaMagnitudeDifferent();
            if (Camera.main.orthographic)
            {
                Camera.main.orthographicSize += deltaMagnitudeDiff * orthZoomSpeed;
                Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
            }
            else
            {
                Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
            }
        }
    }

    public static float GetDeltaMagnitudeDifferent()
    {
        Touch t1 = Input.GetTouch(0);
        Touch t2 = Input.GetTouch(1);
        Vector2 t1PrevPos = t1.position - t1.deltaPosition;
        Vector2 t2PrevPos = t2.position - t2.deltaPosition;

        float prevTouchMag = (t1PrevPos - t2PrevPos).magnitude;
        float currentMag = (t1.position - t2.position).magnitude;

        return prevTouchMag - currentMag;
    }
}

public static class GravitySensor
{
	public static Vector3 GetAcceleration()
	{
		Vector3 dir = Vector3.zero;
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;
		if(dir.sqrMagnitude > 1)
			dir.Normalize();
		return dir;
	}
}

#region InputInfoDefine

public enum inputStatu
{
    Non, Down, Up, Hold
}

[System.Serializable]
public class KeyInfo
{
    public KeyCode key;
    public string name;
    [HideInInspector]
    public inputStatu statu;
}

[System.Serializable]
public struct ButtonInfo
{
    public string name;
    public inputStatu statu;
}

#endregion
