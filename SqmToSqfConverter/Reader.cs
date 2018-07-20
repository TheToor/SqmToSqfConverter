using SqmToSqfConverter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SqmToSqfConverter
{
    internal class Reader
    {
        private static Regex ClassRegex = new Regex(@"class (.*)");
        private static Regex ClassStartRegex = new Regex(@"{");
        private static Regex ClassEndRegex = new Regex(@"};");

        private static Regex ValueRegex = new Regex(@"(.+?)\s*=(.*);");
        private static Regex TypeRegex = new Regex(@"type\[\]=");

        private string _path;

        internal Reader(string path)
        {
            _path = path;
        }

        internal SimpleClass ReadMissionFile()
        {
            var lines = File.ReadAllLines(_path);
            var rawVersion = lines.First();
            var versionMatch = ValueRegex.Match(rawVersion);
            if (!versionMatch.Success)
                throw new InvalidDataException("Invalid SQM file version");

            var version = versionMatch.Groups[2].Value;
            if (version != "53")
                throw new InvalidDataException("Not supported SQM file version");

            var source = lines.Skip(1).ToList();
            for (int i = 0; i < source.Count; i++)
            {
                var currentLine = source[i];
                var match = ClassRegex.Match(currentLine);

                //We are only interested here in classes
                if (!match.Success)
                {
                    Debug.WriteLine($"Skipping {currentLine}");
                    continue;
                }

                var processed = ProcessClass(source, i, out var processedClass);
                i += processed;

                Debug.WriteLine($"=============================");
                Debug.WriteLine($"{processedClass.ClassName}");
                Debug.WriteLine($"{processedClass.Properties.Count}");
                Debug.WriteLine($"{processedClass.SubClasses.Count}");
                Debug.WriteLine($"=============================");

                if (processedClass.ClassName == "Mission")
                    return processedClass;
            }

            return null;
        }

        private int ProcessClass(List<string> lines, int startLine, out SimpleClass currentClass)
        {
            var nestingLevel = 0;

            var classes = new Stack<SimpleClass>();
            currentClass = null;

            for(int i = startLine; i < lines.Count; i++)
            {
                var currentLine = lines[i];

                if (ValueRegex.IsMatch(currentLine))
                {
                    if (currentClass == null)
                        throw new Exception("Got propertie without class");

                    var match = ValueRegex.Match(currentLine);
                    var key = match.Groups[1].Value.Trim();
                    var value = match.Groups[2].Value.Trim();

                    currentClass.Properties.Add(key, value);

                    Debug.WriteLine($"{key}: {value}");
                }
                else if (ClassRegex.IsMatch(currentLine))
                {
                    nestingLevel++;

                    var match = ClassRegex.Match(currentLine);
                    var className = match.Groups[1].Value;

                    Debug.WriteLine($"==== Start: {className} ==== ({i})");

                    var newClass = new SimpleClass()
                    {
                        ClassName = className
                    };
                    currentClass = newClass;
                    classes.Push(newClass);
                }
                else if(TypeRegex.IsMatch(currentLine))
                {
                    Debug.WriteLine($"== type[] match === ({i})");

                    var newClass = new SimpleClass()
                    {
                        ClassName = "type[]",
                        Properties = new Dictionary<string, string>()
                        {
                            { "type", lines[i + 2].Trim() }
                        }
                    };
                    currentClass.SubClasses.Add(newClass.ClassName, newClass);
                    i = i + 3;
                }
                else if(ClassStartRegex.IsMatch(currentLine))
                {
                    //start of a class
                }
                else if(ClassEndRegex.IsMatch(currentLine))
                {
                    //end of a class
                    if (classes.Count == 0)
                        continue;

                    nestingLevel--;

                    var oldClass = classes.Pop();
                    Debug.WriteLine($"==== End: {oldClass.ClassName} ==== ({i})");

                    if (nestingLevel > 0)
                    {
                        currentClass = classes.Peek();
                        currentClass.SubClasses.Add(oldClass.ClassName, oldClass);
                    }
                    else
                        return i - startLine;
                }
                else
                {
                    Debug.WriteLine($"Invalid line: {currentLine}");
                }
            }

            return -1;
        }
    }
}
