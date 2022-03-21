using System.Collections;

// ReSharper disable MemberCanBePrivate.Global

namespace Crowbar90.Common.Utilities.Generics;

public sealed class TwoWayDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    where TKey : notnull
    where TValue : notnull
{
    private readonly Dictionary<TKey, TValue> _forward = new();
    private readonly Dictionary<TValue, TKey> _backward = new();

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _forward.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        var (key, value) = item;
        Add(key, value);
    }
    
    public void Add(KeyValuePair<TValue, TKey> item)
    {
        var (value, key) = item;
        Add(key, value);
    }
    
    public void Add(TKey key, TValue value)
    {
        _forward.Add(key, value);
        _backward.Add(value, key);
    }
    
    public void Add(TValue value, TKey key)
    {
        _forward.Add(key, value);
        _backward.Add(value, key);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var (key, value) = item;
        
        if (!_forward.ContainsKey(key))
            return false;
        
        _forward.Remove(key);
        _backward.Remove(value);
        return true;
    }

    public bool Remove(KeyValuePair<TValue, TKey> item)
    {
        var (value, key) = item;
        
        if (!_backward.ContainsKey(value))
            return false;
        
        _forward.Remove(key);
        _backward.Remove(value);
        return true;
    }

    public bool Remove(TKey key)
    {
        if (!_forward.TryGetValue(key, out var value))
            return false;
        
        _forward.Remove(key);
        _backward.Remove(value);
        return true;
    }
    public bool Remove(TValue value)
    {
        if (!_backward.TryGetValue(value, out var key))
            return false;
        
        _forward.Remove(key);
        _backward.Remove(value);
        return true;
    }

    public void Clear()
    {
        _forward.Clear();
        _backward.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) =>_forward.Contains(item);
    public bool Contains(KeyValuePair<TValue, TKey> item) =>_backward.Contains(item);
    public bool ContainsKey(TKey key) => _forward.ContainsKey(key);
    public bool ContainsKey(TValue value) => _backward.ContainsKey(value);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        
        if (array.Length - arrayIndex < Count)
            throw new ArgumentException("Not enough elements after arrayIndex in the destination array.");

        for (var i = 0 ; i< _forward.Count ; i++)
            array[i + arrayIndex] = _forward.ElementAt(i);
    }
    
    public void CopyTo(KeyValuePair<TValue, TKey>[] array, int arrayIndex)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        
        if (array.Length - arrayIndex < Count)
            throw new ArgumentException("Not enough elements after arrayIndex in the destination array.");

        for (var i = 0 ; i< _forward.Count ; i++)
            array[i + arrayIndex] = _backward.ElementAt(i);
    }

    public int Count => _forward.Count;
    public bool IsReadOnly => false;


    public bool TryGetValue(TKey key, out TValue value) => _forward.TryGetValue(key, out value!);
    public bool TryGetValue(TValue value, out TKey key) => _backward.TryGetValue(value, out key!);

    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out var value))
                return value;

            throw new KeyNotFoundException();
        }
        set
        {
            if (ContainsKey(key))
                Remove(key);
            
            Add(key, value);
        }
    }
    public TKey this[TValue accValue]
    {
        get
        {
            if (TryGetValue(accValue, out var key))
                return key;

            throw new KeyNotFoundException();
        }
        set
        {
            if (ContainsKey(accValue))
                Remove(accValue);
            
            Add(accValue, value);
        }
    }

    public ICollection<TKey> Keys => _forward.Keys;
    public ICollection<TValue> Values => _forward.Values;
}