using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComleted;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO _recipeListSO;
    
    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;
    private void Awake()
    {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count < _waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = _recipeListSO.RecipeSOList[UnityEngine.Random.Range(0, _recipeListSO.RecipeSOList.Count)];
                
                _waitingRecipeSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];

            if (waitingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (ingredientFound == false)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    _waitingRecipeSOList.RemoveAt(i);
                    
                    OnRecipeComleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return _waitingRecipeSOList;
    }
}
