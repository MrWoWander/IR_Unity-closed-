using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class DocRead : MonoBehaviour {
	string line;
	string file = Path.GetFullPath("Assets/Doc/test.txt");
	string directory = Path.GetFullPath("Assets/Doc");
	public List<string> fileLines;
	string doc;

	void Awake () {
		doc = Readfile (file);
		Debug.Log (doc);
	}
	void Update() {


		
	}

	string Readfile(string filename){
		string text = File.ReadAllText (filename);
		return text;
	}

}
