using System.Collections.Concurrent;

namespace AutoIdDictionary.Lib;

public class AutoIdDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
{
	private ConcurrentQueue<int> _freeIds = new();
	private Dictionary<TKey, int> _ids = new();

	public int GetId(TKey key)
	{
		if (_ids.TryGetValue(key, out var id))
		{
			return id;
		}

		throw new Exception();
	}

	public TKey GetKey(int id)
	{
		var key = _ids.FirstOrDefault(x => x.Value == id).Key;

		if (key == null)
		{
			throw new Exception();
		}

		return key;
	}

	public TValue this[int id]
	{
		get
		{
			var key = GetKey(id);
			return this[key];
		}
		set
		{
			var key = GetKey(id);
			this[key] = value;
		}
	}

	private int CreateId(TKey key)
	{
		if (_ids.TryGetValue(key, out var id))
		{
			throw new Exception();
		}

		if (_freeIds.TryDequeue(out var newId))
		{
			_ids.Add(key, newId);
			return newId;
		}

		var nextId = _ids.Count();

		_ids.Add(key, nextId);
		return nextId;
	}

	private void DestroyId(TKey key)
	{
		if (_ids.TryGetValue(key, out var id))
		{
			_freeIds.Enqueue(id);
			_ids.Remove(key);
		}

		throw new Exception();
	}

	public bool TryAdd(TKey key, TValue value)
	{
		if (base.TryAdd(key, value))
		{
			CreateId(key);
			return true;
		}
		return false;
	}

	public new void Add(TKey key, TValue value)
	{
		base.Add(key, value);

		CreateId(key);
	}

	public new bool Remove(TKey key)
	{
		if (base.Remove(key))
		{
			DestroyId(key);
			return true;
		}

		return false;
	}
}
