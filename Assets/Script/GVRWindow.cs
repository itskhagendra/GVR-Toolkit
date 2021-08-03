using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEditor;
public class GVRWindow : EditorWindow
{

    private static string rootPath = Directory.GetCurrentDirectory();
    private static string templatePath = rootPath+ "Assets/Plugins/Android/mainTemplate.gradle";


    private static string[] mainTemplatelist = {"implementation 'androidx.appcompat:appcompat:1.0.0'",
                                          "implementation 'androidx.constraintlayout:constraintlayout:1.1.3'",
                                          "implementation 'com.google.android.gms:play-services-vision:15.0.2'",
                                          "implementation 'com.google.android.material:material:1.0.0'",
                                          "implementation 'com.google.protobuf:protobuf-javalite:3.8.0'"
    };

    private static string[] gradleTemplate = {"android.enableJetifier=true",
                                        "android.useAndroidX=true"};
    
    

    [MenuItem("Window/GVR")]
    // Start is called before the first frame update
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GVRWindow));
    }
    private void OnGUI()
    {
        GUILayout.Label("Information ", EditorStyles.boldLabel);
        if(GUILayout.Button("Switch to Android"))
        {
            Debug.Log("Button Clicked");
            if(EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.Android);
            }
            if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 || SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2)
            {
                PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3, UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2 });
            }
            Screen.autorotateToLandscapeLeft = true;
            PlayerSettings.Android.forceInternetPermission = true;
           


        }

        if (GUILayout.Button("Create Folder"))
        {
            if (AssetDatabase.IsValidFolder("Assets/Plugins/Android"))
            {
                Debug.Log("Folder is Available");
            }
            else
            {
                Debug.Log("Folder is not Available");
            }
            /*if (!Directory.Exists(Directory.GetCurrentDirectory() + "Assets/Plugins/Android/"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "Assets/Plugins/Android/");
                Debug.Log(templatePath);
                File.WriteAllLines(templatePath, mainTemplatelist);
            }
            else
            {
                Debug.Log("Created File");
            }*/
        }


    }

    
}
