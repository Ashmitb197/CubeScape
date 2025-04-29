using UnityEngine;
using TMPro;
using System.Collections;

public class ObjectiveAIController : MonoBehaviour
{
    public GameObject aiPrefab;
    public Transform player;
    public Vector3 offset = new Vector3(0, 0, 2f);
    private GameObject aiInstance;
    private Coroutine hideCoroutine;
    public float displayDuration = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1)) // B button
        {
            if (!ObjectiveManager.Instance.IsAIActive())
            {
                SpawnAI();
            }
            else
            {
                UpdateObjectiveText();
            }
        }

        if (ObjectiveManager.Instance.IsAIActive() && aiInstance != null)
        {
            FollowPlayer();
        }
    }

    void SpawnAI()
    {
        Vector3 spawnPosition = player.position + player.forward * offset.z + Vector3.up * 1f;
        aiInstance = Instantiate(aiPrefab, spawnPosition, Quaternion.LookRotation(-player.forward));
        UpdateObjectiveText();

        ObjectiveManager.Instance.SetAIActive(true);

        hideCoroutine = StartCoroutine(HideAIAfterDelay());
    }

    void FollowPlayer()
    {
        aiInstance.transform.position = player.position + player.forward * offset.z + Vector3.up * 1f;
        aiInstance.transform.rotation = Quaternion.LookRotation(-player.forward);
    }

    void UpdateObjectiveText()
    {
        if (aiInstance == null) return;

        TextMeshProUGUI text = aiInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = ObjectiveManager.Instance.GetCurrentObjective();
        }
    }

    IEnumerator HideAIAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);

        if (aiInstance != null)
        {
            Destroy(aiInstance);
            ObjectiveManager.Instance.SetAIActive(false);
        }
    }
}
