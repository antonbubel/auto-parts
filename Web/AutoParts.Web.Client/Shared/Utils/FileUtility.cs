using Blazor.FileReader;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoParts.Web.Client.Shared.Utils
{
    public static class FileUtility
    {
        public static async Task<IReadOnlyDictionary<IFileInfo, byte[]>> ReadFilesAsync(IFileReaderRef list)
        {
            var files = new Dictionary<IFileInfo, byte[]>();

            foreach (var file in await list.EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();

                using (var fileStream = await file.OpenReadAsync())
                {
                    if (fileStream.Length != 0)
                    {
                        var buffer = new byte[fileStream.Length];

                        await fileStream.ReadAsync(buffer, 0, buffer.Length);

                        files.Add(fileInfo, buffer);
                    }
                }
            }

            return files;
        }
    }
}
