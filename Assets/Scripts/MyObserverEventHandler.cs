using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObserverEventHandler : DefaultObserverEventHandler
{
    public InventoryManager inventoryManager;
    void Awake() {
        inventoryManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<InventoryManager>();
    }

    protected override void OnTrackingFound()
    {
        if (mObserverBehaviour)
        {
            var rendererComponents = mObserverBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mObserverBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mObserverBehaviour.GetComponentsInChildren<Canvas>(true);
            var cardComponents = mObserverBehaviour.GetComponentsInChildren<Card>(true);
            var crystalCompoents = mObserverBehaviour.GetComponentsInChildren<Crystal>(true);


            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;
                
            foreach (var component in crystalCompoents)
                component.enabled = true;

            foreach (var component in cardComponents)
            {
                if (inventoryManager.CountLiquid(component.originalLiquid) > 0)
                {
                    inventoryManager.RemoveLiquid(component.originalLiquid);
                    component.UpdateLiquid(component.originalLiquid);
                    component.enabled = true;
                    component.LockedForMixing = false;
                    component.used = false;
                }else{
                    foreach (var rcomponent in rendererComponents)
                        rcomponent.enabled = false;
                    foreach (var ccomponent in colliderComponents)
                        ccomponent.enabled = false;
                }

            }

            
        }

        OnTargetFound?.Invoke();
    }

    protected override void OnTrackingLost()
    {
        if (mObserverBehaviour)
        {
            var rendererComponents = mObserverBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mObserverBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mObserverBehaviour.GetComponentsInChildren<Canvas>(true);
            var cardComponents = mObserverBehaviour.GetComponentsInChildren<Card>(true);
            var crystalCompoents = mObserverBehaviour.GetComponentsInChildren<Crystal>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            foreach (var component in cardComponents)
            {
                if (component.enabled)
                {
                    component.enabled = false;
                    if(!component.used)
                        inventoryManager.AddLiquid(component.liquid);
                }
            }

            foreach (var component in crystalCompoents)
                component.enabled = false;
        }

        OnTargetLost?.Invoke();
    }
}
