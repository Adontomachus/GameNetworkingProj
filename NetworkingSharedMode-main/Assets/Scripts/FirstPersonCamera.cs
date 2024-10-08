using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class FirstPersonCamera : MonoBehaviour
{
    private bool win;
    private float matchTime;
    public TextMeshProUGUI text;
    public float healthAmount;
    public int totalScore;
    public Transform Target;
    public GameObject Authority;

    public float MouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    private void Awake()
    {
        matchTime = 30f;
    }
    private void Update()
    {
        matchTime -= Time.deltaTime;
        PlayerMovement b = Authority.GetComponent<PlayerMovement>();
        totalScore = b.playerScore;
        PlayerHealth a = Authority.GetComponent<PlayerHealth>();
        healthAmount = a.currentHealthAmount;
    }
    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.position;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);
        horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }

}