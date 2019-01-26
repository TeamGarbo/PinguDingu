using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {

    private static GameObject Player;
    private static GameObject Water;

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

}
