using UnityEngine;
using UnityEngine.UI;


public class MainMenuPanel : MonoBehaviour
{
	private ProgressSavingService progressSavingService;
	public Sprite soundOnSprite;
	public Sprite soundOffSprite;
	public Image soundImage;
	private bool isSoundOn = true;

	private void Start()
	{
		UpdateSoundSprite();

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
		UpdateSoundSprite();
	}
	private void UpdateSoundSprite()
	{
		soundImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
	}
}
