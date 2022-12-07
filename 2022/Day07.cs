using AdventOfCodeSupport;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode._2022;

public class Day07 : AdventBase
{
    private Directory? _root;
    private List<Directory> _directories = null!;

    public class File
    {
        public Directory Parent { get; init; } = null!;
        public long Bytes { get; set; }
    }

    public class Directory : File
    {
        public Dictionary<string, File> Contents { get; } = new();
    }

    [Benchmark]
    public void BuildTree()
    {
        _root = new Directory();
        _directories = new List<Directory>();
        var currentDirectory = _root;
        foreach (var line in Input.Lines)
        {
            if (line.StartsWith("$ cd"))
            {
                var newDir = line.Split(' ')[^1];
                currentDirectory = newDir switch
                {
                    "/" => _root,
                    ".." => currentDirectory.Parent,
                    _ => (Directory)currentDirectory.Contents[newDir]
                };
            }
            else if (line.StartsWith("$ ls"))
            {
                continue;
            }
            else if (line.StartsWith("dir "))
            {
                var newDir = line.Split(' ')[^1];
                var create = new Directory
                {
                    Parent = currentDirectory
                };
                currentDirectory.Contents[newDir] = create;
                _directories.Add(create);
            }
            else
            {
                var file = line.Split(' ');
                var size = long.Parse(file[0]);
                var name = file[1];
                
                currentDirectory.Contents[name] = new File
                {
                    Parent = currentDirectory,
                    Bytes = size
                };
            }
        }
    }

    [Benchmark]
    public void CalcDirSizes()
    {
        if (_root is null) BuildTree();
        CalcDirSizes(_root!);
    }

    private void CalcDirSizes(Directory current)
    {
        foreach (var kvp in current.Contents)
        {
            if (kvp.Value is not Directory directory) continue;
            if (!directory.Contents.Any(x => x.Value is Directory))
            {
                directory.Bytes = directory.Contents.Sum(x => x.Value.Bytes);
                continue;
            }
            CalcDirSizes(directory);
            directory.Bytes = directory.Contents.Sum(x => x.Value.Bytes);
        }
        
        if (current == _root)
            current.Bytes = current.Contents.Sum(x => x.Value.Bytes);
    }
    
    protected override object InternalPart1()
    {
        if (_root is null) BuildTree();
        if (_root!.Bytes == 0) CalcDirSizes(_root);
        var lessThan100K = _directories
            .Where(x => x.Bytes <= 100_000)
            .Sum(x => x.Bytes);
        
        return lessThan100K;
    }

    protected override object InternalPart2()
    {
        if (_root is null) BuildTree();
        if (_root!.Bytes == 0) CalcDirSizes(_root);

        const int maxSpace = 70_000_000;
        const int neededFree = 30_000_000;
        const int mustBeLessThan = maxSpace - neededFree;
        
        var difference = _root.Bytes - mustBeLessThan;
        var toRemove = _directories
            .Where(x => x.Bytes >= difference)
            .OrderBy(x => x.Bytes)
            .First(x => x.Bytes >= difference);
        
        return toRemove.Bytes;
    }
}
