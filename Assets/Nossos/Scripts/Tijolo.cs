using UnityEngine;
using System.Collections;

public class Tijolo : MonoBehaviour {
	private GameObject predio;
	public Posicao id;
	
	public GameObject getPredioGameObject() {
		return predio;
	}
	
	[RPC]
	public void setPredioGameObject(GameObject p) {
		predio = p;
	}
	
	public void setRemotePredioGameObject(GameObject p) {
		networkView.RPC("setPredioGameObject", RPCMode.All, p);
	}
}
