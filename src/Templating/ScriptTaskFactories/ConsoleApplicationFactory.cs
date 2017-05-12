﻿using Cake.CD.Templating.Steps.Build;
using System.Collections.Generic;

namespace Cake.CD.Templating.ScriptTaskFactories
{
    public class ConsoleApplicationFactory : AbstractScriptTaskFactory
    {
        public override bool IsApplicable(ProjectInfo projectInfo)
        {
            return projectInfo.IsExecutableApplication();
        }

        public override IEnumerable<IScriptTask> Create(ProjectInfo projectInfo)
        {
            return new List<IScriptTask>
            {
                new MsBuildTask(MsBuildTask.MsBuildTaskType.ConsoleApplication, projectInfo.Project.Path, projectInfo.Project.Name)
            };
        }
    }
}
