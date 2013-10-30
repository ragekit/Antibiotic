using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class Serial : MonoBehaviour {

	// Créer un objet pour communiquer avec la port série
	// remplacer "/dev/tty.usbmodem1411" par le nom de l'arduino
	SerialPort serial = new SerialPort(guessPortName(), 115200);

	public float[] potValues;
	
	// Use this for initialization
	public void Start () {

		potValues = new float[2];

		// ouvrir la port série
		serial.Open();
		// en cas d'erreur
		serial.ReadTimeout = 400;

	}

	
	// Update is called once per frame
	public void Update () {
		
		if (serial.IsOpen && serial != null) {
			// vérifier si on doit envoyer un message
			updateSerialOut();
			// vérifier si des messages rentrent
			updateSerialIn();
		}

	}


	void updateSerialOut() {

		// regarder si la touche majuscule est enfoncé
		bool shiftKeyState = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);

		// allumer/éteindre en fonction des touches
		if (Input.GetKeyDown(KeyCode.R)) turnOnOff("red", shiftKeyState);
		if (Input.GetKeyDown(KeyCode.B)) turnOnOff("blue", shiftKeyState);
		if (Input.GetKeyDown(KeyCode.U)) openCloseValve("red", shiftKeyState);
		if (Input.GetKeyDown(KeyCode.V)) openCloseValve("blue", shiftKeyState);


	}


	void updateSerialIn() {

		if (serial.BytesToRead == 0) return;

		// chercher le message
		string message = serial.ReadLine();
		// si le message n'est pas vide, traiter le message
		if (message != "") parseSerial(message);

	}




	// traiter le message entrant par la port série
	void parseSerial(string message) {

		// si le message démarre par la lettre "R"...
		if (message.StartsWith("R")) { // potentiomètre rouge

			// chercher le reste du message (après le "R") et transformer-le en une valeur de type int (entier)
			int angleValue = int.Parse(message.Substring(1));
			// transformer cette valeur en une rotation 360
			float percent = (angleValue / 1024.0f);
			//
			rotateObject("Rouge", percent);

		} else if (message.StartsWith("B")) { // potentiomètre bleu

			// chercher le reste du message (après le "R") et transformer-le en une valeur de type int (entier)
			int angleValue = int.Parse(message.Substring(1));
			// transformer cette valeur en une rotation 360
			float percent = (angleValue / 1024.0f);
			//
			rotateObject("Bleu", percent);

		} else if (message.StartsWith("r")) { // bouton rouge

			// chercher le reste du message (après le "R") et transformer le en une valeur de type int (entier)
			int state = int.Parse(message.Substring(1));
			
			buttonInput("Rouge", state);

		} else if (message.StartsWith("b")) { // bouton bleu

			int state = int.Parse(message.Substring(1));

			buttonInput("Bleu", state);

		}

	}


	void rotateObject(string which, float percent) {

		if (which == "Rouge") {

			potValues[0] = percent;
	

		} else if (which == "Bleu") {

			potValues[1] = percent;	

		}

	}



	void buttonInput(string which, int state) {

		if (which == "Rouge") {

			// si l'etat est off
			if (state == 0) {
				// taille normale
				Debug.Log("Button Rouge Up");
			} else {
				// grosse taille
				Debug.Log("Button Rouge Down");
			}

		} else if (which == "Bleu") {

			if (state == 0) {
				Debug.Log("Button Bleu Up");
			} else {
				Debug.Log("Button Bleu Down");
			}

		}

	}


	// ouvrir l'électrovanne
	void openValve(string name) {

		openCloseValve(name, true);

	}


	// fermer l'électrovanne
	void closeValve(string name) {

		openCloseValve(name, false);

	}


	// ouvrir/fermer
	void openCloseValve(string name, bool on) {

		// en fonction du nom à ouvrir/fermer
		if (name == "red") {
			// envoyer le bon message via la port série
			if (on) {
				serial.Write("U");
				serial.Write("P");
			} else {
				serial.Write("u");
				serial.Write("p");
			}
		} else if (name == "blue") {
			if (on) {
				serial.Write("V");
				serial.Write("P");
			} else {
				serial.Write("v");
				serial.Write("p");
			}
		}

	}


	void turnOn(string name) {

		turnOnOff(name,true);

	}


	void turnOff(string name) {

		turnOnOff(name,false);

	}


	void turnOnOff(string name, bool on) {

		// en fonction du nom à ouvrir/fermer
		if (name == "red") {
			// envoyer le bon message via la port série
			if (on) serial.Write("R");
			else serial.Write("r");
		} else if (name == "blue") {
			if (on) serial.Write("B");
			else serial.Write("b");
		}

	}


	void serialWrite(string str) {

		// vérifier que la port série est ouverte avant d'envoyer un message
		if (!serial.IsOpen) return;

		// essayer d'envoyer
		try {
			serial.Write(str);
			// Debug.Log("serial out:" + str);
		} catch (System.Exception) {
			Debug.Log("io exception");
		}

	}


	public void Splash()
	{
		if(serial.IsOpen && serial != null)
		{
			openValve("Rouge");
			closeValve("Bleu");
		}
	}
	
	public void OnApplicationQuit ()
	{
		Debug.Log ("quitting");
		if(serial != null && serial.IsOpen)
		{
			serial.Close();
		}
	}
	
	public static string guessPortName()
	{		
			switch (Application.platform)
			{
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.LinuxPlayer:
				return guessPortNameUnix();
	
			default: 
				return guessPortNameWindows();
			}
	
			//return guessPortNameUnix();
	}
	
	public static string guessPortNameWindows()
	{
		var devices = System.IO.Ports.SerialPort.GetPortNames();
		
		if (devices.Length == 0) // 
		{
			return "COM3"; // probably right 50% of the time		
		} else
			return devices[0];				
	}

	public static string guessPortNameUnix()
	{			
		var devices = System.IO.Ports.SerialPort.GetPortNames();
		
		if (devices.Length ==0) // try manual enumeration
		{
			devices = System.IO.Directory.GetFiles("/dev/");		
		}
		string dev = ""; ;			
		foreach (var d in devices)
		{				
			if (d.StartsWith("/dev/tty.usb") || d.StartsWith("/dev/ttyUSB"))
			{
				dev = d;
				Debug.Log("Guessing that arduino is device " + dev);
				break;
			}
		}		
		return dev;		
	}
}
