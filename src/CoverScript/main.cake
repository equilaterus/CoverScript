/// CoverScript
/// MAIN FILE


/// LOAD MISC COMPONENTS

// Load local files
#load "./enums.cake"
#load "./parameters.cake"

// Load tools
#tool "nuget:?package=OpenCover&version=4.6.519"
#tool "nuget:?package=ReportGenerator&version=2.4.5"

// Load addins
#addin nuget:?package=Cake.CoreCLR
#addin nuget:?package=Cake.Json
#addin nuget:?package=Newtonsoft.Json&version=9.0.1


/// LOAD CONFIGURATION 

// Set basic paths
var rootDir = Context.Argument("Directory", "../");
var rootPath = (DirectoryPath)Context.Directory(rootDir);
var configFile = Context.Argument("ConfigFile", "coverscript.json");
var configFilePath = rootPath.CombineWithFilePath(configFile);

// Check if coverscript configuration exists
if(!FileExists(configFilePath))
{
    throw new Exception($"Config file not found. Path: {configFile}");
}

// Load params
var parameters = DeserializeJsonFromFile<CoverScriptParameters>(
    configFilePath    
);
// Override with provided console params for the context
parameters.Prepare(Context, rootDir, rootPath);

/// LOAD TASKS 
/// For CoverScript
#load "./tasks/setup.cake"
#load "./tasks/clean.cake"
#load "./tasks/nuget.cake"
#load "./tasks/build.cake"
#load "./tasks/run.cake"

/// RUN!
RunTarget(parameters.Target);