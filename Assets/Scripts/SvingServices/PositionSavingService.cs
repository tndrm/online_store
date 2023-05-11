using UnityEngine;
public class PositionSavingService : MonoBehaviour
{
	private float saveInterval = 10.0f; 
	private float timeSinceLastSave = 0.0f; 
	private Vector3 lastSavedPosition; 

	private void Start()
	{
		if (PlayerPrefs.HasKey("LastSavedPositionX") &&
			PlayerPrefs.HasKey("LastSavedPositionZ"))
		{
			float x = PlayerPrefs.GetFloat("LastSavedPositionX");
			float z = PlayerPrefs.GetFloat("LastSavedPositionZ");
			transform.position = new Vector3(x, transform.position.y, z);
		}
	}

	private void Update()
	{
		timeSinceLastSave += Time.deltaTime;

		if (timeSinceLastSave >= saveInterval)
		{
			SavePos(transform.position);
			lastSavedPosition = transform.position;
			timeSinceLastSave = 0.0f;
		}
	}

	private void OnDestroy()
	{
		SavePos(lastSavedPosition);
	}

	private void SavePos(Vector3 position)
	{
		PlayerPrefs.SetFloat("LastSavedPositionX", position.x);
		PlayerPrefs.SetFloat("LastSavedPositionZ", position.z);
		PlayerPrefs.Save();
	}
}
