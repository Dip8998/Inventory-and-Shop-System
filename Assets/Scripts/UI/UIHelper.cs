using UnityEngine;

public static class UIHelper
{
    public static void ClearContainerChildren(Transform container)
    {
        foreach (Transform child in container)
        {
            Object.Destroy(child.gameObject);
        }
    }
}