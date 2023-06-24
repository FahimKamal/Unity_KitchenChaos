using System;
using UnityEngine;

namespace GameScripts {
    public class Player : MonoBehaviour, IKitchenObjectParent {
        public static Player Instance{ get; private set; }

        public event EventHandler OnPickedSomething;

        private void Awake(){
            if (Instance != null){
                Debug.Log("There is more than one Player instance.");
            }

            Instance = this;
        }

        public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

        public class OnSelectedCounterChangedEventArgs : EventArgs {
            public BaseCounter SelectedCounter;
        }

        [SerializeField] private float moveSpeed = 7f;
        [SerializeField] private GameInput gameInput;
        [SerializeField] private LayerMask counterLayerMask;
        [SerializeField] private Transform kitchenObjectHoldPoint;

        private bool _isWalking;
        private Vector3 _lastInteractDir;

        private BaseCounter _selectedCounter;
        private KitchenObject _kitchenObject;

        private void Start(){
            gameInput.OnInteractAction += GameInputOnInteractAction;
            gameInput.OnInteractAlternateAction += GameInputOnInteractAlternateAction;
        }

        private void GameInputOnInteractAlternateAction(object sender, EventArgs e){
            if (!KitchenGameManager.Instance.IsGamePlaying) return;
            
            if (_selectedCounter != null){
                _selectedCounter.InteractAlternate(this);
            }
        }

        private void GameInputOnInteractAction(object sender, EventArgs e){
            if (!KitchenGameManager.Instance.IsGamePlaying) return;
            if (_selectedCounter != null){
                _selectedCounter.Interact(this);
            }
        }

        private void Update(){
            HandleMovement();

            HandleInteractions();
        }

        public bool IsWalking(){
            return _isWalking;
        }

        private void HandleInteractions(){
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            if (moveDir != Vector3.zero){
                _lastInteractDir = moveDir;
            }

            var interactDistance = 2.0f;

            if (Physics.Raycast(transform.position, _lastInteractDir, out var raycastHit, interactDistance,
                    counterLayerMask)){
                if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter)){
                    if (clearCounter != _selectedCounter){
                        SetSelectedCounter(clearCounter);
                    }
                }
                else{
                    SetSelectedCounter(null);
                }
            }
            else{
                SetSelectedCounter(null);
            }
        }

        private void SetSelectedCounter(BaseCounter selectedCounter){
            _selectedCounter = selectedCounter;
            OnSelectedCounterChanged?.Invoke(this,
                new OnSelectedCounterChangedEventArgs{ SelectedCounter = _selectedCounter });
        }

        private void HandleMovement(){
            var inputVector = gameInput.GetMovementVectorNormalized();
            var moveDir = new Vector3(inputVector.x, 0.0f, inputVector.y);

            // Detect side collisions
            var moveDistance = moveSpeed * Time.deltaTime;
            const float playerRadius = 0.5f;
            const float playerHeight = 2.0f;

            // If CapsuleCast return false then it means player can move forward.
            var playerPosition = transform.position;
            var canMove = !Physics.CapsuleCast(playerPosition, playerPosition + Vector3.up * playerHeight, playerRadius,
                moveDir, moveDistance);

            if (!canMove){
                // cannot move towards moveDir
                // Attempt only X movement
                var moveDirX = new Vector3(moveDir.x, 0.0f, 0.0f).normalized;
                canMove = moveDir.x != 0 && !Physics.CapsuleCast(playerPosition,
                    playerPosition + Vector3.up * playerHeight, playerRadius,
                    moveDirX, moveDistance);
                if (canMove){
                    // can only move on the X
                    moveDir = moveDirX;
                }
                else // cannot move on the x
                {
                    // Attempt only Z movement
                    var moveDirZ = new Vector3(0.0f, 0.0f, moveDir.z).normalized;
                    canMove = moveDir.z != 0 && !Physics.CapsuleCast(playerPosition,
                        playerPosition + Vector3.up * playerHeight,
                        playerRadius, moveDirZ, moveDistance);
                    if (canMove){
                        // can only move on the Z
                        moveDir = moveDirZ;
                    }
                    else{
                        // cannot move any direction.
                    }
                }
            }


            // Move player
            if (canMove){
                transform.position += moveDir * moveDistance;
            }

            _isWalking = moveDir != Vector3.zero;

            // Rotate player
            const float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }

        public Transform GetKitchenObjectFollowTransform(){
            return kitchenObjectHoldPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject){
            this._kitchenObject = kitchenObject;
            if (_kitchenObject != null){
                OnPickedSomething?.Invoke(this, EventArgs.Empty);
            }
        }

        public KitchenObject GetKitchenObject(){
            return _kitchenObject;
        }

        public void ClearKitchenObject(){
            _kitchenObject = null;
        }

        public bool HasKitchenObject(){
            return _kitchenObject != null;
        }
    }
}