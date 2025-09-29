using Core.Application.Responses;

namespace Application.Features.Machines.Commands.UploadMachineFile;

public class UploadMachineFileDto : IResponse
{
    public string? Message { get; set; } = "Files uploaded successfully";
    public bool Success { get; set; } = true;
}
