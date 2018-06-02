#load "./enums.cake"

public class CoverScriptParameters
{
    // Directories
    public string RootDirectory { get; set;}            = "../";
    public DirectoryPath DirectoryPath { get; set; }    = null;

    // General params
    public string Target { get; set; }                  = "Run-Tests";
    public string Configuration { get; set; }           = "Release";
    public TestEnviroment TestEnviroment { get; set; }  = TestEnviroment.DotNetCoreTests;
    public string TargetFramework { get; set; }         = "netcoreapp2.0";

    // Custom params for the project
    public string Solution { get; set; }                = null;
    public FilePath SolutionPath { get;set; }
    public string SrcFolder { get; set; }               = "src";
    public DirectoryPath SrcPath { get; set; }          = null;
    public string TestFolder { get; set; }              = "test";
    public DirectoryPath TestPath { get; set; }         = null;

    // OpenCover params
    public string Filter { get; set; }                  = "+[*]* -[*.Tests*]*";
    public string OutputFolder { get; set; }            = null;
    public DirectoryPath OutputDirectoryPath { get; set; }
    public FilePath TestCoverOutput { get; set; }

    // Path params
    public List<FilePath> TestTargetDlls { get; set; } 
    public List<DirectoryPath> ToCleanDirs { get; set; }
    public List<FilePath> TestProjects { get; set; }

    
    public void Prepare(ICakeContext context, string rootDirectory, DirectoryPath directoryPath)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (rootDirectory == null)
        {
            throw new ArgumentNullException(nameof(rootDirectory));
        }
        if (directoryPath == null)
        {
            throw new ArgumentNullException(nameof(directoryPath));
        }

        RootDirectory = rootDirectory;
        DirectoryPath = directoryPath;

        Target = context.Argument("target", Target);
        Configuration = context.Argument("configuration", Configuration);
        TestEnviroment = (TestEnviroment)context.Argument("testEnviroment", TestEnviroment);
        TargetFramework = context.Argument("targetFramework", TargetFramework);

        SrcPath = directoryPath.Combine(SrcFolder);
        TestPath = directoryPath.Combine(TestFolder);
        SolutionPath = directoryPath.CombineWithFilePath(Solution);

        if (OutputFolder == null)
        {
            OutputFolder = Solution;            
        }
        OutputDirectoryPath = 
            ((DirectoryPath)(context
            .Directory("../")))
            .Combine("artifacts")
            .Combine("CoverScript")
            .Combine(OutputFolder);

        TestCoverOutput =
            OutputDirectoryPath.CombineWithFilePath("OpenCover.xml");

        var targetProjects = context.GetFiles(context.MakeAbsolute(SrcPath) + "/**/*.csproj");
        TestTargetDlls = 
			targetProjects
            .Select(file => file.GetDirectory()
                            .Combine("bin")
                            .Combine(Configuration)
                            .Combine(TargetFramework)
                            .CombineWithFilePath(file.GetDirectory().GetDirectoryName() + ".dll"))
            .ToList();

        var testProjects = context.GetFiles(context.MakeAbsolute(TestPath) + "/**/*.csproj");
        TestProjects = testProjects.ToList();


        var cleanBins = 
            targetProjects
                .Select(file => file.GetDirectory()
                                .Combine("bin"))
                .ToList();

        var cleanObjs =
            targetProjects
                .Select(file => file.GetDirectory()
                                .Combine("obj"))
                .ToList();

        ToCleanDirs = cleanBins;
        ToCleanDirs.AddRange(cleanObjs);
    }    
}