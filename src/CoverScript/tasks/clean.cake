Task("Clean")
    .Does(() =>
{
    CleanDirectories(parameters.ToCleanDirs);
});