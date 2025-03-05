using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]//Unity�� Inspector â���� ������ �׷�ȭ�ϰ� �������� ���̴� �Ӽ�(Attribute)
    public float moveSpeed;//�̵� �ӵ�
    private Vector2 curMoveMentInput;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;//�ּ�X��
    public float maxXLook;//�ִ�X��
    float camCurXRot;
    public float lookSensitivity;//�ΰ���
    Vector2 mouseDelta;

    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// ���콺�� ��ġ�� ����
    }
    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        CameraLook();
    }
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
    private void Move()
    {
        Vector3 dir = transform.forward * curMoveMentInput.y + transform.right * curMoveMentInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;//y�� �ʱ�ȭ

        _rigidbody.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext callbackContext)//callbackContext�� ���� ���¸� �޾ƿ� �� ����
    {
        if(callbackContext.phase==InputActionPhase.Started)
        {
            curMoveMentInput = callbackContext.ReadValue<Vector2>();
        }
        else if(callbackContext.phase == InputActionPhase.Canceled)
        {
            curMoveMentInput = Vector2.zero;
        }
    }
    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        mouseDelta = callbackContext.ReadValue<Vector2>();
    }
}
