#addin "Cake.Docker"

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
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetCoreRestore(solution);
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild(solution, new DotNetCoreBuildSettings
    {
        Framework = framework,
        Configuration = configuration,
        NoRestore = true,
        ArgumentCustomization = args => 
            args.Append("/p:DeployOnBuild=True")
                .Append("/p:PublishProfile=FolderProfile")
    });
});

// Task("Tests")
//     .IsDependentOn("Build")
//     .Does(() =>
// {
//     DotNetCoreTest();
// });

// Task("Publish")
//     .IsDependentOn("Build")
//     .Does(() => 
// {
//     DotNetCorePublish(solution, new DotNetCorePublishSettings
//     {
//         Framework = framework,
//         Configuration = configuration,
//         OutputDirectory = artifactsDir
//     });
// });

Task("Sample")
    .IsDependentOn("Build")
    .Does(() => 
{
    MoveFile("./artifacts/Hypomos.Web/secrets.json.sample", "./artifacts/Hypomos.Web/secrets.json");
});

Task("Dockerize")
    // .IsDependentOn("Sample")
    .Does(() => 
{
    DockerBuild("./artifacts/Hypomos.Web/");
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);