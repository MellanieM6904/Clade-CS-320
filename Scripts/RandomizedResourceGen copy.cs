using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedResourceGen : MonoBehaviour
{
    private int spawnItems = 25; //This will give us 50 items
    public int itemsPosCoordArr[spawnItems][spawnItems]; //Save these coords for further use with other methods
    public int itemsNegCoordArr[spawnItems][spawnItems]; //Save these coords for further use with other methods
    List<resourceItem> items = new List<resourceItem>();
    // Start is called before the first frame update
    void Start() {
        //Get random coords for spawn use
        var rand = new Random();
        for (int i = 0; i < spawnItems; i++) {
            resourceItem pos;
            resourceItem neg;
            int posX = rand % 100; //100 keeps it in the immediate map bounds
            int posY = rand % 100;
            int negX = -(rand % 100);
            int negY = -(rand % 100);
            itemsNegCoordArr[i] = negX;
            itemsNegCoordArr[i][i] = negY;
            itemsPosCoordArr[i] = posX;
            itemsPosCoordArr[i][i] = posY;
            pos.attr = "good";
            neg.attr = "good";
            pos.wildcard = rand;
            neg.wildcard = rand;
            pos.special = rand;
            neg.special = rand;
            pos.exists = true;
            neg.exists = true;
            pos.xVal = posX;
            pos.yVal = posY;
            neg.xVal = negX;
            neg.yVal = negY;
            items.add(pos);
            items.add(neg);
        }

        resourceItem starter;
        starter.xVal = 0;
        starter.yVal = 0;
        starter.attr = "starter"
        starter.wildcard = 0;
        starter.special = rand;
        starter.exists = true;
        items.add(starter)
    }

    // Update is called once per frame
    void Update() { //Make sure all the correct items exists and remove old ones
        for (int i = 0; i < items.Count; i++) {
            if (items[i].exists = false) {
                items[i].destroy();
            }
        }
    }

    private void destroy() {
        self.attainable = false; //set object to invisible and non-interactible
    }

    public void pickUp() { //trigger on item interaction
        self.exists = false;
    }
}
