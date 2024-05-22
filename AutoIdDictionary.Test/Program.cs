using AutoIdDictionary.Lib;

var dictionary = new AutoIdDictionary<string, string>() { 
	{ "pre id 0", "pre key 0" },
	{ "pre id 1", "pre key 1" },
	{ "pre id 2", "pre key 2" },
	{ "pre id 3", "pre key 3" },
	{ "pre id 4", "pre key 4" },
};

for (int i = 0; i < 10; i++)
{
	dictionary.Add($"id {i}", $"key {i}");
}

foreach (var item in dictionary)
{
	Console.WriteLine($"{dictionary.GetId(item.Key)}. {item.Key} - {item.Value}");
}
Console.WriteLine();

for (int i = 0; i < 5; i++)
{
	dictionary.Remove($"id {i}");
}

foreach (var item in dictionary)
{
	Console.WriteLine($"{dictionary.GetId(item.Key)}. {item.Key} - {item.Value}");
}
Console.WriteLine();

for (int i = 10; i < 20; i++)
{
	dictionary.Add($"id {i}", $"key {i}");
}

foreach (var item in dictionary)
{
	Console.WriteLine($"{dictionary.GetId(item.Key)}. {item.Key} - {item.Value}");
}
Console.WriteLine();
