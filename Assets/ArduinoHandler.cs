using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoHandler{
	
	private static ArduinoHandler instance = null;		
	public static ArduinoHandler Instance{
		get{
			if(instance == null)
			{
				instance = new ArduinoHandler();
			}
			return instance;
		}
	}
	
	SerialPort stream = new SerialPort(guessPortName(), 9600);
	public float[] potValues;
	
	// Use this for initialization
	public void Start () {
		potValues = new float[2];
		stream.Open();
		stream.ReadTimeout = 400;
	}
	
	// Update is called once per frame
	public void Update () {
		
		if(stream.IsOpen && stream != null)
		{
			string[] values = stream.ReadLine().Split('&');	

			potValues[0] = int.Parse(values[0])/1023f;
			potValues[1] = int.Parse(values[1])/1023f;	
		}
	}
	
	public void Splash()
	{
		if(stream.IsOpen && stream != null)
		{
			stream.WriteLine("gameover");
		}
	}
	
	public void OnApplicationQuit ()
	{
		Debug.Log ("quitting");
		if(stream != null && stream.IsOpen)
		{
			stream.Close();
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
