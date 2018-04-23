using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Orders : MonoBehaviour
{
    public GameObject orderStack;
    public Font font;
    public float oderSpawnRate = 4f;
    public int suffixLevel; //equal suffix word length

    private float elapsed = 0;
    private float orderH = 0.05f;
    private List<string> swords;
    private Dictionary<int, List<string>> suffixes;
    private ArrayList orders;

    class Order
    {
        public string text;
        public RectTransform guiPos;

        public Order(string text, RectTransform guiPos)
        {
            this.text = text;
            this.guiPos = guiPos;
        }
    }

    // Use this for initialization
    void Start()
    {
        orders = new ArrayList();
        suffixes = new Dictionary<int, List<string>>();
        swords = transform.root.Find("Typing").GetComponent<WordsUsed>().swords;
        List<string> suffixList = transform.root.Find("Typing").GetComponent<WordsUsed>().suffixs;

        foreach (string text in suffixList)
        {
            if (suffixes.ContainsKey(text.Length))
            {
                suffixes[text.Length].Add(text);
            }
            else
            {
                suffixes.Add(text.Length, new List<string>());
            }
        }

        /*foreach (KeyValuePair<int, List<string>> t in suffixes)
        {
            Debug.Log("Suffix Length"+t.Key);
            foreach(string text in t.Value)
            {
                Debug.Log(text);
            }
        }*/

        if (swords == null)
        {
            throw new MissingComponentException("Sword List not found. (File reading?)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= oderSpawnRate)
        {
            elapsed -= oderSpawnRate;
            GameObject nextOrder = new GameObject("newOrder" + orders.Count);
            Text text = nextOrder.AddComponent<Text>();

            int rngIndex = Random.Range(0, swords.Count);
            string swordText = swords[rngIndex];
            text.text = swordText;
            if (suffixLevel > 4)
            {
                text.text += "+" + (suffixLevel - 4);
            }

            //Text settings
            text.font = font;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontSize = 20;

            nextOrder.transform.SetParent(orderStack.transform);
            RectTransform rectTransform = nextOrder.GetComponent<RectTransform>();
            orders.Add(new Order(swordText, rectTransform));

            //Rect transform positions... (head ache)
            //Anchor 1 - 0 range
            //AnchorX 1 and 0 fixed. (full with of orders canvas)
            //AnchorY 0.1 interval 0 all way down
            int orderLength = orders.Count;

            rectTransform.anchorMax = new Vector2(1, orderH * orderLength);
            rectTransform.anchorMin = new Vector2(0, (orderH * orderLength) - orderH);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            string t = ((Order)orders[0]).text;
            FinishedOrder(t);
            PrintArray();
        }*/
    }

    public void FinishedOrder(string sword)
    {
        string[] stringSplit = sword.Split(' ');
        if (stringSplit.Length > 1)
        {
            sword = stringSplit[stringSplit.Length - 1];
        }

        for (int i = 0; i < orders.Count; i++)
        {
            if (((Order)orders[i]).text.Equals(sword))
            {
                Destroy(((Order)orders[i]).guiPos.transform.gameObject);
                orders.Remove(orders[i]);

                MoveGuiDown(i);
                break;
            }
        }
    }

    //Float this value for cooler look? Ease inout?
    private void MoveGuiDown(int index)
    {
        for (int k = index; k < orders.Count; k++)
        {
            RectTransform orderRect = ((Order)orders[k]).guiPos;
            Vector2 newMax = orderRect.anchorMax;
            newMax.y -= orderH;
            orderRect.anchorMax = newMax;
            Vector2 newMin = orderRect.anchorMin;
            newMin.y -= orderH;
            orderRect.anchorMin = newMin;
        }
    }

    private void PrintArray()
    {
        foreach (var e in orders)
        {
            Debug.Log(((Order)e).guiPos.transform.gameObject.name);
        }
    }

    public string GetNextOrder()
    {
        if (orders.Count > 0)
        {
            if (suffixLevel <= 4)
            {
                return ((Order)orders[0]).text;
            }
            else
            {
                List<string> suffixGroup = suffixes[suffixLevel];
                int suffixRng = Random.Range(0, suffixGroup.Count);
                string suffix = suffixGroup[suffixRng];
                return suffix + " " + ((Order)orders[0]).text;
            }
        }
        else
        {
            return "";
        }
    }
}
