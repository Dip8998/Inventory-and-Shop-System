using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickInvoker : MonoBehaviour
{
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(InvokeClickEvent);
        }
        else
        {
            Debug.LogError("ButtonClickInvoker script requires a Button component.");
            enabled = false;
        }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(InvokeClickEvent);
        }
    }

    void InvokeClickEvent()
    {
        EventService.Instance.OnButtonClickedEvent.InvokeEvent();
    }
}