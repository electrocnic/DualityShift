using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scoring : MonoBehaviour {

    public static List<int> killedBirbs = new List<int>(5);
    public static List<int> killedBaps = new List<int>(5);
    public static List<int> killedShrooms = new List<int>(5);
    public static List<int> deaths = new List<int>(5);
    public static List<float> timeSurvived = new List<float>(5);

    void Start() {
        killedBirbs.Add(0);
        killedBirbs.Add(0);
        killedBirbs.Add(0);
        killedShrooms.Add(0);
        killedShrooms.Add(0);
        killedShrooms.Add(0);
        deaths.Add(0);
        deaths.Add(0);
        deaths.Add(0);
        killedBaps.Add(0);
        killedBaps.Add(0);
        killedBaps.Add(0);
        timeSurvived.Add(0);
        timeSurvived.Add(0);
        timeSurvived.Add(0);
    }

    public static int getKilledShrooms() {
        if (killedShrooms.Count < (LevelController.currentLevel + 1)) {
            killedShrooms.Add(0);
            killedShrooms.Add(0);
            killedShrooms.Add(0);
            killedShrooms.Add(0);
        }

        return killedShrooms[LevelController.currentLevel];
    }
    
    public static int getKilledBirbs() {
        if (killedBirbs.Count < (LevelController.currentLevel + 1)) {
            killedBirbs.Add(0);
            killedBirbs.Add(0);
            killedBirbs.Add(0);
            killedBirbs.Add(0);
        }

        return killedBirbs[LevelController.currentLevel];
    }
    
    public static int getDeaths() {
        if (deaths.Count < (LevelController.currentLevel + 1)) {
            deaths.Add(0);
            deaths.Add(0);
            deaths.Add(0);
            deaths.Add(0);
        }

        return deaths[LevelController.currentLevel];
    }
    
    public static void incrementBirbs() {
        if (killedBirbs.Count < (LevelController.currentLevel + 1)) {
            killedBirbs.Add(0);
            killedBirbs.Add(0);
            killedBirbs.Add(0);
            killedBirbs.Add(0);
        }

        killedBirbs[LevelController.currentLevel]++;
    }
    
    public static void incrementShrooms() {
        if (killedShrooms.Count < (LevelController.currentLevel + 1)) {
            killedShrooms.Add(0);
            killedShrooms.Add(0);
            killedShrooms.Add(0);
            killedShrooms.Add(0);
        }

        killedShrooms[LevelController.currentLevel]++;
    }
    
    public static void incrementDeaths() {
        if (deaths.Count < (LevelController.currentLevel + 1)) {
            deaths.Add(0);
            deaths.Add(0);
            deaths.Add(0);
            deaths.Add(0);
        }

        deaths[LevelController.currentLevel]++;
    }
    
    public static void incrementBaps() {
        if (killedBaps.Count < (LevelController.currentLevel + 1)) {
            killedBaps.Add(0);
            killedBaps.Add(0);
            killedBaps.Add(0);
            killedBaps.Add(0);
        }

        killedBaps[LevelController.currentLevel]++;
    }
    
    public static void setTimeSurvived() {
        if (timeSurvived.Count < (LevelController.currentLevel + 1)) {
            timeSurvived.Add(0);
            timeSurvived.Add(0);
            timeSurvived.Add(0);
            timeSurvived.Add(0);
        }

        timeSurvived[LevelController.currentLevel] = Time.time;
    }
}
