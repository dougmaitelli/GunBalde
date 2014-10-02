using UnityEngine;
using System.Collections;

public class Predio_RdmFactory : MonoBehaviour {
	public int xrange = 35;
	public int zrange = 35;
	public int xmin = 3;
	public int ymin = 7;
	public int zmin = 3;
	public int xmax = 5;
	public int ymax = 13;
	public int zmax = 5;
	public float predioPercent = 7f;
	public int predioMinDist = 3;
	public GameObject predio;
	
	void CriarPredios() {
		int xdist, zdist;
		xdist = 0;
		zdist = 0;
		for (int x = (-xrange + (xmax / 2) + 1); x <= xrange - (xmax / 2) - 1; x++) {
			if (xdist > 0) {
				xdist--;
				continue;
			}
			for (int z = (-zrange + (zmax / 2) + 1); z <= zrange - (zmax / 2) - 1; z++) {
				if (zdist > 0) {
					zdist--;
					continue;
				}
				// verifica distancia minima entre os predios
				//Collider[] predioPerto = Physics.OverlapSphere(new Vector3(x, 0, z), predioMinDist, LayerMask.NameToLayer("Predio"));
				//if (predioPerto.Length > 0) {
					//continue;
				//}
				//continue;
				// verifica se existira o predio
				if (Random.Range(0f, 100f) <= predioPercent) {
					// cria o predio
					int xp,yp,zp;
					xp = Random.Range(xmin, xmax + 1);
					yp = Random.Range(ymin, ymax + 1);
					zp = Random.Range(zmin, zmax + 1);
					// mantem impar
					if ((xp % 2) == 0) {xp++;}
					if ((yp % 2) == 0) {yp++;}
					if ((zp % 2) == 0) {zp++;}
					xdist = predioMinDist + xmax;
					zdist = predioMinDist + zmax;
					Vector3 pos = new Vector3(x, 0, z);
					GameObject ob = (GameObject) Network.Instantiate(predio, pos, transform.rotation, 0);
					Predio p = (Predio) ob.GetComponent("Predio");
					p.transform.parent = transform;
					p.Build(xp, yp, zp); // monta o predio
				}
			}
		}
	}
}
