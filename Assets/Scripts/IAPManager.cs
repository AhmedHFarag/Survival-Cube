using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;

public class IAPManager : MonoBehaviour , IStoreListener
{
    public static IAPManager Instance;
    private IStoreController controller;
    private IExtensionProvider extensions;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct("100coins", ProductType.Consumable, new IDs
        {
            {"100coins", GooglePlay.Name},
            {"100coins", MacAppStore.Name}
        });
        builder.AddProduct("100coins", ProductType.Consumable, new IDs
        {
            {"200coins", GooglePlay.Name},
            {"200coins", MacAppStore.Name}
        });
        builder.AddProduct("500coins", ProductType.Consumable, new IDs
        {
            {"500coins", GooglePlay.Name},
            {"500coins", MacAppStore.Name}
        });
        builder.AddProduct("1000coins", ProductType.Consumable, new IDs
        {
            {"1000coins", GooglePlay.Name},
            {"1000coins", MacAppStore.Name}
        });
        builder.AddProduct("5000coins", ProductType.Consumable, new IDs
        {
            {"5000coins", GooglePlay.Name},
            {"5000coins", MacAppStore.Name}
        });
        builder.AddProduct("10000coins", ProductType.Consumable, new IDs
        {
            {"10000coins", GooglePlay.Name},
            {"10000coins", MacAppStore.Name}
        });
        UnityPurchasing.Initialize(this, builder);
    }

    // Example method called when the user presses a 'buy' button
    // to start the purchase process.
    public void OnPurchaseClicked(string productId)
    {
        Debug.Log("Buy");
        controller.InitiatePurchase(productId);
    }

    public void OnNoAdsPurchaseClicked()
    {
        controller.InitiatePurchase("remove_ads");
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;

        extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result => {
            if (result)
            {
                // This does not mean anything was restored,
                // merely that the restoration process succeeded.
            }
            else
            {
                // Restoration failed.
            }
        });
    }

    /// <summary>
    /// Called when Unity IAP encounters an unrecoverable initialization error.
    ///
    /// Note that this will not be called if Internet is unavailable; Unity IAP
    /// will attempt initialization until it becomes available.
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {

    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        //string name= e.purchasedProduct.metadata.localizedTitle;
        switch (e.purchasedProduct.definition.id)
        {
            case "100coins":
                DataHandler.Instance.AddCoins(100);
                break;
            case "200coins":
                DataHandler.Instance.AddCoins(200);
                break;
            case "500coins":
                DataHandler.Instance.AddCoins(500);
                break;
            case "1000coins":
                DataHandler.Instance.AddCoins(1000);
                break;
            case "5000coins":
                DataHandler.Instance.AddCoins(5000);
                break;
            case "10000coins":
                DataHandler.Instance.AddCoins(10000);
                break;

            default:
                break;
        }
        
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        if (p == PurchaseFailureReason.PurchasingUnavailable)
        {
            // IAP may be disabled in device settings.
        }
    }
}