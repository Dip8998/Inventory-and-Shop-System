using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currencyText;
    private int currentCurrency;

    public void InitializeCurrency(int initialCurrency)
    {
        currentCurrency = initialCurrency;
        UpdateCurrencyUI();
    }

    private void Awake()
    {
        UpdateCurrencyUI(); 
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        UpdateCurrencyUI();
    }

    public void RemoveCurrency(int amount)
    {
        currentCurrency -= amount;
        if (currentCurrency < 0)
        {
            currentCurrency = 0;
        }
        UpdateCurrencyUI();
    }

    public int GetCurrentCurrency()
    {
        return currentCurrency;
    }

    public bool CanAfford(int amount)
    {
        return currentCurrency >= amount;
    }

    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Gold: " + currentCurrency.ToString();
        }
        else
        {
            Debug.LogWarning("CurrencyText not assigned!");
        }
    }
}