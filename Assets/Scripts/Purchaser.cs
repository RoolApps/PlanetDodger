using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    private Action callbackHolder = null;
    private static Purchaser purchaser;

    public const string NoAds = "noads";
    public const string PowerfulSpaceship = "powerfulspaceship";

    public event EventHandler Initialized;

    private List<String> purchasedProducts = new List<String>();

    public IEnumerable<String> PurchasedProducts
    {
        get
        {
            return purchasedProducts;
        }
    }

    private void Start()
    {
        if (purchaser == null)
        {
            purchaser = this;
            purchaser.InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (purchaser.IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(NoAds, ProductType.NonConsumable);
        builder.AddProduct(PowerfulSpaceship, ProductType.NonConsumable);
        UnityPurchasing.Initialize(purchaser, builder);
    }


    public bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyNoAds(Action callback)
    {
        purchaser.BuyProductID(NoAds, callback);
    }

    public void BuyPowerfulSpaceship(Action callback)
    {
        purchaser.BuyProductID(PowerfulSpaceship, callback);
    }

    void BuyProductID(string productId, Action callback)
    {
        if (purchaser.IsInitialized())
        {
            if (purchaser.purchasedProducts.Contains(productId))
            {
                Debug.Log("BuyProuctID: Product already bought");
            }
            else
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {   
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    purchaser.callbackHolder = callback;
                    m_StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void RestorePurchases()
    {
        if (!purchaser.IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        purchaser.purchasedProducts.AddRange(controller.products.all.Where(product => product.hasReceipt).Select(product => product.definition.storeSpecificId));

        if(purchaser.Initialized != null)
        {
            purchaser.Initialized(this, EventArgs.Empty);
        }
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        purchaser.purchasedProducts.Add(args.purchasedProduct.definition.storeSpecificId);
        purchaser.callbackHolder();
        purchaser.callbackHolder = null;
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
        purchaser.callbackHolder = null;
    }
}