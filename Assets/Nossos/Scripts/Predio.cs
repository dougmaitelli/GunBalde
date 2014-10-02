using UnityEngine;
using System.Collections;

public class Predio : MonoBehaviour {
	public GameObject tijolo;
	public GameObject janela;
	public int x;
	public int y;
	public int z;
	public GameObject[,,] predio;
	public int total;
	
	void Start() {
	}

	// Use this for initialization
	public void Build (int xi, int yi, int zi) {
		x = xi;
		y = yi;
		z = zi;
		total = x * y * z;
		// forca valor de x e y impares
		if ((x % 2) == 0) {
			x++;
		}
		if ((y % 2) == 0) {
			y++;
		}
		if ((z % 2) == 0) {
			z++;
		}
		predio = new GameObject[x,y,z];
		for (int lx = 0; lx < x; lx++) {
			for (int ly = 0; ly < y; ly++) {
				for (int lz = 0; lz < z ; lz++) {
					// calcula posicao
					Vector3 pos = new Vector3(transform.position.x + lx - (x * 0.5f), transform.position.y + ly + 1f, transform.position.z + lz - (z * 0.5f));
					GameObject ob;
					GameObject cuboCriar;
					// se forem as bordas
					if (((ly % 2) == 1) && ((lx % 2) == 1) && ((lz == 0) || (lz == z - 1))) {
						cuboCriar = janela;
						//continue;
					} else {
						// se forem as bordas em z
						if (((ly % 2) == 1) && ((lz % 2) == 1) && ((lx == 0) || (lx == x - 1))) {
							cuboCriar = janela;
							//continue;
						} else {
							cuboCriar = tijolo;
						}
					}
					//cuboCriar = tijolo;
					ob = (GameObject) Network.Instantiate(cuboCriar, pos, transform.rotation, 0);
					predio[lx,ly,lz] = ob;
					Tijolo t = (Tijolo) ob.GetComponent("Tijolo");
					t.setPredioGameObject(gameObject);
					//t.setRemotePredioGameObject(gameObject);
					t.id = new Posicao(lx, ly, lz);
				}
			}
		}
	}
	
	public void destroyBrick(int x, int y, int z, float t) {
		Destroy(predio[x, y, z], t);
		networkView.RPC("remoteDestroyBrick", RPCMode.Others, x, y, z, t);
	}
	
	[RPC]
	public void remoteDestroyBrick(int x, int y, int z, float t) {
		Destroy(predio[x, y, z], t);
	}
	
	public void destroy(float t) {
		Destroy(gameObject, t);
		networkView.RPC("destroyRemote", RPCMode.Others, t);
	}
	
	[RPC]
	public void destroyRemote(float t) {
		Destroy(gameObject, t);
	}
}
