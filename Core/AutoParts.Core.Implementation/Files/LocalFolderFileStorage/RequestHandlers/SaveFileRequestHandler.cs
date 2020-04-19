namespace AutoParts.Core.Implementation.Files.LocalFolderFileStorage.RequestHandlers
{
    using MediatR;

    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Constants;

    using Contracts.Files.Requests;

    public class SaveFileRequestHandler : IRequestHandler<SaveFileRequest, string>
    {
        public async Task<string> Handle(SaveFileRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} of type {nameof(SaveFileRequest)} argument cannot be null.");
            }

            var fileNameToSave = GetFileNameToSave(request.FileName);
            var fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileConstants.LocalFilesFolderName, fileNameToSave);
            var fullFileFolderPath = Path.GetDirectoryName(fullFilePath);

            if (!Directory.Exists(fullFileFolderPath))
            {
                Directory.CreateDirectory(fullFileFolderPath);
            }

            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await stream.WriteAsync(request.Buffer.ToArray(), 0, request.Buffer.Length)
                    .ConfigureAwait(false);
            }

            return fileNameToSave;
        }

        private string GetFileNameToSave(string originalFileName)
        {
            var fileExtension = Path.GetExtension(originalFileName);

            return $"{Guid.NewGuid()}{fileExtension}";
        }
    }
}
