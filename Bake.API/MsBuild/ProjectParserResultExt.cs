﻿using Cake.Common.Solution.Project;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Bake.API.MsBuild
{
    public static class ProjectParserResultExt
    {
        private const string XmlNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";
        private const string Project = "{" + XmlNamespace + "}Project";
        private const string PropertyGroup = "{" + XmlNamespace + "}PropertyGroup";
        private const string ProjectTypeGuids = "{" + XmlNamespace + "}ProjectTypeGuids";
        private const string TestProjectType = "{" + XmlNamespace + "}TestProjectType";

        public static bool IsWebApplication(this ProjectParserResult projectParserResult, string projectPath)
        {
            return GetProjectTypeGuids(projectPath).Any(MsBuildGuids.IsWebApplication);
        }

        public static bool IsExecutableApplication(this ProjectParserResult projectParserResult)
        {
            return StringComparer.OrdinalIgnoreCase.Equals(projectParserResult.OutputType, "EXE");
        }

        public static bool IsTestProjectType(this ProjectParserResult projectParserResult, string projectPath)
        {
            return GetProjectTypeGuids(projectPath).Any(MsBuildGuids.IsTest);
        }

        public static bool IsMsTestProject(this ProjectParserResult projectParserResult, string projectPath)
        {
            XDocument document = XDocument.Load(projectPath);
            var projectType =
                (from project in document.Elements(Project)
                    from propertyGroup in project.Elements(PropertyGroup)
                    select propertyGroup.Elements(TestProjectType).Select(testProjectType => testProjectType.Value)
                        .FirstOrDefault())
                .FirstOrDefault();
            return projectType == "UnitTest";
        }

        private static IEnumerable<string> GetProjectTypeGuids(string projectPath)
        {
            XDocument document = XDocument.Load(projectPath);

            var guidsAsString =
                (from project in document.Elements(Project)
                    from propertyGroup in project.Elements(PropertyGroup)
                    select propertyGroup.Elements(ProjectTypeGuids).Select(projectTypeGuids => projectTypeGuids.Value)
                        .FirstOrDefault())
                .FirstOrDefault();

            return guidsAsString == null ? new List<string>() : guidsAsString.Split(';').ToList();
                 
        }

    }
}
