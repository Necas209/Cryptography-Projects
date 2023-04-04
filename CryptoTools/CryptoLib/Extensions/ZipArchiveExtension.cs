using System.IO.Compression;

namespace CryptoLib.Extensions;

public static class ZipArchiveExtension
{
    private static void CreateEntryFromAny(this ZipArchive archive, string sourceName, string entryName = "")
    {
        var fileName = Path.GetFileName(sourceName);
        if (File.GetAttributes(sourceName).HasFlag(FileAttributes.Directory))
            archive.CreateEntryFromDirectory(sourceName, Path.Combine(entryName, fileName));
        else
            archive.CreateEntryFromFile(sourceName, Path.Combine(entryName, fileName), CompressionLevel.Fastest);
    }

    public static void CreateEntryFromDirectory(this ZipArchive archive, string sourceDirName, string entryName = "")
    {
        var files = Directory.GetFiles(sourceDirName).Concat(Directory.GetDirectories(sourceDirName)).ToArray();
        archive.CreateEntry(Path.Combine(entryName, Path.GetFileName(sourceDirName)));
        foreach (var file in files) archive.CreateEntryFromAny(file, entryName);
    }
}