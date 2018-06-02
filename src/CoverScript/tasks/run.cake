Task("Run-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var success = true;
    var openCoverSettings = new OpenCoverSettings
    {
        OldStyle = true,
        MergeOutput = true
    }
    .WithFilter(parameters.Filter);    
    
    foreach (var project in parameters.TestProjects)
    {
        try 
        {
            var projectFile = MakeAbsolute(project).ToString();
            var dotNetTestSettings = new DotNetCoreTestSettings
            {
                Configuration = parameters.Configuration,
                NoBuild = true,
            };

            OpenCover(context => context.DotNetCoreTest(projectFile, dotNetTestSettings), parameters.TestCoverOutput, openCoverSettings);
        }
        catch(Exception ex)
        {
            success = false;
            Error("There was an error while running the tests", ex);
        }
    }    

    ReportGenerator(parameters.TestCoverOutput, parameters.OutputDirectoryPath);

    if(success == false)
    {
        throw new CakeException("There was an error while running the tests");
    }
});