using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _input;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;

    [Space(10)]
    [SerializeField] private LayerMask bombLayer;
    [SerializeField] private Transform bombArea;
    [SerializeField] private float bombRatio;
    //[SerializeField] private Transform bombPosition;
    //[SerializeField] private Transform bomb;

    private bool isAiming;
    private Color bombAreaColor;

    private void Awake()
    {
        bombArea.gameObject.SetActive(false);
        isAiming = false;

        bombAreaColor = bombArea.GetComponent<SpriteRenderer>().color;
        bombArea.localScale = Vector3.one * bombRatio;
    }
    private void Update()
    {
        GatherInput();
        Look();

        if (Input.GetKeyDown(KeyCode.L)) Aim();
        if (Input.GetKeyUp(KeyCode.L)) Shoot();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GatherInput()
    {
        float _inputX = Input.GetAxisRaw("Horizontal");
        float _inputY = Input.GetAxisRaw("Vertical");

        _input = new Vector3(_inputX,0, _inputY);
    }
    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }
    private void Move()
    {
        _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime); 
    }
    private void Aim()
    {
        isAiming = true;
        bombArea.GetComponent<SpriteRenderer>().color = bombAreaColor;
        bombArea.gameObject.SetActive(true);
    }
    private void Shoot()
    {
        if (!isAiming) return;

        bombArea.gameObject.SetActive(false);
        bombArea.GetComponent<SpriteRenderer>().color = Color.red;
        isAiming = false;

        Collider[] matchs = Physics.OverlapSphere(transform.position, bombRatio/2, bombLayer);

        if (matchs.Length > 1)
        {
            CatEntitie catA = matchs[0].gameObject.GetComponent<CatEntitie>();
            CatEntitie catB = matchs[1].gameObject.GetComponent<CatEntitie>();

            GameController.Instance.PerfectPairing(catA, catB);
        }
    }
}
