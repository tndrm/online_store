using UnityEngine;
using UnityEngine.UI;


public class MainMenuPanel : MonoBehaviour
{
	private ProgressSavingService progressSavingService;
	private bool isSoundOn = true;

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
		AudioListener.volume = isSoundOn ? 1 : 0;
	}
}
