using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Spawn_food : MonoBehaviour
{

    public GameObject foodPrefab; //get food prefab

    public Transform Top_border;
    public Transform Left_border;
    public Transform Bottom_border;
    public Transform Right_border;

    List<string> csv_data = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        ReadCSVFile();
        // Spawn food every 4 seconds, starting in 3
        InvokeRepeating("Spawn", 3.0f, 4.0f);
    }

    void ReadCSVFile()
    {
        StreamReader streamReader = new StreamReader("Assets/food_data.csv");
        bool endOfFile = false;
        while(!endOfFile)
        {
            string data_String = streamReader.ReadLine();
            if(data_String == null)
            {
                endOfFile = true;
                break;
            }
            //store to variable
            var data_values = data_String.Split(',');
            for (int i = 0; i <data_values.Length; i++)
            {
                csv_data.Add(data_values[i]);
            }
            
        }
    }

    // Spawn food inside the borders
    void Spawn()
    {
        //x position 
        int x = (int)Random.Range(Left_border.position.x, Right_border.position.x);

        //y position 
        int y = (int)Random.Range(Bottom_border.position.y, Top_border.position.y);

        // copy prefab at (x, y)
        GameObject newObj = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
        int list_length = csv_data.Count / 2;
        int testing = (int)Random.Range(1, list_length +1) *2;
        switch (csv_data[testing - 2])
        {
            case "Green":
                newObj.GetComponent<SpriteRenderer>().material.color = Color.green;
                break;
            case "Blue":
                newObj.GetComponent<SpriteRenderer>().material.color = Color.blue;
                break;
        }
       
    }

    void Get_points()
    {

    }
}
