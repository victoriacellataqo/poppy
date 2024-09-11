#if UNITY_EDITOR && UNITY_IOS
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEngine;

public class PostBuildProcess
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target == BuildTarget.iOS)
        {
            // Step 1: Modify the Info.plist for UnityFramework to add CFBundleShortVersionString

            // Path to the UnityFramework Info.plist file
            string plistPath = Path.Combine(pathToBuiltProject, "UnityFramework/Info.plist");
            Debug.Log("Info.plist path: " + plistPath);

            if (File.Exists(plistPath))
            {
                // Load the Info.plist file
                PlistDocument plist = new PlistDocument();
                plist.ReadFromFile(plistPath);
                Debug.Log("Info.plist loaded successfully.");

                // Get the root dictionary
                PlistElementDict rootDict = plist.root;

                // Ensure CFBundleShortVersionString is present and set correctly
                if (!rootDict.values.ContainsKey("CFBundleShortVersionString"))
                {
                    rootDict.SetString("CFBundleShortVersionString", "2.0.0");
                    Debug.Log("CFBundleShortVersionString added to Info.plist.");
                }
                else
                {
                    rootDict.SetString("CFBundleShortVersionString", "2.0.0");
                    Debug.Log("CFBundleShortVersionString updated in Info.plist.");
                }

                // Save the modified plist back to the file
                plist.WriteToFile(plistPath);
                Debug.Log("Info.plist saved successfully.");

                // Log the final contents of Info.plist to verify changes
                Debug.Log("Final Info.plist contents:\n" + File.ReadAllText(plistPath));
            }
            else
            {
                Debug.LogError("Info.plist file not found at: " + plistPath);
            }

            // Step 2: Modify Xcode project settings to disable "Generate Info.plist File" for UnityFramework target

            // Path to the Xcode project file
            string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);

            // Load the Xcode project file
            PBXProject pbxProject = new PBXProject();
            pbxProject.ReadFromFile(pbxProjectPath);

            // Get the UnityFramework target GUID
            string unityFrameworkTarget = pbxProject.GetUnityFrameworkTargetGuid();

            // Disable "Generate Info.plist File" for UnityFramework target
            pbxProject.SetBuildProperty(unityFrameworkTarget, "GENERATE_INFOPLIST_FILE", "NO");
            Debug.Log("Set Generate Info.plist File to No for UnityFramework target.");

            // Save the modified Xcode project file
            pbxProject.WriteToFile(pbxProjectPath);
            Debug.Log("Modified Xcode project settings saved successfully.");
        }
    }
}
#endif
