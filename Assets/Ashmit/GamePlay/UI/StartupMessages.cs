using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Image overlay

public class StartupMessages : MonoBehaviour
{
    [Header("Message Setup")]
    public TextMeshProUGUI messageText;
    public float messageDuration = 10f; // Duration each startup message stays
    [SerializeField] public string[] startupMessages;

    [Header("Overlay Setup")]
    public RawImage overlayImage;
    public Color offColor = new Color(1f, 1f, 1f, 0.2f); // Slightly transparent white
    public Color onColor = Color.white; // Fully white (you can adjust)

    private int currentStartupMessageIndex = 0;
    private bool isOverridden = false;
    private Coroutine startupCoroutine;
    private Coroutine overrideCoroutine; // New!

    void Start()
    {
        if (overlayImage != null)
        {
            overlayImage.color = offColor; // Start with OFF color
        }

        if (startupMessages.Length > 0)
        {
            startupCoroutine = StartCoroutine(ShowStartupMessages());
        }
    }

    IEnumerator ShowStartupMessages()
    {
        while (currentStartupMessageIndex < startupMessages.Length)
        {
            if (!isOverridden) // Only update if no override
            {
                UpdateMessage(startupMessages[currentStartupMessageIndex]);
                SetOverlayColor(onColor);
            }

            yield return new WaitForSeconds(messageDuration);

            if (!isOverridden)
            {
                currentStartupMessageIndex++;
            }
        }

        // Optionally hide after all startup messages
        if (!isOverridden)
        {
            messageText.gameObject.SetActive(false);
            if (overlayImage != null)
                overlayImage.color = offColor;
        }
    }

    void UpdateMessage(string newText)
    {
        if (messageText != null)
        {
            messageText.text = newText;
        }
    }

    void SetOverlayColor(Color color)
    {
        if (overlayImage != null)
        {
            overlayImage.color = color;
        }
    }

    // === Called by triggers ===
    public void OverrideMessage(string newMessage)
    {
        isOverridden = true;
        messageText.gameObject.SetActive(true);

        UpdateMessage(newMessage);
        SetOverlayColor(onColor);

        // Restart override timer, no matter if it was already running
        if (overrideCoroutine != null)
            StopCoroutine(overrideCoroutine);

        overrideCoroutine = StartCoroutine(ClearOverrideAfterTime(messageDuration));
    }

    IEnumerator ClearOverrideAfterTime(float time)
    {
        // Wait for the specified time, no matter what
        yield return new WaitForSeconds(time);

        // Clear the override state after the time has passed
        isOverridden = false;

        // Hide the message if there are no more startup messages to show
        if (startupCoroutine == null || currentStartupMessageIndex >= startupMessages.Length)
        {
            messageText.gameObject.SetActive(false);
        }

        // Reset the overlay color
        SetOverlayColor(offColor);
    }

    public void ClearOverride()
    {
        isOverridden = false;

        // Stop any ongoing override timer
        if (overrideCoroutine != null)
            StopCoroutine(overrideCoroutine);

        // Reset the overlay color
        SetOverlayColor(offColor);

        // Hide the message immediately
        messageText.gameObject.SetActive(false);
    }
}
