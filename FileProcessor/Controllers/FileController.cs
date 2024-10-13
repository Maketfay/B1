using System.Net;
using FileProcessor.Logic.Services.Contracts;
using FileProcessor.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FileProcessor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileGenerator _fileGenerator;
    private readonly IFileMerger _fileMerger;
    private readonly IFileImporter _fileImporter;

    public FileController(IFileGenerator fileGenerator, IFileMerger fileMerger, IFileImporter fileImporter)
    {
        _fileGenerator = fileGenerator;
        _fileMerger = fileMerger;
        _fileImporter = fileImporter;
    }
    
    [HttpPost("generate")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GenerateFiles(int fileCount, CancellationToken ct)
    {
        var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedFiles");
        Directory.CreateDirectory(outputPath);
        
        await _fileGenerator.GenerateFilesAsync(fileCount, outputPath, ct);
        return Ok();
    }
    
    [HttpPost("merge")]
    [SwaggerResponse((int)HttpStatusCode.OK, null, typeof(MergeFileResponse))]
    public async Task<IActionResult> MergeFiles(MergeFileRequest request, CancellationToken ct)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedFiles");
        var outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), "MergedFile.txt");
        
        var removedCount = await _fileMerger.MergeFilesAsync(directoryPath, outputFilePath, request.SubstringToRemove, ct);
        
        return Ok(new MergeFileResponse
        {
            RemovedCount = removedCount,
        });
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportFiles(CancellationToken ct)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MergedFile.txt");
        
        await _fileImporter.ImportFileAsync(filePath, ct);
        
        return Ok();
    }
}