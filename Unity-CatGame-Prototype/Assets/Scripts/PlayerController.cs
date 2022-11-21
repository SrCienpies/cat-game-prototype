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
    [SerializeField] private GameObject bomb;
    [SerializeField] private float bombRatio;
    [SerializeField] private int bombAmount;
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

        if (bombAmount > 0)
        {
            if (Input.GetKeyDown(KeyCode.L)) Aim();
            if (Input.GetKeyUp(KeyCode.L)) Shoot();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Input.GetKey(KeyCode.L))
                {
                    isAiming = false;
                    bombArea.gameObject.SetActive(false);
                }
            }
        }

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
        if (bombAmount <= 0) return;


        bombArea.gameObject.SetActive(false);
        bombArea.GetComponent<SpriteRenderer>().color = Color.red;
        isAiming = false;

        LoveBomb newBomb =  Instantiate(bomb, bombArea.position, Quaternion.identity).GetComponent<LoveBomb>();

        newBomb.bombRatio = bombRatio;
        newBomb.bombLayer = bombLayer;

        GameController.Instance.UpdateBombPanel(bombAmount - 1, false);
        bombAmount--;
    }
}
