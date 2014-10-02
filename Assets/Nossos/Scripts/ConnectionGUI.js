var remoteIP = "127.0.0.1";
var remotePort = 25000;
var listenPort = 25000;
var useNAT = false;
var yourIP = "";
var yourPort = "";
var player : GameObject;

function OnGUI () {
	if (Network.peerType == NetworkPeerType.Disconnected) {
		if (GUI.Button(new Rect(10,10,100,30),"Connect")) {
			Network.Connect(remoteIP, remotePort);
		}

		if (GUI.Button(new Rect(10,50,100,30),"Start Server")) {
			Network.InitializeServer(32, listenPort, useNAT);

			for (var go : GameObject in FindObjectsOfType(GameObject)) {
				go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver); 
			}
		}

		remoteIP = GUI.TextField(new Rect(120,10,100,20),remoteIP);
		remotePort = parseInt(GUI.TextField(new Rect(230,10,40,20),remotePort.ToString()));
	} else {
		ipaddress = Network.player.ipAddress;
		port = Network.player.port.ToString();
  
  		GUI.Label(new Rect(140,20,250,40),"IP Adress: "+ipaddress+":"+port);
  		
  		if (GUI.Button (new Rect(10,10,100,50),"Disconnect")) {
			Network.Disconnect(200);
		}
	}
}

function OnConnectedToServer () {
	for (var go : GameObject in FindObjectsOfType(GameObject)) {
		go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
	}
	
	var pos2 = new Vector3(50, 0, 0);
	var ob2 = Network.Instantiate(player, pos2, new Quaternion(0, 0, 0, 0), 0);
	var player2 = ob2.GetComponent("Player_Script");
	player2.tag = "Player2";
	player2.setNum(2);
}

function OnPlayerConnected    () {
	var ob = GameObject.FindWithTag("RdmFactory");
	var pd = ob.GetComponent("Predio_RdmFactory");
	pd.CriarPredios();
	
	var pos1 = new Vector3(-50, 0, 0);
	var ob1 = Network.Instantiate(player, pos1, new Quaternion(0, 0, 0, 0), 0);
	var player1 = ob1.GetComponent("Player_Script");
	player1.tag = "Player1";
	player1.setNum(1);
}