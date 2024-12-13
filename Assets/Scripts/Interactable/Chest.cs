using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    public List<GameObject> itemsInChest;
    [SerializeField] float spawnRadius;
    public Sprite openChestSprite;
    public float chestAnimDuration;
    public float scaleMultiplier;

    bool isOpened;
    Vector3 originalScale;

    private void Awake()
    {
        isInteractable = true;
       originalScale = transform.localScale;
    }

    public override void Interact()
    {
        if (isOpened == false)
        {
            StartCoroutine(OpenChestRoutine());
            isOpened = true;
            isInteractable = false;
        }
    }

    IEnumerator OpenChestRoutine()
    {
        float elapsedTime = 0f;

        Vector3 targetScale = originalScale * scaleMultiplier;

        while(elapsedTime < chestAnimDuration/2)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / (chestAnimDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        sr.sprite = openChestSprite;

        elapsedTime = 0f;

        while (elapsedTime < chestAnimDuration / 2)
        {
            transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsedTime / (chestAnimDuration / 2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
        DropItems();
    }

    void DropItems()
    {
        for (int i = 0; i < itemsInChest.Count; i++)
        {
            //TODO: fix object pool manager to check all items, currently this only works cause the chest is filled with ores
            GameObject item = ObjectPoolManager.Instance.GetPoolObject(itemsInChest[i]);
            item.transform.position = transform.position;
            Vector2 itemSpawnPos = FunctionUtils.GetRandomPositionInCircle(transform.position, spawnRadius);
            item.GetComponent<ItemBounce>().StartBounce(itemSpawnPos);
        }
    }

    //TODO: function to add generate items within the chest
}

