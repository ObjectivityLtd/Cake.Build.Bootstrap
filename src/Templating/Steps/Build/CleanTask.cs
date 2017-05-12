﻿using Cake.Core.IO;

namespace Cake.CD.Templating.Steps.Build
{
    public class CleanTask : IScriptTask
    {
        public string Name => "Clean " + this.SolutionName;

        public ScriptTaskType Type => ScriptTaskType.BUILD_BACKEND;

        public string SolutionName { get; }

        public CleanTask(string solutionName)
        {
            this.SolutionName = SolutionName;
        }

    }
}