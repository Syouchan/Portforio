using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraController : MonoBehaviour
{
    public GameObject TargetObject;
    public float Height = 1.5f;
    public float Distance = 5.0f;
    public float HeightAngle = 10.0f;
    public float RotAngle = 0.0f;
    public float RotAngleAttenRate = 5.0f;
    public float AngleAttenRate = 40.0f;

    public bool EnableAtten = true;
    public float AttenRate = 3.0f;

    public bool EnableNoise = true;
    public float NoiseSpeed = 0.5f;
    public float MoveNoiseSpeed = 1.5f;
    public float NoiseCoeff = 1.3f;
    public float MoveNoiseCoeff = 2.5f;

    public bool EnableFieldOfViewAtten = true;
    public float FieldOfView = 50.0f;
    public float MoveFieldOfView = 60.0f;

    public float ForwardDistance = 2.0f;

    private Camera cam;
    private Vector3 addForward;
    private Vector3 deltaTarget;
    private Vector3 nowPos;
    //private float nowfov;

    private float nowRotAngle;
    private float nowHeightAngle;

    private Vector3 prevTargetPos;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        //nowfov = FieldOfView;
        nowPos = TargetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RotAngle -= Input.GetAxis("Mouse X") * 5.0f;
        HeightAngle += Input.GetAxis("Mouse Y") * 5.0f;
        HeightAngle = Mathf.Clamp(HeightAngle, 0.0f, 80.0f);

        var delta = TargetObject.transform.position - deltaTarget;
        deltaTarget = TargetObject.transform.position;

        // 減衰
        if (EnableAtten)
        {
            var deltaPos = TargetObject.transform.position - prevTargetPos;
            prevTargetPos = TargetObject.transform.position;
            deltaPos *= ForwardDistance;

            addForward += deltaPos * Time.deltaTime * 20.0f;
            addForward = Vector3.Lerp(addForward, Vector3.zero, Time.deltaTime * AttenRate);

            nowPos = Vector3.Lerp(nowPos, TargetObject.transform.position + Vector3.up * Height + addForward, Mathf.Clamp(Time.deltaTime * AttenRate, 0.0f, 1.0f));
        }
        else nowPos = TargetObject.transform.position + Vector3.up * Height;

        // 手ブレ
        bool move = Mathf.Abs(delta.x) > 0.0f;
        var noise = new Vector3();
        if (EnableNoise)
        {
            var ns = (move ? MoveNoiseSpeed : NoiseSpeed);
            var nc = (move ? MoveNoiseCoeff : NoiseCoeff);

            var t = Time.time * ns;

            var nx = Mathf.PerlinNoise(t, t) * nc;
            var ny = Mathf.PerlinNoise(t + 10.0f, t + 10.0f) * nc;
            var nz = Mathf.PerlinNoise(t + 20.0f, t + 20.0f) * nc * 0.5f;
            noise = new Vector3(nx, ny, nz);
        }

        // FoV
        /*if (Input.GetKey(KeyCode.LeftShift) nowfov = Mathf.Lerp(nowfov, move ? MoveFieldOfView : FieldOfView, Time.deltaTime);
        else nowfov = FieldOfView;
        cam.fieldOfView = nowfov;*/

        // カメラ位置
        if (EnableAtten) nowRotAngle = Mathf.Lerp(nowRotAngle, RotAngle, Time.deltaTime * RotAngleAttenRate);
        else nowRotAngle = RotAngle;

        if (EnableAtten) nowHeightAngle = Mathf.Lerp(nowHeightAngle, HeightAngle, Time.deltaTime * RotAngleAttenRate);
        else nowHeightAngle = HeightAngle;

        var deg = Mathf.PI / 180.0f;
        var cx = Mathf.Sin(nowRotAngle * deg) * Mathf.Cos(nowHeightAngle * deg) * Distance;
        var cz = -Mathf.Cos(nowRotAngle * deg) * Mathf.Cos(nowHeightAngle * deg) * Distance;
        var cy = Mathf.Sin(nowHeightAngle * deg) * Distance;
        transform.position = nowPos + new Vector3(cx, cy, cz);

        // カメラ向き
        var rot = Quaternion.LookRotation((nowPos - transform.position).normalized) * Quaternion.Euler(noise);
        if (EnableAtten) transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * AngleAttenRate);
        else transform.rotation = rot;
    }
}