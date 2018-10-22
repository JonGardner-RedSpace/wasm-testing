using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using System.Diagnostics;

namespace PBS.Build
{
    static class BuildScripts
    {
        [MenuItem("Springroll/Run Dev _F5")]
        private static void BuildRunDebug()
        {
            BuildWebGL();
            StartProcess();
        }


        [MenuItem("Springroll/Build Dev")]
        private static void BuildDev()
        {
            BuildWebGL();
        }

        [MenuItem("Springroll/Build Deploy")]
        private static void BuildDeploy()
        {
            BuildWebGL();
        }


        private static void BuildWebGL()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.target = BuildTarget.WebGL;
            options.targetGroup = BuildTargetGroup.WebGL;
            options.locationPathName = "./Build/WebGL";
            BuildPipeline.BuildPlayer(options);

        }

        private static void StartProcess()
        {
            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            // cmd.StartInfo
        }

    }
}


