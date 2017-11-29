using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputManager : MonoBehaviour
{
    LEMainBase mainBase;
    UserInput userInput;
    UPDATE currentUpdate;

    private void Start()
    {
        mainBase = GetComponent<LEMainBase>();
        if (mainBase.processType == LEMainBase.ProcessType.MouseKey_Input)
        {
            currentUpdate = userInput = new UserInput();
        }
    }

    public MKInputData INPUTDATA { get { return userInput.INPUTDATA; } }
    public void UpdateInput() { currentUpdate.UPDATE();}

    interface UPDATE { void UPDATE(); }

    class UserInput : UPDATE
    {
        MKInputData data = new MKInputData();
        const System.Int16 LEFT_MOUSE = 0;
        const System.Int16 RIGHT_MOUSE = 1;

        [SerializeField]
        private string Button_A;
        [SerializeField]
        private KeyCode Key_A;
        [SerializeField]
        private KeyCode Key_B;

        Vector2 inputVH;
        Vector2 currentVelocity;
        float smoothTime = 0.0f;

        public MKInputData INPUTDATA { get { return data; } }

        public void UPDATE()
        {

#if UNITY_IOS || UNITY_ANDROID
	        Vector2 input = TouchLib.GetSwipe2D();
	        InputVH.x = input.x;
	        InputVH.y = input.y;
	        CrossPlatformButtonInput();
#else
            inputVH.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            inputVH.y = CrossPlatformInputManager.GetAxisRaw("Vertical");
#endif
            //Movement Information for Animation
            data.currentVH = Vector2.SmoothDamp(data.currentVH, inputVH, ref currentVelocity, smoothTime, float.MaxValue, Time.deltaTime);
            data.mouseScroll = Input.GetAxis("Mouse ScrollWheel");

            if (Input.GetMouseButton(RIGHT_MOUSE))
            {
                data.mouseVertical = -Input.GetAxis("Mouse Y");
                data.mouseHorizontal = Input.GetAxis("Mouse X");
            }

            StandaredKeyInput();
            //Composite Data .....
        }

        void StandaredKeyInput()
        {
            //Left Mouse Down
            if (Input.GetMouseButtonDown(LEFT_MOUSE))
                data.DownMask.AddMask((int)InputEnum.LeftMouse);
            else
                data.DownMask.RemoveMask((int)InputEnum.LeftMouse);

            //Left Mouse Hold
            if (Input.GetMouseButton(LEFT_MOUSE))
                data.HoldMask.AddMask((int)InputEnum.LeftMouse);
            else
                data.HoldMask.RemoveMask((int)InputEnum.LeftMouse);

            //Left Mouse Up
            if (Input.GetMouseButtonUp(LEFT_MOUSE))
                data.UpMask.AddMask((int)InputEnum.LeftMouse);
            else
                data.UpMask.RemoveMask((int)InputEnum.LeftMouse);

        }

        void CrossPlatformButtonInput()
        {
            //Hold Button A
            if (CrossPlatformInputManager.GetButton(Button_A))
                data.HoldMask.AddMask((int)InputEnum.Button_A);
            else
                data.HoldMask.RemoveMask((int)InputEnum.Key_A);

            //Button A Up
            if (CrossPlatformInputManager.GetButtonUp(Button_A))
                data.UpMask.AddMask((int)InputEnum.Button_A);
            else
                data.UpMask.RemoveMask((int)InputEnum.Button_A);
        }
    }

    class AIInputCommandQueue
    {

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                startPos = touch.position;
                return Vector2.zero;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTime = Time.time;
                endPos = touch.position;
                move = endPos - startPos;
                swipeDistance = move.magnitude;
                float swipeTime = endTime - startTime;
                if (swipeDistance > minSwipDist && swipeTime < maxTime)
                {
                    return new Vector2(Mathf.Clamp(move.x / maxSwipDist, -1.0f, 1.0f),
                                        Mathf.Clamp(move.y / maxSwipDist, -1.0f, 1.0f));
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
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        return dir;
    }
}

/// <summary>
/// MKInput ------- Mouse & Key Input
/// </summary>
public struct MKInputData
{
    public float mouseScroll;
    public float mouseHorizontal;
    public float mouseVertical;
    //3 x 4  = 12 byte

    public Vector2 currentVH;
    //12 + 8 = 20 byte

    public VMask DownMask;
    public VMask HoldMask;
    public VMask UpMask;
    //20 + 12 = 32 byte
}


[System.Flags]
public enum InputEnum
{
    LeftMouse    = 1,
    RightMOuse   = 1 << 1,
    MiddleMouse  = 1 << 2,

    Key_A        = 1 << 3,
    Key_B        = 1 << 4,
    Key_C        = 1 << 5,
    Key_D        = 1 << 6,
    Key_E        = 1 << 7,

    Button_A     = 1 << 8,
}