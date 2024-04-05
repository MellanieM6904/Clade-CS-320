using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHandeler : MonoBehaviour
{
    public itemHandeler itemArr = [];
    public bool isOwnedArr = [];

    public int coordArr = [];
    //These funcs will be called when a random generator function is triggered
    // Start is called before the first frame update
    void Start()
    {
        get coords from random function to specify where to spawn items
        int coords = []; //put func call here
        for (int i = 0; i < coords.length(); i++) {
            //place item in world
            itemHandeler item = new itemHandeler;
            itemArr.append(item);
            coordArr.append(coords[i]);
            isOwnedArr.append(false);
        }
    }

    public void pickUp(itemHandeler item, int coord) {
        int i = 0;
        while (coord != coordArr[i]) {
            i++;
        }

        isOwnedArr[i] = true;
        //give effect to character
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
