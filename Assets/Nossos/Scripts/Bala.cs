using UnityEngine;
using System.Collections;

public class Bala : MonoBehaviour {
	public Transform explosao;
	public float rangeExplosao = 3f;
	private Vector3 posicaoAnterior;
	private GameObject explosaoInstancia;

	// Use this for initialization
	void Start () {
		explosaoInstancia = (GameObject) Network.Instantiate(explosao, transform.position, transform.rotation, 0);
	}
	
	void OnTriggerEnter(Collider other) {
		if (networkView.isMine) {
			explodir();
		}
	}
	
	void addRigidBody(GameObject ob) {
		Tijolo t = (Tijolo) ob.GetComponent("Tijolo");
		Predio p = (Predio) t.getPredioGameObject().GetComponent("Predio");
		for (int y = t.id.y; y < p.predio.GetLength(1); y++) {
			GameObject cima = p.predio[t.id.x, y, t.id.z];
			if (cima == null) {
				continue;
				// se foi destruido
			}
			if (!cima.rigidbody) {
				cima.AddComponent("Rigidbody");
				cima.rigidbody.useGravity = true;
				cima.rigidbody.mass = 1;
				cima.collider.isTrigger = false;
				p.destroyBrick(t.id.x, y, t.id.z, 10);
				p.total--;
			}
		}
		if(!ob.rigidbody) {
			ob.AddComponent("Rigidbody");
			ob.rigidbody.useGravity = true;
			ob.rigidbody.mass = 1;
			ob.collider.isTrigger = false;
			p.destroyBrick(t.id.x, t.id.y, t.id.z, 10);
			p.total--;
		}
		// destroi o pai se nao tem mais filhos
		if (p.total <= 0) {
			p.destroy(11);
		}
	}
	
	void explodir() {
		Collider[] colliders = Physics.OverlapSphere(transform.position, rangeExplosao);
		foreach (Collider col in colliders) {
			if (col.gameObject.layer == LayerMask.NameToLayer("Predio")) {
				if (!col.rigidbody) {
					addRigidBody(col.gameObject);
				}
			}
		}
		// move explosao
		explosaoInstancia.transform.position = transform.position;
		Detonator det = (Detonator) explosaoInstancia.GetComponent("Detonator");
		det.Explode();
		Network.Destroy(gameObject);
	}
}
