using UnityEngine;
using DG.Tweening;

public class SettingsButton : MonoBehaviour
{

	public GameObject settingsMenu;
	public float animationDuration = 0.5f;
	public Ease openEaseType = Ease.OutBack; // Тип кривой для открытия меню
	public Ease closeEaseType = Ease.InBack;
	public GameObject bg;

	private void Start()
	{
		settingsMenu.SetActive(false);
	}
	public void ToggleSettingsMenu(bool isOpen)
	{
		if (isOpen)
		{
			OpenSettingsMenu();
		}
		else
		{
			CloseSettingsMenu();
		}
	}

	private void OpenSettingsMenu()
	{
		bg.SetActive(true);
		settingsMenu.SetActive(true);
		settingsMenu.transform.localScale = Vector3.zero;
		settingsMenu.transform.DOScale(Vector3.one, animationDuration).SetEase(openEaseType);
	}

	private void CloseSettingsMenu()
	{
		settingsMenu.transform.DOScale(Vector3.zero, animationDuration).SetEase(closeEaseType).OnComplete(() =>
		{
			settingsMenu.SetActive(false);
			bg.SetActive(false);

		});

	}

}
