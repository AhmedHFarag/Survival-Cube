/*******************************************************************************
 * Copyright 2012-2014 One Platform Foundation
 *
 *       Licensed under the Apache License, Version 2.0 (the "License");
 *       you may not use this file except in compliance with the License.
 *       You may obtain a copy of the License at
 *
 *           http://www.apache.org/licenses/LICENSE-2.0
 *
 *       Unless required by applicable law or agreed to in writing, software
 *       distributed under the License is distributed on an "AS IS" BASIS,
 *       WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *       See the License for the specific language governing permissions and
 *       limitations under the License.
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnePF;

public class BundlePurchase : MonoBehaviour {
    const string SKU = "100coins";
    bool _isInitialized = false;
    public Inventory _inventory = null;
    public CanvasGroup ConfirmBox;
    public CanvasGroup MainMenu;
    // Use this for initialization
    void Start () {
        OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_GOOGLE, "100coins");
        // Application public key
        var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgEEaiFfxugLWAH4CQqXYttXlj3GI2ozlcnWlZDaO2VYkcUhbrAz368FMmw2g40zgIDfyopFqETXf0dMTDw7VH3JOXZID2ATtTfBXaU4hqTf2lSwcY9RXe/Uz0x1nf1oLAf85oWZ7uuXScR747ekzRZB4vb4afm2DsbE30ohZD/WzQ22xByX6583yYE19RdE9yJzFckEPlHuOeMgKOa4WErt11PHB6FTdT5eN96/jjjeEoYhX/NGkOWKW0Y0T0A7CdUC0D4t2xxkzAQHdgLfcRw9+/EIcaysLhncWYiCifJrRBGpqZU1IrNuehrC5FXUN99786c/TwlxNG5nflE6sWwIDAQAB";
        var yandexPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApvU8l4ONEEsSGznPN6DnjIbJnv6vEgm08nbbi+2fMc0V46N7x7jBWTWAf2K6XLZg/rLUkqbWISq12PLvt7ydcsD+Hb9ZubdN2h8LNCTohVPeDbJjd5khtF4J5FNP2/XSTc1C7cSCBTGmqH0fUr77v4x/JMpxKlSjPN6KbNnaF2BLDAdi3012lz2XX4BVgUj7LArID/vYSYGlwMzMkvhUSpvZOM/WIPN+8YDgQAFBlRGRjLhY/3Vpq/AtXtVAzzyfTOZYkwNqdXpwAq5+/51LphowUI5NEBYh8lhQeOJmPNA6EcF1h5L9cJTVLy3bkuCXcjoN2eEO1Nq0h/40G0R4pwIDAQAB";
        var slideMePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAogOQb0mMbuq4FQ4ZhWRhN8k76/gXOUE370VubZa9Up25GdptYXoRNniecUTDLyjfvWp7+YFW8iPqIp523qNXtQ0EynNhK4xNLvJCd1CjfAju6M0f+o8MOL1zV7g3dHqxICZoHwqBbQOWneDzG/DzJ22AVdLKwty0qbv8ESaCOCJe31ZnoYVMw5KNVkSuRrrhiGGh6xj7F3qZ0T5TOSp3fK7soDamQLevuU7Ndn5IQACjo92HNN0O2PR2cvEjkCRuIkNk2hnqinac984JCzCC0SC/JBnUZUAeYJ7Y8sjT+79z1T1g7yGgDesopnqORiBkeXEZHrFy7PifdA/ZX7rRwQIDAQAB";

        var options = new Options();
        options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
        options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
        options.checkInventory = false;
        options.verifyMode = OptionsVerifyMode.VERIFY_ONLY_KNOWN;
        options.storeKeys.Add(OpenIAB_Android.STORE_GOOGLE, "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmX+XCw0oeenOUxoV32ntXoAg/rqcySQuLW/bEVKjQp20YqmJJ/aQVpSB/S5LqhtF8V7j+zmwIrELHdv1fBEl3Pqj4sAeLD2fnqbDuWexHc39sDq2qORHLXROSgOuTpwkHUTPxXTPLlBdnd+YfoiKfGggy1s0rZTTWn5/5K5DvBATBcUN4dAigbKDc3twxpZWeZZowbUi+5u2Sn2bjZNyFFcItUkMVAWnPB+CB7/fyjKI5bK93il0ribLho20sxwAbCuqJcVk//57IJheWIH1TsSH0sJKxGRxA6hMaSCb2+ZI2Gt4I/31wTZLePBVyFdwf1r6mXJKN0dotiv0MWgdCQIDAQAB");
        options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_AMAZON };
        options.availableStoreNames = new string[] { OpenIAB_Android.STORE_AMAZON };
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_GOOGLE, googlePublicKey } };
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_YANDEX, yandexPublicKey } };
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_SLIDEME, slideMePublicKey } };
        options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
        
        // Transmit options and start the service
        OpenIAB.init(options);
        if (!_isInitialized)
            return;
        OpenIAB.queryInventory(new string[] { SKU });
        
    }
    private void OnEnable()
    {
        // Listen to all events for illustration purposes
        OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
    }
    private void OnDisable()
    {
        // Remove all event handlers
        OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
        OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
        OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
        OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
        OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
        OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
        OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
        OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
    }
    // Update is called once per frame
    void Update () {
		
	}
    public void Purchase(string sku)
    {
        
        OpenIAB.purchaseProduct(sku);

    }
 


    private void billingSupportedEvent()
    {
        Debug.Log("billingSupportedEvent");
    }
    private void billingNotSupportedEvent(string error)
    {
        Debug.Log("billingNotSupportedEvent: " + error);
    }
    private void queryInventorySucceededEvent(Inventory inventory)
    {
        Debug.Log("queryInventorySucceededEvent: " + inventory);
        if (inventory != null)
        {
        }
    }
    private void queryInventoryFailedEvent(string error)
    {
        Debug.Log("queryInventoryFailedEvent: " + error);
    }
    private void purchaseSucceededEvent(Purchase purchase)
    {
        Debug.Log("purchaseSucceededEvent: " + purchase);
        switch (purchase.Sku)
        {
            case SKU:
                DataHandler.Instance.AddCoins(100);
                break;
            default:
                break;
        }
    }
    private void purchaseFailedEvent(int errorCode, string errorMessage)
    {
        Debug.Log("purchaseFailedEvent: " + errorMessage);
    }
    private void consumePurchaseSucceededEvent(Purchase purchase)
    {
        Debug.Log("consumePurchaseSucceededEvent: " + purchase);
    }
    private void consumePurchaseFailedEvent(string error)
    {
        Debug.Log("consumePurchaseFailedEvent: " + error);
    }
}
