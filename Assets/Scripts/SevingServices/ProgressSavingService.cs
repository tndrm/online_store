using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressSavingService : MonoBehaviour
{
	public void ClearProgress()
	{
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
