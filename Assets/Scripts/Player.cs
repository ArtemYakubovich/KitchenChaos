using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;

    private bool _isWalking;
    private Vector3 _lastInteractDirection;

    private void Start()
    {
        _gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            _lastInteractDirection = moveDirection;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //clearCounter.Interact();
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = _moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0f, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDirection = moveDirZ;
                }
            }
        }
        
        if (canMove)
        {
            transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        }
        
        _isWalking = moveDirection != Vector3.zero;
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
}
