#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor;
using System.IO;

/*
 * Provided by the previous project. 
 */
public class StreamingPreComp : IPreprocessBuild{ //Obsolete in Unity 2018.2 Use IPreprocessBuildWithReport 
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildTarget target, string path){
        FindFiles();
    }
    public static void FindFiles() {
        FileInfo[] pathnames = new DirectoryInfo("Assets/StreamingAssets/Earthquakes").GetFiles("*.csv", SearchOption.AllDirectories);
        StreamWriter writer = new StreamWriter("Assets/StreamingAssets/Earthquakes/" + "Files.txt", false);
        foreach (FileInfo file in pathnames) {
            writer.WriteLine(file.Directory.Name + "/" + file.Name.Split('.')[0]);
        }
        writer.Close();
    }
}
#endif