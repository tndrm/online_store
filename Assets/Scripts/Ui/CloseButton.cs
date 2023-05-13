using UnityEngine;

public class CloseButton : MonoBehaviour
{
	[SerializeField] GameObject panel;

	public void ClosePanel()
	{
		panel.SetActive(false);
	}
}
