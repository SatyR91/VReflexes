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

    // Use this for initialization
    void Start()
    {
        dt = new DataTable();
        dt.Clear();
        headers = new string[] { "Type", "Hand", "Reaction Time" };
        for (int i = 0; i < headers.Length; i++)
        {

        }
        dt.Columns.Add();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (gc.globalCounter > 15) {
            FillData();
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
        foreach (Interactable button in visualButtons)
        {
            for (int i = 0; i < button.reactionTimes.Count; i++)
            {
                AddNewData("Visual", button.hands[i], button.reactionTimes[i]);
            }
        }
        foreach (Interactable button in audioButtons)
        {
            for (int i = 0; i < button.reactionTimes.Count; i++)
            {
                AddNewData("Audio", button.hands[i], button.reactionTimes[i]);
            }
        }
    }

    public void WriteDataToExcel()
    {
        var lines = new List<string>();
        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToArray();
        var header = string.Join(",", columnNames);
        lines.Add(header);

        var valueLines = dt.AsEnumerable().Select(row => string.Join(",", (string[])row.ItemArray));
        lines.AddRange(valueLines);

        File.WriteAllLines("VReflexesData.csv", lines.ToArray());

    }

    public float ComputeMeanReactionTime(string type, string hand)
    {
        float mean = 0;
        float totalRows = 0;
        foreach (DataRow row in dt.Rows)
        {
            if (row[int.Parse(headers[0])].ToString().Equals(type) && row[int.Parse(headers[1])].ToString().Equals(hand))
            {
                mean += float.Parse(row[int.Parse(headers[2])].ToString());
                totalRows++;
            }
        }
        mean /= totalRows;
        return mean;
    }
}
