namespace Metadata
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MetadataExtractor;
    using MetadataExtractor.Formats.Exif;
    using MetadataExtractor.Util;

    using Directory = MetadataExtractor.Directory;

    public class Program
    {
        private static void FindUnknown(IEnumerable<FileInfo> files)
        {
            Parallel.ForEach(
                files,
                fileInfo =>
                    {
                        using var stream = fileInfo.OpenRead();
                        if (FileTypeDetector.DetectFileType(stream) == FileType.Unknown)
                        {
                            Console.WriteLine($"Unknown File detected: {fileInfo.FullName}");
                        }
                    });
        }

        private static DateTime? GetDateTimeOriginal(IReadOnlyList<Directory> directories)
        {
            var dateTimeOriginal = directories.OfType<ExifDirectoryBase>()
                .Select(
                    d =>
                        {
                            if (DirectoryExtensions.TryGetDateTime(
                                d,
                                ExifDirectoryBase.TagDateTimeOriginal,
                                out DateTime r))
                            {
                                return (DateTime?)r;
                            }

                            return null;
                        })
                .FirstOrDefault(r => r != null);

            return dateTimeOriginal;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const string RootMediaPath = @"Y:\Processed\";

            var di = new DirectoryInfo(RootMediaPath);

            var files = di.EnumerateFiles("*.*", SearchOption.AllDirectories);

            // TryVideo(files);
            // TryImages(files);
            FindUnknown(files);
        }

        private static void TryImages(IEnumerable<FileInfo> files)
        {
            foreach (var fileInfo in files)
            {
                using var stream = fileInfo.OpenRead();
                var detectedType = FileTypeDetector.DetectFileType(stream);

                if (detectedType == FileType.Unknown)
                {
                    continue;
                }

                var directories = ImageMetadataReader.ReadMetadata(stream);
                var original = GetDateTimeOriginal(directories);

                // var allTags = directories.SelectMany(d => d.Tags.Select(t => $"{d.GetTagName(t.Type)}: {d.GetDescription(t.Type)}"));
                // var tagsFormatted = string.Join($"{Environment.NewLine}\t", allTags);
                Console.WriteLine($"Image: {fileInfo.FullName} / {original}:");

                // Console.WriteLine(tagsFormatted);
            }
        }

        private static void TryVideo(IEnumerable<FileInfo> files)
        {
            var mp4 = files.FirstOrDefault(f => f.Extension == ".mp4");
            using (var stream = mp4.OpenRead())
            {
                var detectFileType = FileTypeDetector.DetectFileType(stream);
                var directories = ImageMetadataReader.ReadMetadata(stream);
            }
        }
    }
}