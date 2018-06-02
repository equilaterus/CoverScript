Setup(context =>
{
    if (!DirectoryExists(parameters.OutputDirectoryPath))
    {
        CreateDirectory(parameters.OutputDirectoryPath);
    }
});