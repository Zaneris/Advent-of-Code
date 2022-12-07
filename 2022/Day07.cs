using AdventOfCodeSupport;

namespace AdventOfCode._2022;

public class Day07 : AdventBase
{
    private readonly Directory _root = new();

    private readonly List<Directory> _directories = new();

    private class File
    {
        public Directory Parent { get; init; } = null!;
        public long Bytes { get; set; }
    }

    private class Directory : File
    {
        public Dictionary<string, File> Contents { get; } = new();
    }

    private void BuildTree()
    {
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

    private void SizeUp(Directory current)
    {
        foreach (var kvp in current.Contents)
        {
            if (kvp.Value is not Directory directory) continue;
            if (!directory.Contents.Any(x => x.Value is Directory))
            {
                directory.Bytes = directory.Contents.Sum(x => x.Value.Bytes);
                continue;
            }
            SizeUp(directory);
            directory.Bytes = directory.Contents.Sum(x => x.Value.Bytes);
        }
        
        if (current == _root)
            current.Bytes = current.Contents.Sum(x => x.Value.Bytes);
    }
    
    protected override object InternalPart1()
    {
        BuildTree();
        SizeUp(_root);
        var lessThan100K = _directories
            .Where(x => x.Bytes <= 100_000)
            .Sum(x => x.Bytes);
        
        return lessThan100K;
    }

    protected override object InternalPart2()
    {
        BuildTree();
        SizeUp(_root);

        const int maxSpace = 70_000_000;
        const int neededFree = 30_000_000;
        const int mustBeLessThan = maxSpace - neededFree;
        
        var difference = _root.Bytes - mustBeLessThan;
        var toRemove = _directories
            .OrderBy(x => x.Bytes)
            .First(x => x.Bytes >= difference);
        
        return toRemove.Bytes;
    }
}
