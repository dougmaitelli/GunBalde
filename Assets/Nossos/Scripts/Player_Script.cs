using UnityEngine;
using System.Collections;

public class Player_Script: MonoBehaviour {
	
	private int num = 0;
	public int life = 100;
	public int force = 100;
	private float extraForce = 0;
	private float reload = 100f;
	private Transform canhao;
	private Transform cano;
	public Transform bala;
	public int posCamera = 0;

	// Use this for initialization
	void Start () {
		canhao = transform.FindChild("Canhao");
		cano = canhao.FindChild("mesh");
		Physics.gravity = new Vector3(-30, -60, 0);
		
		Camera main = GameObject.FindWithTag("MainCamera").camera;
		Camera canCam = canhao.FindChild("Camera").camera;
		
		canCam.enabled = false;
		
		if (networkView.isMine) {
			main.enabled = false;
			canCam.enabled = true;
		}
	}
	
	void TrocarCamera() {
		Camera main = GameObject.FindWithTag("MainCamera").camera;
		Camera canCam = canhao.FindChild("Camera").camera;
		main.enabled = posCamera == 1;
		canCam.enabled = posCamera == 0;
		posCamera++;
		if (posCamera > 1) {
			posCamera = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (networkView.isMine) {
			if (Input.GetButton("Vertical")) {
				GameObject go = new GameObject();
				go.transform.rotation = canhao.rotation;
				Vector3 soma = new Vector3(50, 0, 0) * Input.GetAxis("Vertical") * Time.deltaTime;
				go.transform.Rotate(soma);
				// limite vertical
				if (go.transform.rotation.eulerAngles.x < 345) {
					canhao.Rotate(soma);
				} else {
					canhao.rotation = Quaternion.Euler(new Vector3(345, go.transform.rotation.eulerAngles.y, go.transform.rotation.eulerAngles.z));
				}
				Destroy(go);
			}
			
			if (Input.GetButton("Horizontal")) {
				canhao.Rotate(new Vector3(0, 50, 0) * Input.GetAxis("Horizontal") * Time.deltaTime, Space.World);
			}
			
			if (Input.GetButtonUp("Camera")) {
				TrocarCamera();
			}
			
			if (reload == 100) {
				if (Input.GetButton("Jump")) {
					if (extraForce < 100) {
						extraForce += 100f * Time.deltaTime;
						if (extraForce > 100f) {
							extraForce = 100f;
						}
					}
				}
				
				if (Input.GetButtonUp("Jump")) {
					if (num == 1) {
						Shoot(extraForce);
					} else {
						Debug.Log(cano.position);
						Debug.Log(canhao.rotation);
						networkView.RPC("Shoot", RPCMode.Server, extraForce);
					}
					
					extraForce = 0;
					reload = 0;
				}
			}
			
			if (reload < 100) {
				reload = reload + Time.deltaTime * 50;
				
				if (reload > 100) {
					reload = 100;
				}
			}
		}
	}
	
	[RPC]
	private void Shoot(float eF) {
		ParticleEmitter fire = (ParticleEmitter) canhao.FindChild("fire").GetComponent(typeof(ParticleEmitter));
		fire.Emit();
		
		ParticleEmitter smoke = (ParticleEmitter) canhao.FindChild("smoke").GetComponent(typeof(ParticleEmitter));
		smoke.Emit();
		
		Debug.Log(cano.position);
		Debug.Log(canhao.rotation);
		
		Transform np = (Transform) Network.Instantiate(bala, cano.position, canhao.rotation, 0);
		np.rigidbody.AddForce(cano.up * force * eF / 100, ForceMode.Impulse);
		
		Destroy(np.gameObject, 5);
	}
	
	public int getNum() {
		return num;
	}
	
	public void setNum(int n) {
		num = n;
	}
	
	public int getLife() {
		return life;
	}
	
	public void setLife(int l) {
		life = l;
	}
	
	public int getForce() {
		return force;
	}
	
	public void setForce(int f) {
		force = f;
	}
	
	public float getExtraForce() {
		return extraForce;
	}
}
