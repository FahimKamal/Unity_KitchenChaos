using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScripts
{
    public class StoveCounter : BaseCounter
    {
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        
        public class OnStateChangedEventArgs: EventArgs
        {
            public State state;
        }
        
        public enum State
        {
            Idle,
            Frying,
            Fried,
            Burned
        }

        [SerializeField] private List<FryingRecipeSO> _fryingRecipeSOList;
        [SerializeField] private List<BurningRecipeSO> burningRecipeSOList;

        private State _state;
        private float _fryingTimer;
        private FryingRecipeSO _fryingRecipeSO;
        private BurningRecipeSO _burningRecipeSO;
        private float _burningTimer;

        private void Start()
        {
            _state = State.Idle;
        }

        private void Update()
        {
            if (HasKitchenObject())
            {
                switch (_state)
                {
                    case State.Idle:
                        break;
                    case State.Frying:
                        _fryingTimer += Time.deltaTime;

                        if (_fryingTimer > _fryingRecipeSO.fryingTimerMax)
                        {
                            // Meat is fried.
                            Debug.Log("Meat is fried.");
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);
                            _burningTimer = 0f;
                            _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                            _state = State.Fried;
                            
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = _state});
                        }
                        break;
                    case State.Fried:
                        _burningTimer += Time.deltaTime;

                        if (_burningTimer > _burningRecipeSO.burningTimerMax)
                        {
                            // Meat is burned.
                            Debug.Log("Meat is burned.");
                            GetKitchenObject().DestroySelf();

                            KitchenObject.SpawnKitchenObject(_burningRecipeSO.output, this);
                            _state = State.Burned;
                            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = _state});
                        }
                        break;
                    case State.Burned:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private IEnumerator FryMeat(float fryingTime)
        {
            yield return new WaitForSeconds(fryingTime);
            Debug.Log("Meat is fried.");
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(_fryingRecipeSO.output, this);
        }

        public override void Interact(Player player)
        {
            if (!HasKitchenObject())
            {
                // There is no kitchenObject here.
                if (player.HasKitchenObject())
                {
                    if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                    {
                        // Player carrying something that can be cut. give it to counter.
                        player.GetKitchenObject().SetKitchenObjectParent(this);
                        _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        // StartCoroutine(FryMeat(_fryingRecipeSO.fryingTimerMax));
                        _state = State.Frying;
                        _fryingTimer = 0f;
                        
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = _state});
                    }
                    else
                    {
                        Debug.Log("This kitchenObject can't be fried.");
                    }
                }
                else
                {
                    Debug.Log("Player not carrying anything. Also counter has no object either.");
                    
                }
            }
            else
            {
                // There is a kitchenObject here.
                if (player.HasKitchenObject())
                {
                    Debug.Log("Player carrying something. Can't grab the item now.");
                }
                else
                {
                    // Player is not carrying anything. So give player the kitchenObject.
                    GetKitchenObject().SetKitchenObjectParent(player);
                    _state = State.Idle;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs{state = _state});
                }
            }
        }

        private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSo);
            return fryingRecipeSO != null;
        }


        private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSo)
        {
            var fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSo);
            return fryingRecipeSO != null ? fryingRecipeSO.output : null;
        }

        private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (var recipe in _fryingRecipeSOList)
            {
                if (recipe.input == inputKitchenObjectSo)
                {
                    return recipe;
                }
            }

            return null;
        }


        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSo)
        {
            foreach (var recipe in burningRecipeSOList)
            {
                if (recipe.input == inputKitchenObjectSo)
                {
                    return recipe;
                }
            }

            return null;

        }
    }
}