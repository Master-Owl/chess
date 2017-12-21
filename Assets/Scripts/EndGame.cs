using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	private static Player winner = null;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Text>().text = winner.ToString() + " Wins!";
		AudioSource source = gameObject.AddComponent<AudioSource>();
		source.clip = Resources.Load<AudioClip>("Sounds/jscottrakozy");
		source.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void SetWinner(Player player) {
		winner = player;
	}
}