#addin nuget:?package=Cake.Docker&version=0.11.0

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var framework = Argument("framework", "netcoreapp3.1");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var artifactsDir = Directory("./artifacts");
var solution = "./hypomos-mvp.sln";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifactsDir);
    CleanDirectories("./src/*/bin/");
    CleanDirectories("./src/*/obj/");
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore(solution, new DotNetCoreRestoreSettings
    {
        Sources = new [] { "http://192.168.5.12/v3/index.json" }
    });
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = configuration,
        NoRestore = true,
        ArgumentCustomization = args => args
            .Append("/m:1")
    };

    var projects = GetFiles("./*/*/*.csproj");
    
    var grainProjects = projects.Where(p => p.FullPath.Contains("Grain")).ToList();
    foreach (var project in grainProjects)
    {
        DotNetCoreBuild(project.FullPath, buildSettings);
    }

    buildSettings = new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = configuration,
        NoRestore = true,
        ArgumentCustomization = args => args
            .Append("/p:DeployOnBuild=True")
            .Append("/p:PublishProfile=FolderProfile")
    };
    var nonGrainProjects = projects.Where(p => !p.FullPath.Contains("Grain")).ToList();
    foreach (var project in nonGrainProjects)
    {
        DotNetCoreBuild(project.FullPath, buildSettings);
    }
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest();
});

// Task("Publish")
//     .IsDependentOn("Test")
//     .Does(() => 
// {
//     DotNetCorePublish(solution, new DotNetCorePublishSettings
//     {
//         Framework = framework,
//         Configuration = configuration,
//         // NoBuild = true,
//         // NoRestore = true,
//         // OutputDirectory = artifactsDir
//         ArgumentCustomization = args => args
//                 .Append("-p:PublishProfile=FolderProfile")
//     });
// });

// Task("Sample")
//     .IsDependentOn("Publish")
//     .Does(() => 
// {
//     // MoveFile("./artifacts/Hypomos.Web/secrets.json.sample", "./artifacts/Hypomos.Web/secrets.json");
// });

Task("Dockerize")
    // .IsDependentOn("Sample")
    .Does(() => 
{
    var artifacts = GetDirectories("./artifacts/*");
    foreach (var artifact in artifacts)
    {
        var tags = new List<string>();
        tags.Add($"{artifact.GetDirectoryName()}:dev");

        DockerBuild(
            new DockerImageBuildSettings
            {
                Tag = tags.ToArray()
            },
            $"{artifact}");        
    }
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Test");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);