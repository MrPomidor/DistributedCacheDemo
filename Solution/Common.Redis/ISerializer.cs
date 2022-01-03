﻿using System.Diagnostics.CodeAnalysis;

namespace Common.Redis;

public interface ISerializer
{
    string Serialize<T>([NotNull] T item);
    T Deserialize<T>([NotNull] string serialized);
}
