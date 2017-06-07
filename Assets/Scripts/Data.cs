using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {


  private DataTable dt;
  private string[] headers;

	// Use this for initialization
	void Start () {
		dt = new DataTable();
    dt.Clear();
    headers = new string[] {"Type", "Hand", "Reaction Time"};
    dt.Columns.Add(headers);
  }

	// Update is called once per frame
	void Update () {

	}

  public void addNewData(string type, string hand, float reactionTime){
    DataRow newData = dt.NewRow();
    newData[headers[0]] = type;
    newData[headers[1]] = hand;
    newData[headers[2]] = reactionTime.ToString();
    dt.Rows.Add(newData);
  }
