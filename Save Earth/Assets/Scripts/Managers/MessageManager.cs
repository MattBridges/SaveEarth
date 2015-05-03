using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

	public List<string> messageQueue;
	public int current;
	private Text UIText;
	private Image UIImage;
	public List<string> sendMessages;

	// Use this for initialization
	void Start () {
		messageQueue = new List<string>();
		UIText = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
		UIImage = this.gameObject.GetComponent<Image>();
	}

	public void InsertMessages(List<string> messages)
	{
		messageQueue.Clear();
		messageQueue.AddRange(messages);
		current = 0;
	}

	public void nextMessage()
	{
		if ((current + 1) >= messageQueue.Count) 
		{
			UIText.text = "";
			UIImage.enabled = false;
		}
		else 
		{
			UIText.text = messageQueue[++current];
		}
	}

	public void EnableMessage()
	{
		UIImage.enabled = true;
		UIText.text = messageQueue[0];
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.P)) {
			UIImage.enabled = true;
			InsertMessages(sendMessages);
			UIText.text = messageQueue[0];
		}	
	}
}