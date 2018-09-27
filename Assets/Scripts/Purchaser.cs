using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    private Dictionary<String, Action> callbacksHolder = new Dictionary<String, Action>();

    public const string NoAds = "noads";
    public const string PowerfulSpaceship = "powerfulspaceship";

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
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(NoAds, ProductType.NonConsumable);
        builder.AddProduct(PowerfulSpaceship, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyNoAds(Action callback)
    {
        BuyProductID(NoAds, callback);
    }

    public void BuyPowerfulSpaceship(Action callback)
    {
        BuyProductID(PowerfulSpaceship, callback);
    }

    void BuyProductID(string productId, Action callback)
    {
        if (IsInitialized())
        {
            if (purchasedProducts.Contains(productId))
            {
                Debug.Log("BuyProuctID: Product already bought");
            }
            else
            {
                Product product = m_StoreController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    callbacksHolder[productId] = callback;
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
        if (!IsInitialized())
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
        purchasedProducts.AddRange(controller.products.all.Where(product => product.hasReceipt).Select(product => product.definition.storeSpecificId));

        if (purchasedProducts.Contains(NoAds))
        {
            GameSettings.DisableAds();
        }
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        purchasedProducts.Add(args.purchasedProduct.definition.storeSpecificId);
        callbacksHolder[args.purchasedProduct.definition.storeSpecificId]();
        callbacksHolder.Remove(args.purchasedProduct.definition.storeSpecificId);
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        callbacksHolder.Remove(product.definition.storeSpecificId);
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}