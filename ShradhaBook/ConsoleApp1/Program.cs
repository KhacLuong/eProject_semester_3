// See https://aka.ms/new-console-template for more information
using NLipsum.Core;
var generator = new LipsumGenerator();
var genWords = generator.GenerateCharacters(10);
Console.WriteLine($"{genWords.Length} - {genWords[0]}");

