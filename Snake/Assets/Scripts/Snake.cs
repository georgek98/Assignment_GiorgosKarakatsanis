using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
public class Snake : MonoBehaviour
{

    // Default direction if nothing is pressed
    Vector2 direction = Vector2.right;
    Color color_memory;
    int combo_counter;
    int score;
    int points;

    //Tail list
    List<Transform> tail = new List<Transform>();
    public GameObject tailPrefab;

    bool eat = false;

    public Background Background;

    // Start is called before the first frame update
    void Start()
    {
        // 200ms
        InvokeRepeating("Move", 0.2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            direction = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            direction = -Vector2.up;    
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = -Vector2.right; 
        else if (Input.GetKey(KeyCode.UpArrow))
            direction = Vector2.up;
    }

    void Move()
    {
        // Save current position because a gap will be created there
        Vector2 v = transform.position;

        // Move head into new direction
        transform.Translate(direction);

        if (eat)
        {
            // Load Tail piece 
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            tail.Insert(0, g.transform);

            eat = false;
        }
        else if (tail.Count > 0)
        {
            // move last tail part into the gap created by head moving
            tail.Last().position = v;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //Collided with food
        if (coll.name.StartsWith("foodPrefab"))
        {
            eat = true;
            var obj_color = coll.gameObject.GetComponent<SpriteRenderer>().material.color;
            Points(obj_color);
            // Remove Food

            Background.Score_update(score);
            Destroy(coll.gameObject);
        }
        // Collided with Wall or Body
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        string read_txt = File.ReadAllText("Assets/Top_score.txt");
        if (score > int.Parse(read_txt)){
            File.WriteAllText("Assets/Top_score.txt", score.ToString());
        }
        Background.Setup(score);
    }

    public void Points(Color obj_color)
    {
       if (obj_color == Color.green){
            points = 1;
        }
        else if(obj_color == Color.blue)
        {
            points = 2;
        }

       if (color_memory == obj_color)
        {
            combo_counter = combo_counter + 1;
            score = score + (points * combo_counter);
        }
        else
        {
            color_memory = obj_color;
            combo_counter = 0;
            score = score + points;
        }

    }
}
