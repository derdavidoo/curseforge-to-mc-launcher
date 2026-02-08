using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurseForgeToMcLauncher.Services
{

    public static class FileCopyService
    {
        public static async Task CopyDirectoryMergeAsync(
            string sourceDirectory,
            string targetDirectory,
            CancellationToken cancellationToken = default)
        {
            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException(sourceDirectory);

            Directory.CreateDirectory(targetDirectory);

            foreach (var sourceFile in Directory.GetFiles(sourceDirectory))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var fileName = Path.GetFileName(sourceFile);
                var targetFile = Path.Combine(targetDirectory, fileName);

                if (File.Exists(targetFile))
                    continue;

                await CopyFileAsync(sourceFile, targetFile, cancellationToken);
            }

            foreach (var sourceSubDir in Directory.GetDirectories(sourceDirectory))
            {
                cancellationToken.ThrowIfCancellationRequested();

                var dirName = Path.GetFileName(sourceSubDir);
                var targetSubDir = Path.Combine(targetDirectory, dirName);

                await CopyDirectoryMergeAsync(
                    sourceSubDir,
                    targetSubDir,
                    cancellationToken);
            }
        }

        private static async Task CopyFileAsync(
            string sourceFile,
            string targetFile,
            CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile)!);

            using var sourceStream = new FileStream(
                sourceFile,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            using var targetStream = new FileStream(
                targetFile,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None);

            await sourceStream.CopyToAsync(targetStream, cancellationToken);
        }
    }
}
