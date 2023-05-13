using UnityEngine;

public class SettingsButton : MonoBehaviour
{
	[SerializeField] GameObject menuPanel;

	public void ShowMenu()
	{
		menuPanel.SetActive(true);
	}
}
