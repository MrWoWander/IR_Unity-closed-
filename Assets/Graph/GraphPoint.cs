using System; 
using UnityEngine; 
using System.Collections; 
using System.Linq; 
using UnityEngine.UI; 

public class GraphPoint : MonoBehaviour 
{ 
[SerializeField]private bool isNow; 

public Color ActiveColor; 

private Image image; 

public bool IsNow 
{ 
get 
{ 
return isNow; 
} 
set 
{ 
if (value) 
{ 
image.color = ActiveColor; 
foreach (var graphPoint in Capabylites) 
{ 
graphPoint.Show(Color.yellow); 
} 
} 
else 
{ 
foreach(var graphPoint in Capabylites) 
{ 
graphPoint.Show(Color.white); 
} 
image.color = Color.white; 
} 
isNow = value; 
} 
} 

public GraphPoint WayTo; 
public GraphPoint[] WaysFrom; 
public GraphPoint[] Capabylites; 

// Use this for initialization 
void Start () 
{ 
image = GetComponent<Image>(); 
IsNow = isNow; 
} 

// Update is called once per frame 
void Update () { 

} 


public void InvokeSolution() 
{ 
var last = WaysFrom.FirstOrDefault(solution => solution.IsNow); 
if (last == null) return; 
last.IsNow = false; 
WayTo.IsNow = true; 
} 


public void Show(Color col) 
{ 
image.color = col; 
} 
}