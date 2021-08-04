using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEditor;
using System;

public class GVRWindow : EditorWindow
{
    private int issues = 0;
    private String Age;
    [MenuItem("Window/GVR")]
    // Start is called before the first frame update
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(GVRWindow));
    }
   
    private void OnGUI()
    {
        issues = 0;
        
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            
            if (GUILayout.Button("FIx Android Build"))
            {
                if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                {
                    EditorUserBuildSettings.SwitchActiveBuildTargetAsync(BuildTargetGroup.Android, BuildTarget.Android);

                }
            }

        }
        
        if(PlayerSettings.GetGraphicsAPIs(BuildTarget.Android)[0].ToString() == "Vulkan")
        //if(UnityEngine.Rendering.GraphicsDeviceType.Vulkan )
        {
            Debug.Log("Trapped");
            issues += 1;
            if (GUILayout.Button("Fix Graphics"))
            {
                try
                {
                    PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES2, UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 });
                }
                catch (Exception)
                {
                    
                    GUILayout.Label("Unable to Set Graphics API's");
                }
                
            }
        }
 
        if(PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android).ToString()!= "IL2CPP")
        {
            issues += 1;
            GUILayout.Label("Fix Scripting issue");
            if (GUILayout.Button("Fix issue"))
            {
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            }
        }
        if(!PlayerSettings.Android.targetArchitectures.ToString().Contains("ARMv7") || !PlayerSettings.Android.targetArchitectures.ToString().Contains("ARM64"))
        {
            issues += 1;
            GUILayout.Label("Fix Android Architecture issue");
            if (GUILayout.Button("Fix issue"))
            {
                AndroidArchitecture aac = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
                PlayerSettings.Android.targetArchitectures = aac;

            }
        }
        if(!PlayerSettings.Android.forceInternetPermission)
        {
            issues += 1;
            GUILayout.Label("Fix internet issue");
            if (GUILayout.Button("Fix issue"))
            {
                PlayerSettings.Android.forceInternetPermission = true;

            }
        }
       
      
       
        //FIx Buttons

        if(issues<=0)
        {
            GUILayout.Label("No Issues are found in this GVR Project You can build the Project",EditorStyles.wordWrappedLabel);
        }
        else
        {
            GUILayout.Label("Total Issues : " + issues.ToString(), EditorStyles.boldLabel);
        }


        //Andoid Publishing settings
        GUILayout.BeginArea(new Rect(Screen.width/2-150, Screen.height / 2 - 125, 300, 50));
        Age = GUILayout.TextArea(Age,64);
        GUILayout.EndArea();
        

        
    }

    
}
