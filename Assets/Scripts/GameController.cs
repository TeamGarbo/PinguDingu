using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {

    private static GameObject Player;
    private static GameObject Water;
    private static GameObject Igloo;
    private static GameObject[] Items;

    public static GameObject GetWater() {
        if (Water != null) {
            return Water;
        }
        else {
            Water = GameObject.Find("Water");
            return Water;
        }
    }

    public static GameObject GetPlayer() {
        if (Player != null) {
            return Player;
        }
        else {
            Player = GameObject.Find("Player");
            return Player;
        }
    }

    public static void SetPlayer(GameObject player) {
        Player = player;
    }

    public static GameObject[] GetItems() {
        if (Items != null) {
            return Items;
        }
        else {
            Items = GameObject.FindGameObjectsWithTag("Item");
            return Items;
        }        
    }

    public static bool RemoveItem(GameObject item) {
        for (int i = 0; i < Items.Length; i++) {
            if (Items[i].Equals(item)) {
                Items[i] = null;
                return true;
            }
        }
        return false;
    }

    public static GameObject GetIgloo() {
        if (Igloo != null) {
            return Igloo;
        }
        else {
            Igloo = GameObject.Find("Igloo");
            return Igloo;
        }
    }

}
