using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public void OnFireExtinguished()
    {
        Debug.Log("Training complete!");
        // TODO: Success sound effect baad mein add karenge
    }

    public void ResetScenario()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}