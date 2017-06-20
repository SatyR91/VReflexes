using System.Collections;
using System.Data;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Data : MonoBehaviour
{

    private DataTable dt;
    public GameController gc;
    private string[] headers;
    private bool downloadData;
    private bool dataCollected;

    // Use this for initialization
    void Start()
    {
        downloadData = false;
        dataCollected = false;
        dt = new DataTable();
        dt.Clear();
        headers = new string[] { "Type", "Hand", "Reaction Time" };
        for (int i = 0; i < headers.Length; i++)
        {
            DataColumn dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = headers[i];
            dt.Columns.Add(dtColumn);
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gc.globalCounter > 5 && !downloadData) {
            downloadData = true;
        }
        if (downloadData && !dataCollected) {
            Debug.Log("Data write");
            FillData();
            WriteDataToExcel();

            gc.VRTMean = ComputeMeanReactionTime("Visual", "button.hand[]");
            gc.ARTMean = ComputeMeanReactionTime("Audio", "button.hand[]");
            gc.ShowResultsOnScreen();

            downloadData = false;
            dataCollected = true;
        }
    }

    public DataTable getDataTable()
    {
        return dt;
    }

    public void AddNewData(string type, string hand, float reactionTime)
    {
        DataRow newData = dt.NewRow();
        newData[headers[0]] = type;
        newData[headers[1]] = hand;
        newData[headers[2]] = reactionTime;
        dt.Rows.Add(newData);
    }

    public void FillData()
    {
        List<Interactable> visualButtons = gc.VisualButtonController.buttons;
        List<Interactable> audioButtons = gc.AudioButtonController.buttons;
        for (int i = 0; i < visualButtons.Count; i++)
        {
            var button = visualButtons[i];
            for (int j = 0; j < button.reactionTimes.Count; j++)
            {
                AddNewData("Visual", button.hands[j], button.reactionTimes[j]);
            }
        }
        for(int i = 0; i < visualButtons.Count; i++)
        {
            var button = visualButtons[i];
            for (int j = 0; j < button.reactionTimesperturbations.Count; j++)
            {
                AddNewData("VisualWithPerturbation", button.handsPerturbations[j], button.reactionTimesPerturbations[j]);
            }
        }
        for (int i = 0; i < audioButtons.Count; i++)
        {
            var button = audioButtons[i];
            for (int j = 0; j < button.reactionTimes.Count; j++)
            {
                AddNewData("Audio", button.hands[j], button.reactionTimes[j]);
            }
        }
        
    }

    public void WriteDataToExcel()
    {
        var lines = new List<string>();
        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
        var header = string.Join(",", columnNames);
        lines.Add(header);

        var valueLines = dt.AsEnumerable().Select(row => string.Join(",", row.ItemArray.Where(x => x != null).Select(x => x.ToString()).ToArray()));
        lines.AddRange(valueLines);
        
        //File.WriteAllLines(@"C:\Users\etudiant\Downloads\VReflexes-dev-vive\VReflexesData.csv", lines.ToArray());
        File.WriteAllLines(@"F:\Unity\VReflexes\VReflexesData.csv", lines.ToArray());

    }

    public float ComputeMeanReactionTime(string type, string hand)
    {
        float mean = 0;
        float totalRows = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (row[headers[0]].ToString().Equals(type) && row[headers[1]].ToString().Equals(hand))
            {
                mean += float.Parse(row[headers[2]].ToString());
                totalRows++;
            }
        }
        mean /= totalRows;
        return mean;
    }
}
