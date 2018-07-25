using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    private void Awake()
    {
        EventManager.PlayerCharacterDeathEvent += OnPlayerCharacterDeath;
    }

    private void OnDestroy()
    {
        EventManager.PlayerCharacterDeathEvent -= OnPlayerCharacterDeath;
    }

    private void OnPlayerCharacterDeath(Transform player)
    {
        StartCoroutine(LoadScene((3)));
    }

    private IEnumerator LoadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
    }
}
