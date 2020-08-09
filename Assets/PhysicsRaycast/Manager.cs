using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Transform cylinder;
    public Transform physicsRaycast;
    Vector3 initPosition;
    Vector3 initRotation;
    public Button angle;
    public Button position;
    public Button reset;
    /// <summary>
    /// 点击时，只执行射线碰撞检测，不进行角度和位置的变化；
    /// </summary>
    public Button rePhysicsRaycast;
    /// <summary>
    /// 射线碰撞到的点
    /// </summary>
    Vector3 hitPosition = Vector3.zero;
    /// <summary>
    /// 在Update中是否开始打印
    /// </summary>
    bool isDebugMessage = false;
    /// <summary>
    /// Update中开启打印时间，默认打印1s数据后停止打印
    /// </summary>
    float time = 1;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = cylinder.position;
        initRotation = cylinder.localEulerAngles;

        angle.onClick.AddListener(AngleClick);
        position.onClick.AddListener(PositionClick);
        reset.onClick.AddListener(ResetClinder);
        rePhysicsRaycast.onClick.AddListener(RePhysicsRaycastClick);
    }

    void AngleClick()
    {
        Transform obj = cylinder.GetChild(0).GetComponent<Transform>();
        Debug.Log("位置变化---前，物体位置坐标信息：        X1 : " + obj.position.x + "***"
            + "Y1 : " + obj.position.y + "***"
            + "Z1 : " + obj.position.z);

        cylinder.localEulerAngles = new Vector3(cylinder.localEulerAngles.x, cylinder.localEulerAngles.y, cylinder.localEulerAngles.z - 10);
        UpdatePhysics("角度发生改变： ");

        Debug.Log("位置变化----后，物体位置坐标信息：        X2 : " + obj.position.x + "***"
            + "Y2 : " + obj.position.y + "***"
            + "Z2 : " + obj.position.z);
    }

    void PositionClick()
    {
        Transform obj = cylinder.GetChild(0).GetComponent<Transform>();
        Debug.Log("位置变化---前，物体位置坐标信息：        X3 : " + obj.position.x + "***"
            + "Y3 : " + obj.position.y + "***"
            + "Z3 : " + obj.position.z);

        cylinder.position = new Vector3(cylinder.position.x, cylinder.position.y + 0.2f, cylinder.position.z);
        UpdatePhysics("位置发生改变： ");

        Debug.Log("位置变化----后，物体位置坐标信息：        X4 : " + obj.position.x + "***"
            + "Y4 : " + obj.position.y + "***"
            + "Z4 : " + obj.position.z);
    }

    void ResetClinder()
    {
        cylinder.position = initPosition;
        cylinder.localEulerAngles = initRotation;
    }

    void RePhysicsRaycastClick()
    {
        UpdatePhysics("不进行位置、角度，只进行射线碰撞检测：");

        Debug.Log("不进行位置、角度，只进行射线碰撞检测，碰撞点位置信息：     X5 : " + hitPosition.x + "***"
                    + "Y5 : " + hitPosition.y + "***"
                    + "Z5 : " + hitPosition.z);
    }

    void UpdatePhysics(string name)
    {
        isDebugMessage = true;
        time = 1;

        //创建一条指定方向的射线
        Ray ray = new Ray(physicsRaycast.position, physicsRaycast.right * 100);
        RaycastHit hit;
        //范围内是否有碰撞
        bool isHit = Physics.Raycast(ray, out hit, 100);
        if (isHit)
        {
            hitPosition = hit.point;

            Debug.Log("按钮点击时，射线碰撞检测，碰撞点位置信息：     X6 : " + hitPosition.x + "***"
            + "Y6 : " + hitPosition.y + "***"
            + "Z6 : " + hitPosition.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDebugMessage)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                isDebugMessage = false;
                time = 1;
            }
            else
            {
                //创建一条指定方向的射线
                Ray ray = new Ray(physicsRaycast.position, physicsRaycast.right * 100);
                RaycastHit hit;
                //范围内是否有碰撞
                bool isHit = Physics.Raycast(ray, out hit, 100);

                if (isHit)
                {
                    hitPosition = hit.point;

                    Debug.Log("在Update中开启射线检测：     X7 : " + hitPosition.x + "***"
                                + "Y7 : " + hitPosition.y + "***"
                                + "Z7 : " + hitPosition.z);
                }

            }
        }
    }
}
