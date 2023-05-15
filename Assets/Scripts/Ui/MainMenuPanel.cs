using UnityEngine;

public class MainMenuPanel : MonoBehaviour
{
	private ProgressSavingService progressSavingService;
	private bool isSoundOn;

	private void Start()
	{
		progressSavingService = (ProgressSavingService)FindObjectOfType(typeof(ProgressSavingService));

	}
	public void ClearProgress()
	{
		progressSavingService.ClearProgress();
	}

	public void ToggleSound()
	{
		isSoundOn = !isSoundOn;
	}
}
