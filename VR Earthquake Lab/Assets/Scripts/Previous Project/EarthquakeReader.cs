using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * This class is provided by the previous project
*/
public class EarthquakeReader {
    public static string CSVPath;
    public static float TimeStep = 0.02f;
    private static Dictionary<string,List<string>> locations;
    private static AnimationCurve[] ReadCSV(){
        AnimationCurve[] output = { new AnimationCurve(), new AnimationCurve(), new AnimationCurve() };
        #if UNITY_EDITOR
        TextReader sr = File.OpenText(Application.streamingAssetsPath+"/Earthquakes/"+ CSVPath);
        #else //Accessing compressedfiles
        WWW reader = new WWW(Application.streamingAssetsPath+"/Earthquakes/"+ CSVPath);
        while (!reader.isDone) { }
        TextReader sr = new StringReader(reader.text);
        #endif
        string line = null;
        float time = 0;
        while ((line = sr.ReadLine()) != null){
            string[] frag = line.Split(',');
            output[0].AddKey(new Keyframe(time, float.Parse(frag[0]) * 0.001f));
            output[1].AddKey(new Keyframe(time, float.Parse(frag[1]) * 0.001f));
            output[2].AddKey(new Keyframe(time, float.Parse(frag[2]) * 0.001f));
            time += TimeStep;
        }
        return output;
    }
    public static void Import(){
        GroundController.instance.Earthquake = ReadCSV();
        Player.GetInstance().Earthquake = ReadCSV();
        SoundManager.getInstance().Earthquake = ReadCSV();
    }
    public static string[] FindEarthquakes(){
        if (locations != null) { //If the database of earthquake is already built,
            return new List<string>(locations.Keys).ToArray();
        }
#if UNITY_EDITOR
        StreamingPreComp.FindFiles();
        TextReader sr = File.OpenText(Application.streamingAssetsPath + "/Earthquakes/Files.txt");
#else
        WWW reader = new WWW(Application.streamingAssetsPath + "/Earthquakes/Files.txt");
        while (!reader.isDone);
        TextReader sr = new StringReader(reader.text);
#endif
        string line;
        locations = new Dictionary<string, List<string>>();
        while ((line = sr.ReadLine()) != null){
            string[] frag = line.Split('/');
            if (!locations.ContainsKey(frag[0])){
                locations.Add(frag[0], new List<string>());
            }
            locations[frag[0]].Add(frag[1]);
        }
        return new List<string>(locations.Keys).ToArray();
    }
    public static string[] FindLocations(string Earthquake){
        return locations[Earthquake].ToArray();
    }
}
