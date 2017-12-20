﻿using AlephNote.PluginInterface;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlephNote.PluginInterface.Util;

namespace AlephNote.Plugins.Filesystem
{
	public static class FileSystemUtil
	{
		public static IEnumerable<string> EnumerateFilesDeep(string baseFolder, int remainingDepth)
		{
			if (remainingDepth == 0) yield break;

			foreach (var f in Directory.EnumerateFiles(baseFolder)) yield return f;

			foreach (var d in Directory.EnumerateDirectories(baseFolder))
			{
				foreach (var f in EnumerateFilesDeep(d, remainingDepth-1)) yield return f;
			}
		}

		public static void DeleteFileAndFolderIfEmpty(IAlephLogger log, string baseFolder, string file)
		{
			File.Delete(file);

			DeleteFolderIfEmpty(log, baseFolder, Path.GetDirectoryName(file));
		}

		public static void DeleteFolderIfEmpty(IAlephLogger log, string baseFolder, string folder)
		{
			var p1 = Path.GetFullPath(baseFolder).TrimEnd(Path.DirectorySeparatorChar).ToLower();
			var p2 = Path.GetFullPath(folder).TrimEnd(Path.DirectorySeparatorChar).ToLower();
			if (p1 == p2) return;
			if (p1.Count(c => c == Path.DirectorySeparatorChar) >= p2.Count(c => c == Path.DirectorySeparatorChar)) return;

			if (Directory.EnumerateFileSystemEntries(folder).Any()) return;

			log.Debug(FilesystemPlugin.Name, $"Cleanup empty folder '{p2}' (base = '{p1}')");
			Directory.Delete(folder);
		}

		public static DirectoryPath GetDirectoryPath(string pBase, string pInfo)
		{
			var sBase = Path.GetFullPath(pBase).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
			var sInfo = Path.GetFullPath(pInfo).TrimEnd(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

			var relative = sInfo.Skip(sBase.Length);

			return DirectoryPath.Create(relative);
		}
	}
}
